$local = [System.Version](Get-ItemProperty "$PSScriptRoot/ImportIcal.dll").versioninfo.ProductVersionRaw

$online = 
	Find-Module ImportIcal | 
	Select-Object -First 1 -ExpandProperty Version |
	ForEach-Object {
		 [System.Version]::new($_)
	}
if($local -lt $online){
	Write-Warning("Version $online available.  Please update: Update-Module ImportIcal")

}elseif($null -eq $online){
	Write-Warning("Please check for updates regularly, this is in early development and I will be adding features regularly.`r`nThe API is prone to change at this stage.")
}
