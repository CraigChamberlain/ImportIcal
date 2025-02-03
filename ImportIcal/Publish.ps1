dotnet publish -c Release
Push-Location ".\ImportIcal\bin\Release\net7.0\publish"
Publish-Module -Name .\ImportIcal.psd1 -NuGetApiKey (Get-Secret NuGetApiKey -AsPlainText ) -AllowPrerelease
Pop-Location


