using Ical.Net;
using System.Management.Automation;
using System.Text;

namespace ImportIcal.Commands.SerialisarCommand
{
    [Cmdlet("Import", "Calendar",DefaultParameterSetName = Sets.File)]
    public class DeSerializerCommand : PSCmdlet
    {

        [ValidateNotNull]
        [Parameter(ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true, ParameterSetName = Sets.Stream)]
        public Stream? Stream { get; set; }

        [ValidateNotNull]
        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = Sets.Stream)]
        public Encoding? Encoding { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true, ParameterSetName = Sets.File)]
        public string? Path { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true, ParameterSetName = Sets.String)]
        public string? String { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, ParameterSetName = Sets.String)]
        public SwitchParameter AsString { get; set; }

        protected override void ProcessRecord()
        {
            CalendarCollection? calendars = null;
            if (ParameterSetName == Sets.Stream && Stream != null)
            {
                var streamreader = Encoding is null ? new StreamReader(Stream) : new StreamReader(Stream, Encoding);
                calendars = CalendarCollection.Load(streamreader);
            }
            if (ParameterSetName == Sets.File && Path != null)
            {
                var reader = File.OpenText(Path);
                calendars = CalendarCollection.Load(reader);
            }
            if (ParameterSetName == Sets.String && String != null)
            {
                calendars = CalendarCollection.Load(String);
            }
            if (calendars is not null)
                WriteObject(calendars,true);
            base.ProcessRecord();
        }

        private static class Sets {

            public const string Stream = "Stream";
            public const string String = "String";
            public const string File = "File";
        }


    }

}