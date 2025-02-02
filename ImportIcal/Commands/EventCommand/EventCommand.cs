using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System.Management.Automation;

//https://www.rfc-editor.org/rfc/rfc2445#section-4.6.1
namespace ImportIcal.Commands.EventCommand
{
    public abstract class EventCommand : PSCmdlet
    {
        //TODO Allday event? start 00 Duration 24H?


        //TODO add spec link
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? Class { get; set; }

        // TODO type accelerator? Not that easy to create a noda date in PWSH?
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public IDateTime? Created { get; set; }

        //class / created / description / dtstart / geo / last-mod / location / organizer / priority /
        //dtstamp / seq / status / summary / transp / uid / url / recurid /
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? Description { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        //Duration param?
        //Allday event?
        public CalDateTime? Start { get; set; }
            
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public GeographicLocation? Geo { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public CalDateTime? LastModified { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? Location { get; set; }


        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.3

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Uri? OrganiserEmail { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? OrganiserName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Uri? OrganiserDirectory { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? OrganiserLanguage { get; set; }

        //e.g.
        //0 - No Priority
        //1 - Highest
        //2 - 2nd Highest
        // ...
        //9 - Lowest
        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.9
        [ValidateRange(0, 9)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int Priority { get; set; } = 0;

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
        public int Sequence { get; set; } = 0;

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
        public IDateTime? RecurrenceId { get; set; }

        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.6
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Uri? Url { get; set; }


 
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = DateEndSet)]
        public CalDateTime? End { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = DurationSet)]
        public TimeSpan Duration { get; set; }




        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.1
        //TODO Should accept an array of Email & Name or Have another pair of commands for adding/attaching attendees, perhaps allow single in here as a convenience?
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? AttendeeEmail { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? AttendeeName { get; set; }

        //Must only have one.
        private const string DateEndSet = "DateEndSet";
        private const string DurationSet = "DurationSet";

        //Many
        //attach / attendee / categories / comment / contact / exdate / exrule / rstatus / related / resources / rdate / rrule / x-prop

        protected CalendarEvent SetEvent(CalendarEvent evt)
        {
            evt.Class = Class;
            evt.Created = Created;
            evt.Description = Description;
            evt.Start = Start;
            evt.GeographicLocation = Geo;
            evt.LastModified = LastModified;

            var org = new Organizer();
            org.CommonName = OrganiserName;
            org.SentBy = OrganiserEmail;
            org.Language = OrganiserLanguage;
            org.DirectoryEntry = OrganiserDirectory;
            evt.Organizer = org;

            evt.Priority = Priority;

            //TODO auto created, why different from Created?  It's a compulsary item?
            //evt.DtStamp = DtStamp;

            evt.Sequence = Sequence;
            evt.Status = Status.ToString();
            evt.Summary = Summary;
            evt.Transparency = Transparency.ToString();

            //TODO Is autocreated, is this best left alone?
            //evt.Uid = Uid;
            evt.Url = Url;
            evt.RecurrenceId = RecurrenceId;




            evt.Duration = Duration;
            evt.End = End; //# This also sets the duration TODO - condider adding a duration param to commandlet?
            
            evt.Location = Location;

            _ = evt.Attachments;
            _ = evt.Categories;
            _ = evt.Comments;
            _ = evt.Contacts;
            _ = evt.ExceptionDates;
            _ = evt.RequestStatuses;
            _ = evt.RelatedComponents;
            _ = evt.Resources;
            _ = evt.RecurrenceDates;
            _ = evt.RecurrenceRules;
            _ = evt.Properties;// is this x-Prop?

            //attach / attendee / categories / comment / contact / exdate / exrule / rstatus / related / resources / rdate / rrule / x-prop


            return evt;
        }


    }
}