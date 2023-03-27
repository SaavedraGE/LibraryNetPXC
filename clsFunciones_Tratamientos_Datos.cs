using lbyParaleXcode.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace lbyParaleXcode
{
    public class clsFunciones_Tratamientos_Datos
    {
        public decimal? Obtener_Parte_Decimal(string incomingValue)
        {
            decimal val;
            string sEntero = string.Empty;
            if (incomingValue.Contains(",") || incomingValue.Contains("."))
            {
                if (!decimal.TryParse(incomingValue.Replace(",", "").Replace(".", ""), NumberStyles.Number, CultureInfo.InvariantCulture, out val))
                    return null;
                return val / 100;
            }
            else
            {
                return Convert.ToDecimal(incomingValue);
            }
        }

        public clsAfiliado_DNI_CUIL Obtener_DNI_CUIL(string sValor)
        {
            clsAfiliado_DNI_CUIL objADC = new clsAfiliado_DNI_CUIL();
            objADC.sCUIL = sValor.Trim();
            objADC.sDNI = sValor.Substring(2, 9);
            objADC.sDNI = objADC.sDNI.Remove(objADC.sDNI.Length - 1);
            return objADC;
        }


    }
}
