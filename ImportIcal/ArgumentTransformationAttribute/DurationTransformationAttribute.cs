using Ical.Net.DataTypes;
using System.Management.Automation;

namespace ImportIcal.ArgumentTransformationAttribute
{
    public class DurationTransformationAttribute : System.Management.Automation.ArgumentTransformationAttribute
    {
        public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
        {

            if (inputData is PSObject)
            {
                inputData = ((PSObject)inputData).BaseObject;
            }

            if (inputData is Duration)
            {
                return inputData;
            }

            if (inputData is TimeSpan)
            {
                return Duration.FromTimeSpanExact((TimeSpan)inputData);
            }

            throw new InvalidOperationException($"Unexpected Parameter Type. {inputData.GetType().ToString()}");
        }
    } 
}