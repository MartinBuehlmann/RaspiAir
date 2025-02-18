@echo off

set raspi_air_name=%raspi_air_name%
set key_file="C:\temp\Raspberry\ssh_keys\id_rsa"

rd ..\artifacts\Windows /s /q
call DeployLocalLinux.bat "RaspiAir"
call DeployLocalLinux.bat "RaspiAir.UI"
REM ssh -i %key_file% pi@%raspi_air_name% "sudo systemctl stop RaspiAir"
scp -i %key_file% -r ..\artifacts\Linux\. pi@%raspi_air_name%:/home/pi/RaspiAir/bin
REM ssh -i %key_file% pi@%raspi_air_name% "sudo systemctl start RaspiAir"
pause