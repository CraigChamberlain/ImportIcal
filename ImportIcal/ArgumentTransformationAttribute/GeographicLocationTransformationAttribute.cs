using Ical.Net.DataTypes;
using System.Management.Automation;

namespace ImportIcal.ArgumentTransformationAttribute
{
    public class GeographicLocationAttribute : System.Management.Automation.ArgumentTransformationAttribute
    {
        public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
        {

            if (inputData is PSObject)
            {
                inputData = ((PSObject)inputData).BaseObject;
            }

            if (inputData is GeographicLocation)
            {
                return inputData;
            }

            if (inputData is object[])
            {

                var pair = (object[])inputData;
                double x, y;
                if (pair.Length == 2)
                {
                    x = Convert.ToDouble(pair[0]);
                    y = Convert.ToDouble(pair[1]);
                    return new GeographicLocation(x, y);

                } 
            }
            if (inputData is string)
            {
                engineIntrinsics.Host.UI.WriteWarningLine("Will return blank object.  Parsing by string not supported in current Ical.Net Version.");
                return new GeographicLocation((string)inputData);
            }

            throw new InvalidOperationException($"Unexpected Parameter Type. {inputData.GetType().ToString()}");
        }
    } 
}