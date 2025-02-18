﻿using Ical.Net;
using Ical.Net.CalendarComponents;
using System.Management.Automation;

namespace ImportIcal.Commands.EventCommand
{
    [Cmdlet("Add", "Event", DefaultParameterSetName = Sets.DateEndSet)]
    public class AddEventCommand : EventCommand
    {
        [ValidateNotNull]
        [Parameter(Mandatory = true)]
        public Calendar? Calendar { get; set; }

        [Parameter()]
        public SwitchParameter Passthru { get; set; }

        protected override void ProcessRecord()
        {

            var evt = Calendar?.Create<CalendarEvent>();

            if (evt != null)
                SetEvent(evt);

            base.ProcessRecord();
        }

        protected override void EndProcessing()
        {
            if (Passthru) WriteObject(Calendar);
            base.EndProcessing();
        }
    }
}