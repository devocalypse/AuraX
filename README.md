# AuraX

Automatically turn off AuraSync A-RGB lights when locked and restore lighting to previous state when unlocked.<br />
Runs as a service. Requires Asus MOBO with [Armoury Crate](https://rog.asus.com/us/armoury-crate/) including Aura SDK and [.NET 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)

## Installation (from admin command prompt)

`AuraX --install`
Installs and then starts the service

`AuraX --uninstall`
Stops the service if running and removes it

`AuraX --start`
Starts the service if not running

`AuraX --stop`
Stops the service if not running

## Usage
Win+L to lock PC => Lights turn off<br />
Log on again => Lights come back as they were
