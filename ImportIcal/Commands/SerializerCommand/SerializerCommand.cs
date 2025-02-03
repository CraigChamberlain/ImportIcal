using Ical.Net;
using Ical.Net.Serialization;
using System.Management.Automation;


namespace ImportIcal.Commands.SerialisarCommand
{
    [Cmdlet("Export", "Calendar", DefaultParameterSetName = Sets.File)]
    public class SerializerCommand : PSCmdlet
    {
        // TODO readup on lifetime may not be need to be a param for dependency injection.
        private CalendarSerializer? Serializer { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = Sets.Stream)]
        public Stream? Stream { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = Sets.Stream)]
        public SwitchParameter AsStream { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = Sets.String)]
        public SwitchParameter AsString { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = Sets.File)]
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = Sets.Stream)]
        public System.Text.Encoding? Encoding { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true, ValueFromPipeline = true, ParameterSetName = Sets.File)]
        public string? Path { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ParameterSetName = Sets.File)]
        public SwitchParameter Force { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true, ValueFromPipeline = true, ParameterSetName = Sets.Stream)]
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true, ValueFromPipeline = true, ParameterSetName = Sets.String)]
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true, ValueFromPipeline = true, ParameterSetName = Sets.File)]
        public Calendar? Calendar { get; set; }

        protected override void BeginProcessing()
        {
            Serializer = new CalendarSerializer();
            base.BeginProcessing();
        }
        protected override void ProcessRecord()
        {
            if (ParameterSetName == Sets.File)
            {
                if (string.IsNullOrEmpty(Path))
                    throw new Exception("Path may not be null or empty");
                else
                {
                    Stream = Force ? File.OpenWrite(Path) : File.Open(Path, FileMode.CreateNew);
                    Encoding ??= System.Text.Encoding.UTF8;
                    Serializer?.Serialize(Calendar, Stream, Encoding);
                    Stream.Dispose();
                }
            }

            if (ParameterSetName == Sets.Stream)
            {
                Stream ??= new MemoryStream();
                Encoding ??= System.Text.Encoding.UTF8;
                Serializer?.Serialize(Calendar, Stream, Encoding);

                WriteObject(Stream);
            }

            if (ParameterSetName == Sets.String)
            {
                var calendarText = Serializer?.SerializeToString(Calendar);
                    WriteObject(calendarText);
            }

            base.ProcessRecord();
        }

        private static class Sets { 
        
            public const string String = "String";
            public const string Stream = "Stream";
            public const string File   = "File";

        }

    }

}