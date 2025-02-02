# ImportIcal
Module containing Cmdlts to create and assemble objects from the [Ical.Net](https://github.com/ical-org/ical.net) library.

I have made a variety of parameter sets in an attempt to make it easy to assemble a valid Calender object in reference to the specification [rfc2445](https://www.rfc-editor.org/rfc/rfc2445).

This project is in its early stage and does not yet include all of the object types as I decide how I want to structure and test the project and also learn how to interpret the specification.

It is already possible to Assemble a calendar with events and alarms (reminders).

Serialisation/De-Serialisation will follow Shortly.

##Install##

````pwsh
Install-Module ImportIcal
````

##Examples##

Please see the [tests directory](./tree/master/ImportIcal/tests) for an extensive set of examples in the Pester files:

````pwsh
$calendar = New-IcalCalendar `
        -ProductId "//ABC Corporation//NONSGML My Product//EN" `
        -Version "2.0"`
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
            Attachments = $Filename
            Subject = "Some $Variable1 Subject"
            Start = Get-Date -Year 2025 -Month 12 -Day 1 
        }
    } |
    Add-IcalEvent -Calendar $calendar 
````
      
