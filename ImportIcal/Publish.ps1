﻿dotnet publish -c Release
cd ".\ImportIcal\bin\Release\net7.0\publish"
Publish-Module -Name .\ImportIcal.psd1 -NuGetApiKey (Get-Secret NuGetApiKey -AsPlainText )


