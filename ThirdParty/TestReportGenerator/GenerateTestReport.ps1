[CmdletBinding()]
param ($ReportSharedLocation, $ReportOutDir)
#param ([Parameter(Mandatory)]$ReportSharedLocation, $ReportOutDir)

#Common Parameters
function Get-Test-Environment-Detail ($FilePath)
{
    if($FilePath)
    {
        $header=Get-Content $FilePath -First 1
        $HeaderFields=$header.Split(",")
        $VmUser=$HeaderFields[0] |Select-String -Pattern "\[(.*)\].*" | % {$_.Matches.Groups[1].Value}
        $VmHost=$HeaderFields[1] |Select-String -Pattern "\[(.*)\].*" | % {$_.Matches.Groups[1].Value}
        $TestEnvironment=$HeaderFields[2] |Select-String -Pattern "\[(.*)\].*" | % {$_.Matches.Groups[1].Value}
        $ArtifactURL=$HeaderFields[3] |Select-String -Pattern "\[(.*)\].*" | % {$_.Matches.Groups[1].Value}
        $SummitVersion=$HeaderFields[4] |Select-String -Pattern "\[(.*)\].*" | % {$_.Matches.Groups[1].Value}
        $ActivateVersion=$HeaderFields[5] |Select-String -Pattern "\[(.*)\].*" | % {$_.Matches.Groups[1].Value}
		if($SummitVersion -eq $null){
			$SummitVersion="LATEST"
		}
		if($ActivateVersion -eq $null){
			$ActivateVersion="LATEST"
		}
        $VmUser
        $VmHost
        $TestEnvironment
        $ArtifactURL
        $SummitVersion
        $ActivateVersion
    }
}

function Process-ReportSummary-File ($FilePath) {
        
    Write-Verbose "Process Report-Summary File: $FilePath"
    if($FilePath)
    {
        #$testPlanName=$FilePath | Select-String -Pattern "\\(.*)_R.*.csv" | % {$_.Matches.Groups[1].Value}
        
        $testPlanName=Split-Path $FilePath -leaf | Select-String -Pattern "(.*)_R.*.csv" | % {$_.Matches.Groups[1].Value}
        
    
        $envDetails=Get-Test-Environment-Detail -FilePath $FilePath

        $VmUser=$envDetails[0]
        $VmHost=$envDetails[1]
        $TestEnvironment=$envDetails[2]
        $ArtifactURL=$envDetails[3]
        $SummitVersion=$envDetails[4]
        $ActivateVersion=$envDetails[5]
        $CombinedVersionKey=@($TestEnvironment,$VmHost,$VmUser) -join ","
        $CombinedVersionRecord=@($VmHost,$VmUser,$ActivateVersion,$TestEnvironment,$SummitVersion) -join ","
		
        if(-not $UniqueVersionData.ContainsKey($CombinedVersionKey)){
            $UniqueVersionData[$CombinedVersionKey]=$CombinedVersionRecord
        }
        $reportData=Get-Content $FilePath

        for($i=1; $i -lt $reportData.Count; $i++)
        { 

            $eachRecordField=$reportData[$i].Split(",")

            $scriptName=$eachRecordField[0]
            if($scriptName -inotlike "job*")
            {
                $testCaseId=$eachRecordField[0]| Select-String -Pattern "_(\d{6,})" | % {$_.Matches.Groups[1].Value}
                $status=$eachRecordField[1]| Select-String -Pattern "\[(.*)\]" | % {$_.Matches.Groups[1].Value}    
                $duration=$eachRecordField[2]| Select-String -Pattern "\((.*)\)" | % {$_.Matches.Groups[1].Value}    
                $startedOn=$eachRecordField[3]    

                 try
                {
                    if($testCaseId)
                    {
                        
                        $CombinedMidRecord=$testCaseId+","+$scriptName+","+$status+","+$duration+","+$startedOn+","+$VmHost+","+$VmUser+","+$TestEnvironment+","+$ArtifactURL+","+$testPlanName 
                        #Write-Verbose $testCaseId"==>"$status
                        #Write-Verbose "Store results for $testCaseId==>$CombinedMidRecord"                        
                        if($UniqueTestCaseData.ContainsKey($testCaseId)){
                            
                            $PreviousValue=$UniqueTestCaseData[$testCaseId]
                            $PreviousStatus=$PreviousValue.Split(",")[2]
                            
                            #Write-Verbose PreviousStatus=>$PreviousStatus CurrentStatus=>$status
                            # retry>Fail>Abort>Skip>Pass
                            if($testPlanName -ilike "retry*")
                            { 
                               $PreviousPlan=$PreviousValue.Split(",")[9]
                               if($CombinedMidRecord -inotlike "retry*")
                               {
                                   $UniqueTestCaseData[$testCaseId]=$CombinedMidRecord+"_"+$PreviousPlan
                               }
                       
                            }elseif($status -ilike "Fail")
                            {
                                #Current Previous Result
                                #Fail   Pass    Fail
                                #Fail   Abort   NA
                                #Fail   Skip    Fail
                                #Fail   Fail    Fail
                                
                                #Write-Verbose "Fail test-cases has highest priority"                                
                                $UniqueTestCaseData[$testCaseId]=$CombinedMidRecord  
                                
                            }elseif($status -ilike "Abort" -and $PreviousStatus -inotlike "Fail")
                            {
                                #Current    Previous    Result
                                #Abort  Pass    Abort
                                #Abort  Abort   Abort
                                #Abort  Skip    Abort
                                #Abort  Fail    Fail
                                $UniqueTestCaseData[$testCaseId]=$CombinedMidRecord  
                                
                            }elseif($status -ilike "Skip" -and $PreviousStatus -inotlike "Fail"){
                                #Current    Previous    Result
                                #Skip   Pass    Skip
                                #Skip   Abort   NA
                                #Skip   Skip    Skip
                                #Skip   Fail    Fail.
                                $UniqueTestCaseData[$testCaseId]=$CombinedMidRecord                                
                            }elseif($status -ilike "Pass" -and $PreviousStatus -inotlike "Pass")
                            {
                                #Current    Previous    Result
                                #Pass   Pass    Pass
                                #Pass   Abort   Abort
                                #Pass   Skip    Skip
                                #Pass   Fail    Fail
                                #Write-Verbose "Do not perform any operation as CurrentStatus=$status , PreviousStatus=$PreviousStatus"                                
                            
                            }
                            
                        }else{
                            $UniqueTestCaseData[$testCaseId]=$CombinedMidRecord
                        }
                    }

                }
                catch [System.Exception]
                {
                    Write-Verbose $_.Exception
                    Write-Verbose "[Failed to store] $testCaseId==>$CombinedMidRecord"                   
                }
                finally
                {
                    
                }

            }
        }

     $UniqueTestCaseData   
    }

    
}

