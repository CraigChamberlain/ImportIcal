#Requires -Module Pester

Describe 'New-IcalEvent' {
    It 'Given all single parameters' {
        # TODO -AllDay
        # New-IcalEvent -
        

        # TODO Geo Type accelerator take a obj[] if 2x numeric, construct.  ";" seperator not seeming to work.  try in c#?

        $evt = New-IcalEvent `
            -Class "PUBLIC" `
            -Created  ([Ical.Net.DataTypes.CalDateTime]::new(2000, 1, 1)) `
            -Description "Description text." `
            -Start (Get-Date -Month 1 -Year 2000 -Day 2) `
            -GeographicLocation ([Ical.Net.DataTypes.GeographicLocation]::new("37.386013","-122.082932")) `
            -LastModified (Get-Date -Month 1 -Year 2000 -Day 3) `
            -Location "SomeLocation" `
            -Organizer  "bob@example.com" `
            -Priority 2 `
            -Sequence 3 `
            -Status "TENTATIVE" `
            -Summary "Summary Text" `
            -Transparency "OPAQUE" `
            -RecurrenceId (Get-Date -Month 1 -Year 2000 -Day 4) `
            -Url "https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.6" `
            -End (Get-Date -Month 1 -Year 2000 -Day 6) 

            # TODO -Duration 100000

        $evt.Class | Should -Be "PUBLIC" 
        $evt.Created.Year | Should -Be 2000
        $evt.Created.Month | Should -Be 1
        $evt.Created.Day | Should -Be 1

        # TODO look at time only, datetime conversion seems to be fine
        # There is a Has time property. Not clear how to make it just time, or just date.
        #$evt.Created.Hour | Should -BeNullOrEmpty
        
        $evt.Description | Should -Be  "Description text." 
        $evt.Start | Should -Be ([Ical.Net.DataTypes.CalDateTime](Get-Date -Month 1 -Year 2000 -Day 2)) 
        $evt.GeographicLocation | Should -Be "37.386013;-122.082932" 
        $evt.LastModified | Should -Be  ([Ical.Net.DataTypes.CalDateTime](Get-Date -Month 1 -Year 2000 -Day 3)) 
        $evt.Location | Should -Be "SomeLocation" 
        $evt.Organizer.Value | Should -Be "mailto:bob@example.com" 
        $evt.Priority | Should -Be 2 
        $evt.Sequence | Should -Be  3 
        $evt.Status | Should -Be "TENTATIVE" 
        $evt.Summary | Should -Be "Summary Text" 
        $evt.Transparency | Should -Be "OPAQUE" 
        $evt.RecurrenceId | Should -Be ([Ical.Net.DataTypes.CalDateTime](Get-Date -Month 1 -Year 2000 -Day 4)) 
        $evt.Url | Should -Be "https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.6" 
        $evt.End.Day | Should -Be 6 

        # TODO 4 days between start and end, End should not be present when Duration is.  May get caught in Serialization?
        $evt.Duration | Should -Be ([timespan]::new(4,0,0,0))
        $evt.Attachments.Count | Should -Be 0
        $evt.Attendees.Count | Should -Be 0
        $evt.Categories.Count | Should -Be 0
        $evt.Comments.Count | Should -Be 0
        $evt.Contacts.Count | Should -Be 0
        $evt.ExceptionDates.Count | Should -Be 0
        $evt.ExceptionRules.Count | Should -Be 0
        $evt.RequestStatuses.Count | Should -Be 0
        $evt.RelatedComponents.Count | Should -Be 0
        $evt.Resources.Count | Should -Be 0
        $evt.RecurrenceDates.Count | Should -Be 0
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

        # TODO Default of type?
        $evt.Duration | Should -Be ([Timespan]::new(0))

        $evt.Attachments.Count | Should -Be 0
        $evt.Attendees.Count | Should -Be 0
        $evt.Categories.Count | Should -Be 0
        $evt.Comments.Count | Should -Be 0
        $evt.Contacts.Count | Should -Be 0
        $evt.ExceptionDates.Count | Should -Be 0
        $evt.ExceptionRules.Count | Should -Be 0
        $evt.RequestStatuses.Count | Should -Be 0
        $evt.RelatedComponents.Count | Should -Be 0
        $evt.Resources.Count | Should -Be 0
        $evt.RecurrenceDates.Count | Should -Be 0
        $evt.RecurrenceRules.Count | Should -Be 0
    }
    It 'Given -Duration parameter' {

        $evt = New-IcalEvent -Duration 10
        $evt.Duration | Should -Be ([Timespan]::new(10))
        $evt.Start | Should -BeNullOrEmpty
        $evt.End | Should -BeNullOrEmpty
    }
    It 'Given -Duration, -End and parameters' {

        {New-IcalEvent `
            -Duration 10 `
            -End (Get-Date -Month 1 -Year 2000 -Day 6) } | 
            Should -Throw
    }
    It 'Given -Duration -Start parameters' {

        $evt = 
            New-IcalEvent `
                -Duration 3456000000000 `
                -Start (Get-Date -Month 1 -Year 2000 -Day 2) 

        $evt.Duration | Should -Be ([Timespan]::new(3456000000000))
        $evt.Start.Day | Should -Be 2
        #TODO End and Duration linked is this causing any issue on Serialize
        $evt.End.Day | Should -Be 6

    }
}