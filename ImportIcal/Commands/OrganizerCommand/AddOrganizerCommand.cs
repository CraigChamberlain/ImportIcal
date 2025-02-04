using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System.Management.Automation;

namespace ImportIcal.Commands.OrganizerCommand
{
    [Cmdlet("Add", "Organizer")]
    public class AddOrganizerCommand : OrganizerCommand
    {
        [ValidateNotNull]
        [Parameter(Mandatory = true)]
        public CalendarEvent? Event { get; set; }

        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {
            var org = new Organizer();

            if (Event is not null)
            {
                SetOrganizer(org);
                Event.Organizer = org;
            }

            if (Passthru) WriteObject(Event);

            base.ProcessRecord();
        }

    }
}