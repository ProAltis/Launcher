Project Altis Launcher Source Code
==================================
[![Build Status](https://travis-ci.com/altisofficial/Project-Altis-Launcher.svg?token=gEmYwqjDCsLTVbma9rmy&branch=master)](https://travis-ci.com/altisofficial/Project-Altis-Launcher)

This repository contains all launcher source code to launch into Project Altis

### Info
* When compiling for production, **ALWAYS** make a release build **NEVER** make a debug build
* To build, open the solution file with Visual Studio and click Start. The packages and dependencies will be restored and you will be able to edit the code.
* *Never* force a git push, and always double check with other developers before a merge.
* Create a temporary branch if you are uncertain your changes will have a negative effect to the repository.
* DONT PISS OFF DUBITO


## Releasing with Squirrel

https://youtu.be/PJuIsbCYIaw

Squirrel is a lightning fast installer and update manager framework. It is a c++ bootstraper application and installs within seconds on first run.

Advantages:

1. no UAC prompts
2. click exe -> silently installs within 5 seconds -> opens launcher
3. launcher will not require administrator privileges. 

### Requirements

1. [Nuget Package Explorer](https://www.microsoft.com/store/apps/9wzdncrdmdm3?ocid=badge) (Oof i hope you have win 10)
2. can build RELEASE config in visual studio

### Releasing


1. Set the launcher version in properties/AssemblyInfo.cs to the next version
2. In visual studio **build release**
2. Open NuGet Package Exporer
3. File -> open -> ProjectAltisLauncher.x.x.x.nuspec (latest nuspec)
4. Change version to the version you want to release
5. file -> save metadata as -> save the default name
6. file -> save -> save the default name
7. in visual studio, click toolbar -> VIEW -> other windows -> package manager console
8. in the console, type `Squirrel --releasify `(TAB COMPLETE the nupkg you just made)
9. upload the entire releases folder to web server (specifically to the folder link at Program.cs:MainAsync())

Done! Clients will start updating to that and since Setup.exe has been renamed new people will download latest release.
