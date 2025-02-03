#Requires -Module Pester

Describe 'New-IcalCalendar' {
    It 'Given no parameters, it should not throw and use LocalTime for TzId.' {
        $calender = New-IcalCalendar


        $calender.Scale | Should -BeNullOrEmpty
        $calender.Method | Should -BeNullOrEmpty

        $calender.TimeZones.Count | Should -Be 1
        
        $calender.TimeZones[0].Id | Should -Be ([Ical.Net.CalendarComponents.VTimeZone]::FromLocalTimeZone().Id)
    }

    It 'Given parameters, values appear in object.' {
        $calender = New-IcalCalendar `
                -Scale "GREGORIAN" `
                -Method "REQUEST" `
                -VTimeZones "America/New_York","Africa/Abidjan"

        $calender.Scale | Should -Be "GREGORIAN"
        $calender.Method.ToString() | Should -Be "REQUEST"

        $calender.TimeZones.Count | Should -Be 2
        $calender.TimeZones.TzId | Should -Contain "Africa/Abidjan"
        $calender.TimeZones.TzId | Should -Contain "America/New_York"
    }

    It 'Given the UseLocalTimeZone:$false parameter only, all parameters should be empty.' {
        $calender = New-IcalCalendar -UseLocalTimeZone:$false

        $calender.Scale | Should -BeNullOrEmpty
        $calender.Method | Should -BeNullOrEmpty

        $calender.TimeZones | Should -BeNullOrEmpty
    }

}
