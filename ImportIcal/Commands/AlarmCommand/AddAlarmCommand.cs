using Ical.Net.CalendarComponents;
using System.Management.Automation;

namespace ImportIcal.Commands.AlarmCommand
{
    [Cmdlet("Add", "Alarm")]
    public class AddAlarmCommand : AlarmCommand
    {
        [Parameter(Mandatory = true)]
        public CalendarEvent? Event { get; set; }

        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {

            var alarm = CreateAlarm();
            Event?.Alarms.Add(alarm);

            base.ProcessRecord();
        }

        protected override void EndProcessing()
        {
            if (Passthru) WriteObject(Event);
            base.EndProcessing();
        }
    }
}