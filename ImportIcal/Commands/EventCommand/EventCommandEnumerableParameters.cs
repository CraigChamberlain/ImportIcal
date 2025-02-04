using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Globalization;
using System.Management.Automation;
using System.Resources;
using System.Xml.Linq;

//https://www.rfc-editor.org/rfc/rfc2445#section-4.6.1
namespace ImportIcal.Commands.EventCommand
{
    public abstract partial class EventCommand : PSCmdlet
    {
        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.1
        // e.g.
        // ATTACH:CID:jsmith.part3.960817T083000.xyzMail@host1.com
        // ATTACH;FMTTYPE=application/postscript:ftp://xyzCorp.com/pub/reports/r-960812.ps

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Attachment[]? Attachments { get; set; }

        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.1
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Attendee[]? Attendees { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.2
        //CATEGORIES:APPOINTMENT,EDUCATION
        public IEnumerable<string>? Categories { get; private set; }

        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.4
        // TODO should I use [] or IEnumerable? IEnumerable I gues if it works in tests.
        // Todo Should we be using private set?  Always did set.
        // Todo What about language postfix, is optional, not part of the construct.
        // Does it get appended in serialization

        public IEnumerable<string>? Comments { get; private set; }


        // https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.2
        // e.g.
        // CONTACT:Jim Dolittle\, ABC Industries\, +1-919-555-1234
        ///CONTACT; ALTREP="ldap://host.com:6666/o=3DABC%20Industries\, c=3DUS??(cn=3DBJim%20Dolittle)":Jim Dolittle\, ABC Industries\, +1-919-555-1234
        ///CONTACT; ALTREP="CID=<part3.msg970930T083000SILVER@host.com>":Jim Dolittle\, ABC Industries\, +1-919-555-1234
        ///CONTACT;ALTREP="http://host.com/pdi/jdoe.vcf":Jim Dolittle\, ABC Industries\, +1-919-555-1234
        public IEnumerable<string>? Contacts { get; private set; }


        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.5.1

        public IEnumerable<PeriodList>? ExceptionDates { get; private set; }

        // https://www.rfc-editor.org/rfc/rfc2445#section-4.8.5.2
        public IEnumerable<RecurrencePattern>? ExceptionRules { get; private set; }


        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.8.2
        // TODO test conversion from string?
        // REQUEST-STATUS:2.0;Success
        // REQUEST-STATUS:3.1;Invalid property value;DTSTART:96-Apr-01
        // REQUEST-STATUS:2.8; Success\, repeating event ignored.Scheduled as a single event.; RRULE:FREQ=WEEKLY\; INTERVAL=2
        // REQUEST-STATUS:4.1; Event conflict. Date/time is busy.
        // REQUEST-STATUS:3.7; Invalid calendar user; ATTENDEE: MAILTO:jsmith @host.com
        public RequestStatus[]? RequestStatuses { get; private set; }

        //https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.5
        // typically UID of parent
        // TODO can this be sorted by adding child?
        public IEnumerable<string>? RelatedComponents { get; private set; }

        // https://www.rfc-editor.org/rfc/rfc2445#section-4.8.1.10
        // EASEL,PROJECTOR,VCR
        public string[]? Resources { get; private set; }

        // https://www.rfc-editor.org/rfc/rfc2445#section-4.8.5.3
        public IEnumerable<PeriodList>? RecurrenceDates { get; private set; }

        // https://www.rfc-editor.org/rfc/rfc2445#section-4.8.5.4
        public IEnumerable<RecurrencePattern>? RecurrenceRules { get; private set; }

        //Xprop?
        // TODO understand seems to be set elsewhere.
        //public IEnumerable<ICalendarProperty>? Properties { get; private set; }

        
        public static void AddEach<T>(IList<T> list, IEnumerable<T>? toAdd) { 
            if (toAdd is not null)
                foreach (var item in toAdd)
                {
                    list.Add(item);
                }
        }

        public void AttachEnumerableParameters(CalendarEvent evt) {

            AddEach(evt.Attachments, Attachments);
            AddEach(evt.Attendees, Attendees);
            AddEach(evt.Categories, Categories);
            AddEach(evt.Comments, Comments);
            AddEach(evt.Contacts, Contacts);
            AddEach(evt.ExceptionDates, ExceptionDates);
            AddEach(evt.ExceptionRules, ExceptionRules);
            AddEach(evt.RequestStatuses, RequestStatuses);
            AddEach(evt.RelatedComponents, RelatedComponents);
            AddEach(evt.Resources, Resources);
            AddEach(evt.RecurrenceDates, RecurrenceDates);
            AddEach(evt.RecurrenceRules, RecurrenceRules);

        }


    }
}