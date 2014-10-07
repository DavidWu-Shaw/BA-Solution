using System;

namespace General.Utility
{
    public class UnitConvert
    {
        public static decimal ToDecimal(object value)
        {
            Decimal output = 0;

            if (value != null)
            {
                try
                {
                    output = Decimal.Parse(value.ToString());
                }
                catch
                {
                }
            }

            return output;
        }

    }
}
