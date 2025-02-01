using Ical.Net.CalendarComponents;
using System.Management.Automation;

namespace ImportIcal.Commands.AlarmCommand
{
    [Cmdlet("New", "IcalAlarm")]
    public class NewAlarmCommand : AlarmCommand
    {
        protected override void ProcessRecord()
        {

            WriteObject(CreateAlarm());

            base.ProcessRecord();
        }
    }
}