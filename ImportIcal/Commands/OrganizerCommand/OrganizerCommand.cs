using Ical.Net.DataTypes;
using System.Management.Automation;

//https://www.rfc-editor.org/rfc/rfc2445#section-4.8.4.3
namespace ImportIcal.Commands.OrganizerCommand
{
    public abstract class OrganizerCommand : PSCmdlet
    {
        // TODO add links to spec
        // To do add examples
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Uri? SentBy { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? CommonName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Uri? DirectoryEntry { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string? Language { get; set; }

        protected void SetOrganizer(Organizer org)
        {
            if (CommonName is not null)
                org.CommonName = CommonName;
            if (SentBy is not null)
                org.SentBy = SentBy;
            if (Language is not null)
                org.Language = Language;
            if (DirectoryEntry is not null)
                org.DirectoryEntry = DirectoryEntry;
        
        }


    }
}