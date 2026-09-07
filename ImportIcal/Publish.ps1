dotnet publish -c Release
Push-Location ".\bin\Release\net10.0\publish"
Publish-Module -Name .\ImportIcal.psd1 -NuGetApiKey (Get-Secret NuGetApiKey -AsPlainText ) -AllowPrerelease
Pop-Location