#Remove-Variable * -ErrorAction SilentlyContinue

if ("$ReportSharedLocation" -eq "") {
$ReportSharedLocation = "D:\Startup\Jenkin_Shared_Report"
}
if ($ReportOutDir -eq $null) {
$ReportOutDir = $ReportSharedLocation
}


$FilePath="$ReportOutDir\Output.ini"
$CombinedCsvReport="$ReportOutDir\Results.csv"
$CombinedVersionReport="$ReportOutDir\Versions.csv"
$CombinedHTMLReport="$ReportOutDir\Results.html"
$CombinedVersionHTMLReport="$ReportOutDir\Versions.html"
$UniqueTestCaseData=@{}
$UniqueTestCaseData.Clear()
$UniqueVersionData=@{}
$UniqueVersionData.Clear()
Write-Verbose "Prepare List of Results files to read. Check $FilePath"

Get-ChildItem -Path "$ReportSharedLocation"  -Recurse  -Filter *.csv -Exclude Config*.csv,Results.csv| Sort-Object LastWriteTime |  Format-Table -Property FullName -HideTableHeaders   | Out-String -Width 4096 | Out-File $FilePath


#Write Header in CSV Formatted Report. (Same headers also used in HTML Report Generation)
Write-Output "TestCaseId,ScriptName,Status,Duration,StartedOn,VmHost,VmUser,TestEnvironment,ArtifactURL,TestPlanName"| Out-File $CombinedCsvReport

#Write Header in CSV Formatted Version Report.
Write-Output "VmHost,VmUser,ActivateVersion,TestEnvironment,SummitVersion"| Out-File $CombinedVersionReport
$ListOfResultFiles=Get-Content $FilePath 

for($i=0; $i -lt $ListOfResultFiles.Count; $i++)
{
    if($ListOfResultFiles[$i])
    {    
        #Process-ReportSummary-File -FilePath $ListOfResultFiles[$i]
        $UniqueTestCaseData=Process-ReportSummary-File -FilePath $ListOfResultFiles[$i]
        #echo $UniqueTestCaseData.Count
        #$UniqueTestCaseData | ConvertTo-Json
    }
     
}

