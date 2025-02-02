#Requires -Module Pester

Describe 'Export-IcalCalendar' {
    It 'Given a blank Calendar object.' {
        $stream = [Ical.Net.Calendar]::new() | Export-IcalCalendar
        $stream | Should -BeOfType System.IO.MemoryStream

        #TODO read some lines
    }
}