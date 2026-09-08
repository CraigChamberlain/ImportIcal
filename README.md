# ImportIcal
Module containing Cmdlets to create and assemble objects from the [Ical.Net](https://github.com/ical-org/ical.net) library.

I have made a variety of parameter sets in an attempt to make it easy to assemble a valid Calender object in reference to the specification [rfc2445](https://www.rfc-editor.org/rfc/rfc2445).
However, it remains very possible to create invalid objects and you may need to read the above rfc and test extensively to see that results are represented in your target calendar clients.

This project is in its early stage and does not yet include all of the object types as I decide how I want to structure and test the project and also learn how to interpret the specification.

It is already possible to Assemble a calendar or import an existing from file, string or stream. 

To his events may be added.  To events, one can add alarms (reminders) attendees.  The resulting Calendar can then be Exported to a file, string or stream.

I will aim to improve useability help documentation before adding many more features.  Feel free to add an issue if you want to see a particular feature set.  I think TODO lists may be the next item to look at but I've no needed yet.

I have used this to assemble meeting requests for emailing using [SendMail](https://github.com/CraigChamberlain/SendMail) and itterate over events, writing them into a spreadsheet.


![Net Build](https://github.com/CraigChamberlain/ImportIcal/actions/workflows/dotnet.yml/badge.svg)


## Install

````pwsh
Install-Module ImportIcal
````

## List available commands

````pwsh
Get-Commands -Module ImportIcal
````

## Examples

Please see the [tests directory](https://github.com/CraigChamberlain/ImportIcal/tree/master/ImportIcal/tests) for an extensive set of examples in the Pester files:

### Populate a Calendar with Arbitary data from a pipeline and save to an .ics file.

````pwsh
$Calendar = New-IcalCalendar `
        -Scale "GREGORIAN" `
        -Method "REQUEST" `
        -VTimeZones "America/New_York","Africa/Abidjan"

Get-SomeData |
    For-EachObject {
        $name = $_.SomeProperty | Select-Object $SomeTransformation
        $OtherVarialbe = $_.SomeData | ConvertTo-Html

        [pscustomobject]@{
            AttendeeEmail = "some.person@example.com"
            AttendeeName = $name
            Description = Get-Body -Arg1 $OtherVariable
            Subject = "Some $Variable1 Subject"
            Start = Get-Date -Year 2025 -Month 12 -Day 1 -Hour 12 -Min 0
            End =  Get-Date -Year 2025 -Month 12 -Day 1 -Hour 13 -Min 30
        }
    } |
    Add-IcalEvent -Calendar $Calendar 

$Calendar | Export-IcalCalendar -Path example.ical
````

### Add Events, Attendees, Alarms, and Organizers

````pwsh
    
    $Evt = 
            New-IcalEvent `
                -Start $Start `
                -End $End `
                -Description $Description `
                -Location  $Location `
                -Summary  $Title `
                -Organizer (
                    New-IcalOrganizer `
                        -Value "MAILTO:$OrganiserEmail" `
                        -CommonName $OrganiserName
                    )

    $Calendar.Events.Add($Evt)   
    
    # Create an attendee
    $Attendee = 
        New-IcalAttendee `
            -CommonName $AttendeeName `
            -ExpectRsvp `
            -Value "MAILTO:$AttendeeEmail"

    $evt.Attendees.Add($Attendee)

    # Create a reminder 1h before the event
    Add-IcalAlarm -Trigger -60 -AlarmAction "Display" -Event $evt
````     


### Import and Iterate over events

````pwsh
    $Calendar = Import-IcalCalendar -path $Example.ics
    $Calendar.Events |
        Select-Object Start, End, Description, Location, Summary |
        Export-Excel  -path $Example.xlsx
````     
