# Server Box
Store your server credentials and connect easily through RDP or SSH

## Screenshot
![Adding Server](https://raw.githubusercontent.com/royryando/server-box/master/images/add_server.png)
![List of Server](https://raw.githubusercontent.com/royryando/server-box/master/images/home.png)

## Requirements
- Visual Studio (with Net Framework 4.5)
- [LiteDB](https://www.nuget.org/packages/LiteDB/ "LiteDB")

## Create Portable Version
To create portable version you need to install [ILMerge](http://www.download82.com/download/windows/microsoft-ilmerge/ "ILMerge") and follow these step:
- Build project (using Visual Studio)
- Open CMD
- Go to debug folder `cd [root-project]\ServerBox\bin\Debug`
- Execute ilmerge command `ILMerge.exe /target:winexe /targetplatform:"v4,C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5" /out:ServerBoxPortable.exe ServerBox.exe LiteDB.dll`
- Portable version will generated in the current directory as ServerBoxPortable.exe
