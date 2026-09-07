using Ical.Net.DataTypes;
using System.Management.Automation;

namespace ImportIcal.Commands.AttendeeCommand
{
    //https://www.rfc-editor.org/info/rfc2445/#section-4.8.4.1
    public abstract class AttendeeCommand : PSCmdlet
    {
        //https://www.rfc-editor.org/info/rfc2445/#section-4.2.16

        //CHAIR         ,    // Indicates chair of the calendar entity
        //  REQ-PARTICIPANT ,   //Indicates a participant whose participation is required
        //OPT-PARTICIPANT,    //Indicates a participant whose participation is optional
        //NON-PARTICIPANT     //Indicates a participant who is copied for information purposes only
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Role { get; set; } = "REQ-PARTICIPANT";

        // https://www.rfc-editor.org/info/rfc2445/#section-4.2.2
        // E.G. John Smith.
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public String? CommonName { get; set; }

        // https://www.rfc-editor.org/info/rfc2445/#section-4.2.12
        //"NEEDS-ACTION"        ; To-do needs action
        //"ACCEPTED"            ; To-do accepted
        //"DECLINED"            ; To-do declined
        // "TENTATIVE"          ; To-do tentatively accepted
        // "DELEGATED"          ; To-do delegated
        // "COMPLETED"          ; To-do completed.  COMPLETED property has date/time completed.
        // "IN-PROCESS"         ; To-do in process of being completed
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string ParticipationStatus { get; set; } = "NEEDS-ACTION";

        // https://www.rfc-editor.org/info/rfc2445/#section-4.2.17
        // RSVP Expectation
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public SwitchParameter ExpectRsvp { get; set; }

        // https://www.rfc-editor.org/info/rfc2445/#section-4.8.4.1
        // E.G. MAILTO:Bob@domain.com
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public Uri Value { get; set; } = null!;

        protected Attendee CreateAttendee()
        {
            var attendee = new Attendee();
            attendee.Role = Role;
            attendee.CommonName = CommonName;
            attendee.Rsvp = ExpectRsvp.IsPresent;
            attendee.Value = Value;
            attendee.ParticipationStatus = ParticipationStatus;

            return attendee;
        }


    }
}