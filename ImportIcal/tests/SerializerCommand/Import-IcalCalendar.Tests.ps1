#Requires -Module Pester

Describe 'Import-IcalCalendar functions with a memory stream' {
    It 'Should load form -Stream' {

    $text = Get-Content "$PSScriptRoot/sample.ics" -Raw

    $bytes = [System.Text.Encoding]::UTF8.GetBytes($text)
    $icalStream  = [System.IO.MemoryStream]::new($bytes)
    $icalStream.Position = 0

    $calendar = $icalStream | Import-IcalCalendar -Encoding ([System.Text.Encoding]::UTF8)

    #Deserializer tested in Ical.Net no need to test further here.
    $calendar.ProductId | Should -Be "-//xyz Corp//NONSGML PDA Calendar Version 1.0//EN"
    $calendar.Version | Should -Be  "2.0"
    
    }
    It 'Should load form a -Path' {

    $calendar = "$PSScriptRoot/sample.ics" | Import-IcalCalendar 

    #Deserializer tested in Ical.Net no need to test further here.
    $calendar.ProductId | Should -Be "-//xyz Corp//NONSGML PDA Calendar Version 1.0//EN"
    $calendar.Version | Should -Be  "2.0"
    
    }
    It 'Should load form a -String' {

    $text = Get-Content "$PSScriptRoot/sample.ics" -Raw

    $calendar = $text | Import-IcalCalendar -AsString

    #Deserializer tested in Ical.Net no need to test further here.
    $calendar.ProductId | Should -Be "-//xyz Corp//NONSGML PDA Calendar Version 1.0//EN"
    $calendar.Version | Should -Be  "2.0"
    
    }
     It 'Should load a multipartcalender' {

    $calendar = "$PSScriptRoot/multicalendar.ics" | Import-IcalCalendar 

    $calendar.Count | Should -Be 3

    #Deserializer tested in Ical.Net no need to test further here.
    $calendar[0].ProductId | Should -Be "-//xyz Corp//NONSGML PDA Calendar Version 1.0//EN"
    $calendar[0].Version | Should -Be  "2.0"
    
    }
}