using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using ImportIcal.ArgumentTransformationAttribute;
using System.Management.Automation;

namespace ImportIcal.Commands.AlarmCommand
{
    //https://www.rfc-editor.org/info/rfc2445/#section-4.6.6
    public abstract class AlarmCommand : PSCmdlet
    {
        //https://www.rfc-editor.org/info/rfc2445/#section-4.8.6.1
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public Enum.AlarmAction AlarmAction { get; set; } // AlarmAction.Display;

        // https://www.rfc-editor.org/info/rfc2445/#section-4.8.6.3
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        [TriggerTransformation]
        public Trigger? Trigger { get; set; }

        // https://www.rfc-editor.org/info/rfc2445/#section-4.8.6.2
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int Repeat { get; set; } = 0;

        protected Alarm CreateAlarm()
        {
            var alarm = new Alarm();
            alarm.Action = AlarmAction.ToString();
            alarm.Trigger = Trigger;
            alarm.Repeat = Repeat;

            return alarm;
        }


    }
}