$UniqueTestCaseData.GetEnumerator() | Sort-Object Name  | Format-Table -Property Value -HideTableHeaders -Autosize | Out-String -Width 4096 | % { $_ -Replace '[ \t]+ ', '' }|% { $_.trim()}| Out-File -Append $CombinedCsvReport 
$UniqueVersionData.GetEnumerator() | Sort-Object Name  | Format-Table -Property Value -HideTableHeaders -Autosize | Out-String -Width 1024 |% { $_.trim()}| Out-File -Append $CombinedVersionReport 
#Report Summary Data
$totalTestCaseCount=$UniqueTestCaseData.Count
$passTestCaseCount=0
$failTestCaseCount=0
$skipTestCaseCount=0
$abortTestCaseCount=0
$UniqueTestCaseData.Values | Foreach {if($_ -ilike '*,pass,*'){$passTestCaseCount++};if($_ -ilike '*,fail,*'){$failTestCaseCount++};if($_ -ilike '*,abort,*'){$abortTestCaseCount++};if($_ -ilike '*,Skip,*'){$skipTestCaseCount++}}

#HTML Report Content preparation
$ReportPreContent=@"
<div align="center">
<h1>MSS-Automation-Test-Report</h1>
<h2>Total Test Count = $totalTestCaseCount</h2>
<h2>Pass = $passTestCaseCount</h2>
<h2>Fail = $failTestCaseCount</h2>
<h2>Abort = $abortTestCaseCount</h2>
<h2>Skip = $skipTestCaseCount</h2>
</div>
"@


$ReportHeadSection=@"
<title>MSS-Test-Report</title>
<style>
table {
  border-spacing: 1px;
  width : 80%;
  table-layout: auto;
  margin-left: auto;
  margin-right: auto;
}

th {
  color:white;
  border: 0px solid ;
  background: green
}

td {
  border: 0px solid;
  word-wrap: break-word;

}
tr:nth-child(even) {background-color: lightgray;}
tr:nth-child(odd) {background-color: white;}
tr:hover {background-color: gray;}

.FailStatus{
  background-color: red;
}

.PassStatus{
  background-color: lightgreen;
}
.AbortStatus{
  background-color: gray;
}
.SkipStatus{
  background-color: lightyellow;
}
</style>
"@

$currentDateTime=Get-Date -Format "MM/dd/yyyy HH:mm:ss"

Import-Csv -Path $CombinedVersionReport -Delimiter "," |
 Group-Object -Property Sort |
 Foreach {$_.Group | ConvertTo-Html} |
 Out-File $CombinedVersionHTMLReport

$ReportPostContent=$env:QUICK_DISPLAY_URL+"<br>Report generated at "+$currentDateTime


#Generate HTML Report
Import-Csv -Path $CombinedCsvReport -Delimiter "," |
 Group-Object -Property Sort |
 Foreach {$_.Group | ConvertTo-Html -Property TestCaseId,Status,VmHost,VmUser,TestEnvironment,ArtifactURL,TestPlanName  -Title MSS-Test-Report -PreContent $ReportPreContent -PostContent $ReportPostContent -Head $ReportHeadSection} |
 Out-File $CombinedHTMLReport



try
{
    #Add Fail Highlighted
    (Get-Content $CombinedHTMLReport).replace('>Fail<', '><div class="FailStatus">Fail</div><') | Set-Content $CombinedHTMLReport    
    (Get-Content $CombinedHTMLReport).replace('>Pass<', '><div class="PassStatus">Pass</div><') | Set-Content $CombinedHTMLReport
    (Get-Content $CombinedHTMLReport).replace('>Abort<', '><div class="AbortStatus">Abort</div><') | Set-Content $CombinedHTMLReport
    (Get-Content $CombinedHTMLReport).replace('>Skip<', '><div class="SkipStatus">Skip</div><') | Set-Content $CombinedHTMLReport
    #Link shorting if http url is the 2nd last column. Regex is greedy match.
    (Get-Content $CombinedHTMLReport).replace('<th>ArtifactURL</th>','') | Set-Content $CombinedHTMLReport  
    (Get-Content $CombinedHTMLReport)|% { $_ -Replace '(http.*)</td><td>(.*)</td></tr>', '<a href="$1" href="#">$2</a></td></tr>' }| Set-Content $CombinedHTMLReport
}
catch [System.Exception]
{
    
}
finally
{
}
