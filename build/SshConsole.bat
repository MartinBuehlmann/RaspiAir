@echo off

set raspi_air_name=%raspi_air_name%
set key_file="C:\temp\Raspberry\ssh_keys\id_rsa"

ssh -i "C:\temp\Raspberry\ssh_keys\id_rsa" pi@%raspi_air_name%