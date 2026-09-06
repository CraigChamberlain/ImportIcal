using Ical.Net.DataTypes;
using System.Diagnostics.Eventing.Reader;
using System.Management.Automation;

namespace ImportIcal.ArgumentTransformationAttribute
{
    public class TriggerTransformationAttribute : System.Management.Automation.ArgumentTransformationAttribute
    {
        public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
        {

            if (inputData is PSObject)
            {
                inputData = ((PSObject)inputData).BaseObject;
            }
            if (inputData is string)
            {
                var s = (string)inputData;
                int parsedInputData;
                if (int.TryParse(s , out parsedInputData)) {
                    inputData = parsedInputData;
                }
                else {
                    engineIntrinsics.Host.UI.WriteWarningLine("Will return blank object.  Parsing by string not supported in current Ical.Net Version.");
                    return new Trigger((string)inputData);
                }
            }

            if (inputData is Trigger)
            {
                return inputData;
            }

            if (inputData is Duration)
            {
                return new Trigger((Duration)inputData);
            }
            if (inputData is TimeSpan)
            {
                var ts = (TimeSpan)inputData;
                var duration = Duration.FromTimeSpanExact(ts);
                return new Trigger(duration);
            }
            if (inputData is int)
            {
                return new Trigger(Duration.FromMinutes((int)inputData));
              
            }
            

            throw new InvalidOperationException($"Unexpected Parameter Type. {inputData.GetType().ToString()}");
        }
    } 
}