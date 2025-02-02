dotnet publish
Import-Module "$PSScriptRoot/bin/Debug/net7.0/publish/ImportIcal.dll"

Invoke-Pester "$PSScriptRoot/tests"