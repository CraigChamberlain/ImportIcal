using Ical.Net.CalendarComponents;
using System.Management.Automation;

namespace ImportIcal.Commands.AttendeeCommand
{
    [Cmdlet("Add", "Attendee")]
    public class AddAttendeeCommand : AttendeeCommand
    {
        [Parameter(Mandatory = true)]
        public CalendarEvent? Event { get; set; }

        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {

            var Attendee = CreateAttendee();
            Event?.Attendees.Add(Attendee);

            base.ProcessRecord();
        }

        protected override void EndProcessing()
        {
            if (Passthru) WriteObject(Event);
            base.EndProcessing();
        }
    }
}