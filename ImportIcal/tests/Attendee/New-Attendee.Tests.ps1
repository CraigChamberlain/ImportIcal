#Requires -Module Pester

Describe 'New-IcalAttendee' {
    It 'Given no parameters' {
        $Attendee = New-IcalAttendee -Value "mailto:bob@domain.com"

        $Attendee.CommonName | Should-BeNull
        $Attendee.Rsvp | Should-BeFalse
        $Attendee.Role | Should-Be  "REQ-PARTICIPANT"
        $Attendee.Value | Should-Be  "mailto:bob@domain.com"
    }
    It 'Given Trigger all params' {
        $Attendee = 
            New-IcalAttendee `
                -CommonName "Bob" `
                -ExpectRsvp `
                -Role "CHAIR" `
                -Value "MAILTO:bob@domain.com"
        
        $Attendee.CommonName | Should-Be "Bob"
        $Attendee.Rsvp | Should-BeTrue
        $Attendee.Role | Should-Be  "CHAIR"
        $Attendee.Value | Should-Be  "MAILTO:bob@domain.com"
    }
    

}