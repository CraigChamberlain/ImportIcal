using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System.Management.Automation;

namespace ImportIcal.Commands.OrganizerCommand
{
    [Cmdlet("New", "Organizer")]
    public class NewOrganizerCommand : OrganizerCommand
    {
        protected override void ProcessRecord()
        {
            var org = new Organizer();

            SetOrganizer(org);
            WriteObject(org);

            base.ProcessRecord();
        }
    }
}