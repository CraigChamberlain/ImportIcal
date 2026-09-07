#Requires -Module Pester

Describe 'New-IcalEvent' {
    It 'Given all single parameters' {
        # TODO -AllDay

        $evt = New-IcalEvent `
            -Class "PUBLIC" `
            -Created  ([Ical.Net.DataTypes.CalDateTime]::new(2000, 1, 1)) `
            -Description "Description text." `
            -Start ([DateOnly]::new(2000,1,2)) `
            -GeographicLocation ([Ical.Net.DataTypes.GeographicLocation]::new("37.386013","-122.082932")) `
            -LastModified (Get-Date -Month 1 -Year 2000 -Day 3 -Hour 0 -Min 0 -Second 0) `
            -Location "SomeLocation" `
            -Organizer  "bob@example.com" `
            -Priority 2 `
            -Sequence 3 `
            -Status "TENTATIVE" `
            -Summary "Summary Text" `
            -Transparency "OPAQUE" `
            -RecurrenceId (Get-Date -Month 1 -Year 2000 -Day 4 -Hour 0 -Min 0 -Second 0) `
            -Url "https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.6" `
            -End (Get-Date -Month 1 -Year 2000 -Day 6 -Hour 0 -Min 0 -Second 0) 

            # TODO -Duration 100000

        $evt.Class | Should -Be "PUBLIC" 
        $evt.Created.Year | Should -Be 2000
        $evt.Created.Month | Should -Be 1
        $evt.Created.Day | Should -Be 1
        $evt.Created.HasTime | Should -Be $false
        $evt.Created.Hour | Should -Be 0
        
        $evt.Description | Should -Be  "Description text." 
        $evt.Start | Should -Be ([Ical.Net.DataTypes.CalDateTime]::new(2000,1, 2))
        $evt.GeographicLocation | Should -Be "37.386013;-122.082932" 
        $evt.LastModified | Should -Be  ([Ical.Net.DataTypes.CalDateTime]::new(2000,1, 3,0,0,0)) 
        $evt.Location | Should -Be "SomeLocation" 
        $evt.Organizer.Value | Should -Be "mailto:bob@example.com" 
        $evt.Priority | Should -Be 2 
        $evt.Sequence | Should -Be  3 
        $evt.Status | Should -Be "TENTATIVE" 
        $evt.Summary | Should -Be "Summary Text" 
        $evt.Transparency | Should -Be "OPAQUE" 
        $evt.RecurrenceId | Should -Be ([Ical.Net.DataTypes.CalDateTime]::new(2000,1,4,0,0,0)) 
        $evt.Url | Should -Be "https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.6" 
        $evt.End.Day | Should -Be 6 

        # TODO 4 days between start and end, End should not be present when Duration is.  May get caught in Serialization?
        $evt.Duration | Should -BeNullOrEmpty
        
        $evt.Attachments.Count | Should -Be 0
        $evt.Attendees.Count | Should -Be 0
        $evt.Categories.Count | Should -Be 0
        $evt.Comments.Count | Should -Be 0
        $evt.Contacts.Count | Should -Be 0
        $evt.ExceptionDates.GetAllDates() | Should -BeNullOrEmpty
        $evt.ExceptionRules.Count | Should -Be 0
        $evt.RequestStatuses.Count | Should -Be 0
        $evt.RelatedComponents.Count | Should -Be 0
        $evt.Resources.Count | Should -Be 0
        $evt.RecurrenceDates.GetAllDates() | Should -BeNullOrEmpty
        $evt.RecurrenceRules.Count | Should -Be 0
    }
     It 'Given minimum parameters' {

        $evt = New-IcalEvent 

        $evt.Class | Should -BeNullOrEmpty 
        $evt.Created | Should -BeNullOrEmpty
        $evt.Description | Should -BeNullOrEmpty
        $evt.Start | Should -BeNullOrEmpty
        $evt.GeographicLocation | Should -BeNullOrEmpty
        $evt.LastModified | Should -BeNullOrEmpty 
        $evt.Location | Should -BeNullOrEmpty
        $evt.Organizer.Value | Should -BeNullOrEmpty 
        
        # Default value in spec and for int
        $evt.Priority | Should -Be 0 

        # TODO defaults to 0 because it's an int.  What is spec default?
        $evt.Sequence | Should -Be 0

        $evt.Status | Should -BeNullOrEmpty 
        $evt.Summary | Should -BeNullOrEmpty 
        $evt.Transparency | Should -BeNullOrEmpty
        $evt.RecurrenceId | Should -BeNullOrEmpty 
        $evt.Url | Should -BeNullOrEmpty
        $evt.End.Day | Should -BeNullOrEmpty
        $evt.Duration | Should -BeNullOrEmpty

        $evt.Attachments.Count | Should -Be 0
        $evt.Attendees.Count | Should -Be 0
        $evt.Categories.Count | Should -Be 0
        $evt.Comments.Count | Should -Be 0
        $evt.Contacts.Count | Should -Be 0
        $evt.ExceptionDates.GetAllDates() | Should -BeNullOrEmpty
        $evt.ExceptionRules.Count | Should -Be 0
        $evt.RequestStatuses.Count | Should -Be 0
        $evt.RelatedComponents.Count | Should -Be 0
        $evt.Resources.Count | Should -Be 0
        $evt.RecurrenceDates.GetAllDates() | Should -BeNullOrEmpty
        $evt.RecurrenceRules.Count | Should -Be 0
    }
    It 'Given -Duration parameter as Timespan' {

        $evt = New-IcalEvent -Duration ([Timespan]::new(1,2,3))
        $evt.Duration.Seconds | Should -Be 3
        $evt.Duration.Minutes | Should -Be 2
        $evt.Duration.Hours | Should -Be 1
        $evt.Start | Should -BeNullOrEmpty
        $evt.End | Should -BeNullOrEmpty
    }
    It 'Given -Duration parameter as Duration' {

        $evt = New-IcalEvent -Duration ([Ical.Net.DataTypes.Duration]::new(1,2,3,4,5))
        $evt.Duration.Seconds | Should -Be 5
        $evt.Duration.Minutes | Should -Be 4
        $evt.Duration.Hours | Should -Be 3
        $evt.Duration.Days | Should -Be 2
        $evt.Duration.Weeks | Should -Be 1
        $evt.Start | Should -BeNullOrEmpty
        $evt.End | Should -BeNullOrEmpty
    }
    It 'Given -Duration, -End and parameters' {

        {New-IcalEvent `
            -Duration ([Ical.Net.DataTypes.Duration]::new(1,2,3,4,5)) `
            -End (Get-Date -Month 1 -Year 2000 -Day 6) } | 
            Should-Throw -ExceptionMessage "Parameter set cannot be resolved using the specified named parameters. One or more parameters issued cannot be used together or an insufficient number of parameters were provided."
    }
    It 'Given -Duration -Start parameters' {

        $evt = 
            New-IcalEvent `
                -Duration ([Timespan]::new(3456000000000)) `
                -Start (Get-Date -Month 1 -Year 2000 -Day 2) 

        $evt.Duration.Hours | Should -Be 96
        $evt.Start.Day | Should -Be 2
        #TODO End and Duration linked is this causing any issue on Serialize
        $evt.End | Should -BeNullOrEmpty

    }

    It 'Given -GeographicLocation parameter as array' {

        $evt = 
            New-IcalEvent `
                -GeographicLocation 12,13

        $evt.GeographicLocation.Latitude | Should -Be 12.0
        $evt.GeographicLocation.Longitude | Should -Be 13.0

    }
    
    Write-Warning "Test for parsing from string should hopefully fail when Ical.Net fixes bug."

    It 'Given -GeographicLocation parameter as string pair' {

        $evt = 
            New-IcalEvent `
                -GeographicLocation "37.386013;-122.082932"

        $evt.GeographicLocation.Latitude | Should -Be 0 #37.386013
        $evt.GeographicLocation.Longitude | Should -Be 0 #-122.082932

    }

    It 'Given Organizer parameter as simple string' {

        $evt = 
            New-IcalEvent `
                -Organizer "bob@example.com"

        $evt.Organizer.Value | Should-Be "MAILTO:bob@example.com"
        $evt.Organizer.CommonName | Should-BeNull

    }
    It 'Given Organizer parameter as complex string' {

        #TODO better way of making an organiser?
        $evt = 
            New-IcalEvent `
                -Organizer "Bob <bob@example.com>"

        $evt.Organizer.Value | Should-BeNull # Would be good if this worked "MAILTO:bob@example.com"
        $evt.Organizer.CommonName | Should-BeNull # "Bob"
        
    }

}