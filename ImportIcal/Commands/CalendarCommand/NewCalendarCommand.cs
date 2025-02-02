using Ical.Net;
using Ical.Net.CalendarComponents;
using System.Management.Automation;

namespace ImportIcal.Commands.CalendarCommand
{
    // https://www.rfc-editor.org/rfc/rfc2445#section-4.4
    [Cmdlet("New", "Calendar", DefaultParameterSetName = UseLocalTimeZoneSet)]
    public class NewCalendarCommand : PSCmdlet
    {
        // TODO should be manditory or preset by this App
        // https://www.rfc-editor.org/rfc/rfc2445#section-4.7.3
        // e.g. //ABC Corporation//NONSGML My Product//EN
        // Default to this App?
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = UseLocalTimeZoneSet)]
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = UseProvidedTimeZoneSet)]
        public string? ProductId { get; set; }

        // TODO should be manditory or preset by this App
        // https://www.rfc-editor.org/rfc/rfc2445#section-4.7.4
        // Default 2.0, need to see what it handles it
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = UseLocalTimeZoneSet)]
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = UseProvidedTimeZoneSet)]
        public string? Version { get; set; }

        //https://www.rfc-editor.org/rfc/rfc2445#section-4.1.1
        //Default "GREGORIAN"
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = UseLocalTimeZoneSet)]
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = UseProvidedTimeZoneSet)]
        public string? Scale { get; set; }

        // Todo check if this is copied into the content type.
        //https://www.rfc-editor.org/rfc/rfc2445#section-4.7.2
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = UseLocalTimeZoneSet)]
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = UseProvidedTimeZoneSet)]
        public Method? Method { get; set; }

        //https://www.rfc-editor.org/rfc/rfc2445#section-4.2.19
        // TODO see if this is applied to all events automatically or not?
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = UseProvidedTimeZoneSet)]
        public VTimeZone[] VTimeZones { get; set; }

        //Might be more convenient to accept as string.  Would like to provide an enumation of accetable strings see TzdbDateTimeZoneSource
        //Validation set?
        //public string VTimeZoneId { get; set; } = TzdbDateTimeZoneSource;

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = UseLocalTimeZoneSet)]
        public SwitchParameter UseLocalTimeZone { get; set; } = true;

        private const string UseLocalTimeZoneSet = "UseLocalTimeZoneSet";
        private const string UseProvidedTimeZoneSet = "UseProvidedTimeZoneSet";

        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == UseLocalTimeZoneSet && UseLocalTimeZone.ToBool())
                VTimeZones = new VTimeZone[] { VTimeZone.FromLocalTimeZone() };
            var calendar = new Calendar();
            if (! string.IsNullOrEmpty(Version))
                calendar.Version = Version;

            if (!string.IsNullOrEmpty(ProductId))
                calendar.ProductId = ProductId;

            if (!string.IsNullOrEmpty(Scale))
                calendar.Scale = Scale;

            if (Method is not null)
                calendar.Method = Method.ToString();

            if(VTimeZones is not null && VTimeZones.Count() > 0)
                foreach (VTimeZone tz in VTimeZones)
                    calendar.AddTimeZone(tz);

            WriteObject(calendar);

            base.ProcessRecord();
        }
    }
}