using Ical.Net.CalendarComponents;
using System.Management.Automation;

namespace ImportIcal.Commands.EventCommand
{
    [Cmdlet("New", "Event")]
    public class NewEventCommand : EventCommand
    {
        protected override void ProcessRecord()
        {
            var evt = new CalendarEvent();

            SetEvent(evt);
            WriteObject(evt);

            base.ProcessRecord();
        }
    }
}