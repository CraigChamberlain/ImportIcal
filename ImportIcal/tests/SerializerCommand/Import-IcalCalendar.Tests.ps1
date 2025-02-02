#Requires -Module Pester

Describe 'Import-IcalCalendar functions with a memory stream' {
    It 'Should load form a MemoryStream' {

    $text = Get-Content "$PSScriptRoot\sample.txt" -Raw

    $bytes = [System.Text.Encoding]::UTF8.GetBytes($text)
    $icalStream  = [System.IO.MemoryStream]::new($bytes)
    $icalStream.Position = 0

    $calendar = $icalStream | Import-IcalCalendar -Encoding ([System.Text.Encoding]::UTF8)

    #Deserializer tested in Ical.Net no need to test further here.
    $calendar.ProductId | Should -Be "-//xyz Corp//NONSGML PDA Calendar Version 1.0//EN"
    $calendar.Version | Should -Be  "2.0"
    
    }
}