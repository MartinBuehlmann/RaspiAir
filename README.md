# RaspiAir
Air quality checker using a Raspberry Pi and a SCD41 sensor.

## Measurement
The project uses a seeed studio Grove - CO2 & Temperature & Humidity Sensor (SCD41) which is connected
via I2C interface to the Raspberry PI Zero 2.

### Specification
| Item | Value |
| :--- | :---- |
| Working voltage | 2.4V~5V |
| Operating range | -10~+60C; 0-100% r.H.; 0-40'000ppm |
| I2C Address | 0x62 |

More details can be found here: https://wiki.seeedstudio.com/Grove-CO2_&_Temperature_&_Humidity_Sensor-SCD41/

## Visualization
This project provides a Blazor WebAssembly based web frontend.

## Overall State Visualization
To give a rough overview about the current air quality, the project can visualize the overall state via
a WS2812B based LED stripe. This stripe is connected using the Raspberry PI Zero 2 SPI interface.[1 - Setup of Raspberry PI Zero 2.md](documents/1%20-%20Setup%20of%20Raspberry%20PI%20Zero%202.md)[1 - Setup of Raspberry PI Zero 2.md](documents/1%20-%20Setup%20of%20Raspberry%20PI%20Zero%202.md)[2 - Install and Configure nginx as Proxy.md](documents/2%20-%20Install%20and%20Configure%20nginx%20as%20Proxy.md)