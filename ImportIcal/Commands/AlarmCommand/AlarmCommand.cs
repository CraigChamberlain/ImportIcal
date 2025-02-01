using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System.Management.Automation;

namespace ImportIcal.Commands.AlarmCommand
{
    public abstract class AlarmCommand : PSCmdlet
    {
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string AlarmAction { get; set; } // AlarmAction.Display;

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public TimeSpan Duration { get; set; }

        protected Alarm CreateAlarm()
        {
            var alarm = new Alarm();
            alarm.Action = AlarmAction;
            alarm.Trigger.Duration = Duration;

            return alarm;
        }


    }
}