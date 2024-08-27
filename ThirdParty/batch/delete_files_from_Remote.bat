set folder_location=%1
set host_name=%2
set host_username=%3
set host_password=%4
set file_mask=%5

echo folder location %1
echo host name %2
echo host username %3
echo host password %4
echo file_mask %5

echo    cd /				   > command.txt  
echo    cd %folder_location%   >> command.txt  
echo 	rm %file_mask%*.*      >> command.txt
echo 	quit                   >> command.txt

:try_transfer
set PATH=%PATH%;%~dp0\..\PuTTY

psftp %host_username%@%host_name% -pw %host_password% -P 22 < command.txt

if not %ERRORLEVEL% == 0 (
	echo "Error! Could not delete files on host %2"
)

echo Files deleted on server!
exit 0
