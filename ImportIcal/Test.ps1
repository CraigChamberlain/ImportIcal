param(
	[switch] $JustTest
)

if  (!$JustTest){
dotnet publish "$PSScriptRoot" -c Release
}
Import-Module "$PSScriptRoot/bin/Release/net7.0/publish/ImportIcal.psd1"

Invoke-Pester "$PSScriptRoot/tests"