set host_value=%1
set pathofsqlfile=%2
cd/
cd %pathofsqlfile%
sqlplus system/manager@%host_value% @sqlqry2.sql
exit
exit