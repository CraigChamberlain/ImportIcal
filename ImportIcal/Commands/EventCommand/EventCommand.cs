using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using ImportIcal.ArgumentTransformationAttribute;
using System.Management.Automation;

//https://www.rfc-editor.org/rfc/rfc2445#section-4.6.1
namespace ImportIcal.Commands.EventCommand
{
    [CmdletBinding(DefaultParameterSetName = Sets.DateEndSet)]
    public abstract partial class EventCommand : PSCmdlet
    {
        //TODO Allday event? start 00 Duration 24H?


        // https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.3
        //  Is it possible to provide sugested values, open ended set?
        // "PUBLIC" / "PRIVATE" / "CONFIDENTIAL" / iana-token / x-name
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? Class { get; set; }

        // https://www.rfc-editor.org/rfc/rfc2445#section-4.8.7.1
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [CalDateTimeTransformation]
        public CalDateTime? Created { get; set; }

        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.5
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? Description { get; set; }

        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.2.4
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [CalDateTimeTransformation]
        public CalDateTime? Start { get; set; }

        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.6
        // e.g.         
        // 37.386013;-122.082932
        // Co-ords.
        // TODO test can be supplied as a pair of strings
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [GeographicLocation]
        public GeographicLocation? GeographicLocation { get; set; }

        //  https://www.rfc-editor.org/rfc/rfc2445#section-4.8.7.3
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [CalDateTimeTransformation]
        public CalDateTime? LastModified { get; set; }

        // https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.7
        /// <summary>
        ///    Example: The following are some examples of this property:
        ///
        ///  LOCATION:Conference Room - F123, Bldg. 002
        /// LOCATION;ALTREP="http://xyzcorp.com/conf-rooms/f123.vcf":
        ///Conference Room - F123, Bldg. 002
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? Location { get; set; }


        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.3
        // TODO is required in some circumstances?
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Organizer? Organizer { get; set; }

        //e.g.
        //0 - No Priority
        //1 - Highest
        //2 - 2nd Highest
        // ...
        //9 - Lowest
        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.9
        [ValidateRange(0, 9)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int? Priority { get; set; } = 0;


        // TODO seems to be autoset
        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc2445#section-4.8.7.2
        /// </summary>
        //[Parameter(ValueFromPipelineByPropertyName = true)]
        //public CalDateTime? DtStamp { get; set; }


        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc2445#section-4.8.7.4
        /// Defaults to 0.  Is incremented on major change so significant change requests can be linked to the version it is attempting to diff.
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int? Sequence { get; set; } = 0;

        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.11
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Enum.EventStatus? Status { get; set; }


        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.12
        /// </summary>
        /// TODO can have language and other components.
        /// 
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? Summary { get; set; }

        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc2445#section-4.8.2.7
        /// </summary>
        /// TODO can have language and other components.
        /// 
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Enum.TimeTransparency? Transparency { get; set; }


        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.4
        /// </summary>
        /// Identify element in a series. Can also be a range?
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Obsolete("'EventCommand.RecurrenceId' is obsolete: ''RecurringComponent.ExceptionRules' is obsolete: 'EXRULE is marked as deprecated in RFC 5545 and will be removed in a future version'.")]
        [CalDateTimeTransformation]
        public CalDateTime? RecurrenceId { get; set; }

        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.6
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Uri? Url { get; set; }


        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.2.2
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = Sets.DateEndSet)]
        [CalDateTimeTransformation]
        public CalDateTime? End { get; set; }

        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.2.5
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = Sets.DurationSet)]
        [DurationTransformation]
        public Duration? Duration { get; set; }

        protected static class Sets
        {
            public const string DateEndSet = "DateEndSet";
            public const string DurationSet = "DurationSet";
        }

        private static void Set<T>(T parameter, Action SetAction)
        {
            if (parameter != null) SetAction.Invoke();
        }
        protected CalendarEvent SetEvent(CalendarEvent evt)
        {
            if (Class != null) evt.Class = Class;
            if (Created != null) evt.Created = Created;
            if (Description != null) evt.Description = Description;
            if (Start != null) evt.Start = Start;
            if (GeographicLocation != null) evt.GeographicLocation = GeographicLocation;
            if (LastModified != null) evt.LastModified = LastModified;
            if (Organizer != null) evt.Organizer = Organizer;
            if (Priority != null) evt.Priority = (int)Priority;
            if (Sequence != null) evt.Sequence = (int)Sequence;
            if (Status != null) evt.Status = Status.ToString();
            if (Summary != null) evt.Summary = Summary;
            if (Transparency != null) evt.Transparency = Transparency.ToString();
            if (Url != null) evt.Url = Url;
            if (RecurrenceId != null) evt.RecurrenceId = RecurrenceId;
            if (Duration != null) evt.Duration = Duration;

            // TODO validate if is Date or DateTime?
            if (End != null) evt.End = End;
            if (Location != null) evt.Location = Location;

            return evt;
        }


    }
}