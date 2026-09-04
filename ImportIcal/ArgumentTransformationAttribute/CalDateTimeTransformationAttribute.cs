using Ical.Net.DataTypes;
using System.Management.Automation;

namespace ImportIcal.ArgumentTransformationAttribute
{
    public class CalDateTimeTransformationAttribute : System.Management.Automation.ArgumentTransformationAttribute
    {
        public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
        {

            if (inputData is PSObject)
            {
                inputData = ((PSObject)inputData).BaseObject;
            }

            if (inputData is CalDateTime)
            {
                return inputData;
            }

            if (inputData is DateTime)
            {
                var dt = (DateTime)inputData;
                if (dt.Kind == DateTimeKind.Local)
                {
                    engineIntrinsics.Host.UI.WriteWarningLine("DateTimeKind.Local is not specified.  Assuming Unspecified.");
                    dt = new DateTime(dt.Ticks, DateTimeKind.Unspecified);
                }
                return new CalDateTime(dt, true);
            }

            if (inputData is DateOnly)
            {
                return new CalDateTime((DateOnly)inputData);
            }

            throw new InvalidOperationException($"Unexpected Parameter Type. {inputData.GetType().ToString()}");
        }
    } 
}