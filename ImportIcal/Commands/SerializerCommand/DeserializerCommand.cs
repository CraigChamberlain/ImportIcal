using Ical.Net;
using Ical.Net.Serialization;
using System.Management.Automation;
using System.Text;

namespace ImportIcal.Commands.SerialisarCommand
{
    [Cmdlet("Import", "Calendar")]
    public class DeSerializerCommand : PSCmdlet
    {

        [Parameter(ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true)]
        public Stream? Stream { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Encoding? Encoding { get; set; }

        // TODO accept file name in place of stream
        // Should it have force to overwrite existing?
        // Should extension be guessed as iCal?

        protected override void ProcessRecord()
        {
            //TODO
            var cal = Calendar.Load(Stream);
            WriteObject(cal);
            base.ProcessRecord();
        }


    }

}