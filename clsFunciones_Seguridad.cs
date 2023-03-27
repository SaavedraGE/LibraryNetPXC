using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace lbyParaleXcode
{
    public class clsFunciones_Seguridad
    {
        private string sClave_Autorizacion = "T8H2yZUKWpMvUxLOvFoiweri3423400234ksdf";
        private string sToken_Generation = "A63WKOyZbdAq2woJZEiu";

        public string Generar_GUID()
        {
            int longitud = 15;
            const string alfabeto = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$";
            StringBuilder token = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < longitud; i++)
            {
                int indice = rnd.Next(alfabeto.Length);
                token.Append(alfabeto[indice]);
            }
            Guid objGuid = Guid.NewGuid();
            string sResultado = objGuid.ToString() + token.ToString();
            return sResultado;
        }

        public string Encriptar_Valores(object objInput)
        {
            string sResultado = string.Empty;
            try
            {
                byte[] llave;
                byte[] arreglo = UTF8Encoding.UTF8.GetBytes(objInput.ToString());
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(sClave_Autorizacion));
                md5.Clear();
                TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
                tripledes.Key = llave;
                tripledes.Mode = CipherMode.ECB;
                tripledes.Padding = PaddingMode.PKCS7;
                ICryptoTransform convertir = tripledes.CreateEncryptor();
                byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
                tripledes.Clear();
                sResultado = Convert.ToBase64String(resultado, 0, resultado.Length);
            }
            catch (Exception)
            {
                sResultado = "-1";
            }

            return sResultado;
        }

        public string Desencriptar_Valores(object objInput)
        {
            string sResultado = string.Empty;
            try
            {
                byte[] llave;
                byte[] arreglo = Convert.FromBase64String(objInput.ToString());
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(sClave_Autorizacion));
                md5.Clear();
                TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
                tripledes.Key = llave;
                tripledes.Mode = CipherMode.ECB;
                tripledes.Padding = PaddingMode.PKCS7;
                ICryptoTransform convertir = tripledes.CreateDecryptor();
                byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
                tripledes.Clear();
                sResultado = UTF8Encoding.UTF8.GetString(resultado);
            }
            catch (Exception)
            {
                sResultado = "-1";
            }
            return sResultado;
        }
    }
}
