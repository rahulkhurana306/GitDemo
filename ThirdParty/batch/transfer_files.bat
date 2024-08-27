@echo off
rem Run this script to transfer all files with a given prefix from the specified environment's print_outs directory.
rem Parameters:
rem     1 = path to copy from, i.e. print_outs, xfer, etc.
rem     2 = path to copy to on the local PC
rem     3 = file copy prefix
rem     4 = host to copy from
rem     5 = environment ID (SID)
rem     6 = host user name
rem     7 = host user password (SID)
rem     8 = remote source directory

rem 	set password=squash77

set path_from=%1
set path_to=%2
set file_prefix=%3
set host_name=%4
set env_sid=%5
set host_username=%6
set host_password=%7
set remote_source_dir=%8

echo Path to copy from %1
echo Path to copy to %2
echo File copy prefix %3
echo Host to copy from %4
echo Environment SID %5
echo RemoteSourceDir %8

set password=%host_password%
set PATH=%PATH%;%~dp0\..\PuTTY
set try_counter=0

:try_transfer

set password=%host_password%

echo Trying to copy files from %path_from%/%env_sid%/%file_prefix%* on host %host_name%
echo y | pscp -p -pw %password% %host_username%@%host_name%:%remote_source_dir%/%env_sid%/%path_from%/%file_prefix%*  %path_to%

if not %ERRORLEVEL% == 0 (
	echo "Error! Could not transfer files from host %4"
	goto transfer_failed
)

echo Transfer complete!
exit 0

:transfer_failed

set /A try_counter+=1

if %try_counter% lss 3 (
	echo "Retrying with another password..."
	
	if %try_counter% equ 1 (
		set password=squash
	)
	
	if %try_counter% equ 2 (
		set password=summit5
	)
	
	if %try_counter% equ 3 (
		set password=squash7
	)
	
	goto try_transfer
)

if %try_counter% gtr 3 (
	echo Run out of passwords to try, and none of the known passwords have worked for the host %host_name%, therefore exiting...
	echo Please contact Development Services, as this script has failed for environment %env_sid% on %host_name%.

	exit 1
)
