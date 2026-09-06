#Requires -Module Pester

Describe 'New-IcalAlarm' {
    It 'Given no parameters, it should throw.' {
        #{ New-IcalAlarm } Should-Throw
    }
    It 'Given Trigger as Timespan' {
        $Actual = 
            New-IcalAlarm `
                -AlarmAction DISPLAY `
                -Trigger ([TimeSpan]::new(-1, 0, 0))
        
        $Actual.Trigger.Duration.Hours | Should-Be -1
        $Actual.Trigger.Duration.Minutes | Should-BeNull
        $Actual.Trigger.Duration.Seconds | Should-BeNull
        $Actual.Trigger.Related | Should-Be "Start"
        $Actual.Action | Should-Be "DISPLAY"
    }
    It 'Given Trigger as int' {
        $Actual = 
            New-IcalAlarm `
                -AlarmAction DISPLAY `
                -Trigger ([int](-5))
        
        $Actual.Trigger.Duration.Hours | Should-BeNull
        $Actual.Trigger.Duration.Minutes | Should-Be -5
        $Actual.Trigger.Duration.Seconds | Should-BeNull
        $Actual.Trigger.Related | Should-Be "Start"
        $Actual.Action | Should-Be "DISPLAY"
    }
    It 'Given Trigger as negative int (can be interpretted as a string)' {
        $Actual = 
            New-IcalAlarm `
                -AlarmAction DISPLAY `
                -Trigger -15
        
        $Actual.Trigger.Duration.Hours | Should-BeNull
        $Actual.Trigger.Duration.Minutes | Should-Be -15
        $Actual.Trigger.Duration.Seconds | Should-BeNull
        $Actual.Trigger.Related | Should-Be "Start"
        $Actual.Action | Should-Be "DISPLAY"
    }
    It 'Given Trigger as Duration' {
        $Actual = 
            New-IcalAlarm `
                -AlarmAction DISPLAY `
                -Trigger ([Ical.Net.DataTypes.Duration]::FromHours(5))          
        
        $Actual.Trigger.Duration.Hours | Should-Be 5
        $Actual.Trigger.Duration.Minutes | Should-BeNull
        $Actual.Trigger.Duration.Seconds | Should-BeNull
        $Actual.Trigger.Related | Should-Be "Start"
        $Actual.Action | Should-Be "DISPLAY"
    }
    It 'Given Trigger as Trigger' {
        $Actual = 
            New-IcalAlarm `
                -AlarmAction DISPLAY `
                -Trigger ([Ical.Net.DataTypes.Trigger]::new([Ical.Net.DataTypes.Duration]::FromHours(5)))          
        
        $Actual.Trigger.Duration.Hours | Should-Be 5
        $Actual.Trigger.Duration.Minutes | Should-BeNull
        $Actual.Trigger.Duration.Seconds | Should-BeNull
        $Actual.Trigger.Related | Should-Be "Start"
        $Actual.Action | Should-Be "DISPLAY"
    }

    Write-Warning "Test for parsing from string should start to fail when Ical.Net fixes bug."
    It 'Given no -Duration as String' {
        $Actual = 
            New-IcalAlarm `
                -AlarmAction DISPLAY `
                -Trigger "RELATED=END:P5M"
        
        $Actual.Trigger.Duration.Hours | Should-BeNull
        $Actual.Trigger.Duration.Minutes | Should-BeNull
        $Actual.Trigger.Duration.Seconds | Should-BeNull
        $Actual.Trigger.Related | Should-Be "Start"
        $Actual.Action | Should-Be "DISPLAY"
    }
    
}