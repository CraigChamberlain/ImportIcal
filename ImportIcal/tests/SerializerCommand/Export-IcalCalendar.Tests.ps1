#Requires -Module Pester

Describe 'Export-IcalCalendar' {
    It 'Given a blank Calendar object.' {
        $stream = [Ical.Net.Calendar]::new() | Export-IcalCalendar -AsStream 
        
        $stream | Should -BeOfType System.IO.MemoryStream
        $stream.Position = 0
        $reader = [System.IO.StreamReader]::new($stream)
        $reader.ReadLine() | Should -Be "BEGIN:VCALENDAR"
        $reader.Dispose()
        $stream.Dispose()
    }
    It 'Given a blank Calendar object -AsString.' {
        $string = [Ical.Net.Calendar]::new() | Export-IcalCalendar -AsString
        $string | Should -BeOfType string
        $string | Should -BeLike "BEGIN:VCALENDAR*"
        
    }
    It 'Given a blank Calendar object File based Export.' {
        $path =  "$PSScriptRoot/Export.ics"
        {[Ical.Net.Calendar]::new() | Export-IcalCalendar -Path $path} | Should -Throw 
        {[Ical.Net.Calendar]::new() | Export-IcalCalendar -Path $path -Force} | Should -Not -Throw 
        [Ical.Net.Calendar]::new() | Export-IcalCalendar -Path $path -Force | Should -BeNullOrEmpty
        
        $path | Should -Exist

        Get-Content $path | Select-Object -First 1 | Should -BeLike "BEGIN:VCALENDAR*"

    }

}