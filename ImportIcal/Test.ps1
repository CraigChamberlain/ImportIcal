dotnet publish
Import-Module "./bin/Debug/net7.0/publish/ImportIcal.dll"

Invoke-Pester ./tests