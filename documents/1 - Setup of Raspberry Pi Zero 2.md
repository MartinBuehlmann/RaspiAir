# Setup of Raspberry Pi Zero 2

## Enable SPI

To control a WS2812B based LED stripe, SPI (Serial Peripheral Interface) must be enabled, and its frequency must be fixed.
This can be done in file /boot/firmware/config.txt

```
sudo nano /boot/firmware/config.txt
```

Turn on SPI and fix core frequency:
```
dtparam=spi=on
core_freq=250
core_freq_min=250
```

Save the file and reboot the device.
```
sudo reboot
```
### Install RaspiAir as service

The following section describes step-by-step how to configure RaspiAir as a service.

1. Add RaspiAir to _systemd_

To create a systemd service, create a RaspiRobot.service file in the /lib/systemd/system/ directory with the following content:

```
[Unit]
Description=RaspiAir

[Service]
Type=simple
User=pi
WorkingDirectory=/home/pi/RaspiAir/bin
ExecStart=/home/pi/RaspiAir/bin/RaspiAir
Restart=always
RestartSec=5

[Install]
WantedBy=multi-user.target
```

To edit the file use the following command line:

```
sudo nano /lib/systemd/system/RaspiAir.service
```

When the configuration of a service has changed, it might be required to reload the configuration:

```
sudo systemctl daemon-reload
```

2. Enable the service

To ensure that the service gets started after starting/rebooting the device, use the following command line:

```
sudo systemctl enable RaspiAir
```

3. Start/stop the service

The service can be started or stopped by the following commands:

```
sudo systemctl start RaspiAir
```

```
sudo systemctl stop RaspiAir
```

4. Check state of a service

```
sudo systemctl status RaspiAir
```

### Prepare for automatic deployment via script

1. Create ssh key file

Create the public/private keys used later for ssh and scp commands.

```
ssh-keygen -t rsa -b 2048 -f C:\Temp\Raspberry\ssh_keys\id_rsa
```

2. Quick access your Raspberry Pi

The scripts of this project use the environment variable _%raspi_name%_ to configure the name of the device.

Setup this environment variable with the name or ip of your device on your development computer or change the scripts
to hardcode your device.

3. Copy the public key to the Raspberry Pi

In this example the Raspberry Pi is named _raspiair_, if it is different you need to adjust the deployment scripts.

Connect to your Raspberry Pi:

```
ssh pi@raspiair
```

Create the directory on your Raspberry Pi and copy the content of your pubic key file there:

```
mkdir -p ~/.ssh
```

```
echo "<pase your public key file here>" >> ~/.ssh/authorized_keys
```

```
chmod 600 ~/.ssh/authorized_keys
```

4. First time deployment (only first time)

Log in to your Raspberry Pi using ssh and create the required directories.

```
ssh -i "C:\temp\Raspberry\ssh_keys\id_rsa" pi@raspiair

mkdir RaspiAir
mkdir RaspiAir
```

5. Execute deployment script
