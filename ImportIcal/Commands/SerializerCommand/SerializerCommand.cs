using Ical.Net;
using Ical.Net.Serialization;
using System.Management.Automation;


namespace ImportIcal.Commands.SerialisarCommand
{
    [Cmdlet("Export", "Calendar")]
    public class SerializerCommand : PSCmdlet
    {
        public SerializationContext? Context { get; set; }
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public CalendarSerializer? Serializer { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Stream? Stream { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public System.Text.Encoding? Encoding { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true, ValueFromPipeline = true)]
        public Calendar? Calendar { get; set; }

        // TODO accept file name in place of stream
        // Should it have force to overwrite existing?
        // Should extension be guessed as iCal?

        protected override void BeginProcessing()
        {
            // Neither Contect or Serialise seem to need desposing of.
            if (Context is null)
                Context = new SerializationContext();
            if (Serializer is null)
                Serializer = new CalendarSerializer();
            base.BeginProcessing();
        }
        protected override void ProcessRecord()
        {
            if (this.Stream is null)
                Stream = new MemoryStream();

            Encoding ??= System.Text.Encoding.UTF8;

            Serializer?.Serialize(Calendar, this.Stream, Encoding);
            
            // TODO only on passthru? 
            // Default to passthru if stream is created.
            WriteObject(Stream);

            base.ProcessRecord();
        }


    }

}