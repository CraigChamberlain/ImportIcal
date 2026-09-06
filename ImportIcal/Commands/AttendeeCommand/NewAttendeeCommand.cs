using System.Management.Automation;

namespace ImportIcal.Commands.AttendeeCommand
{
    [Cmdlet("New", "Attendee")]
    public class NewAttendeeCommand : AttendeeCommand
    {
        protected override void ProcessRecord()
        {

            WriteObject(CreateAttendee());

            base.ProcessRecord();
        }
    }
}