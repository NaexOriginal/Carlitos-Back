using System.Security.Cryptography;
using System.Text;

namespace Carlitos5G.Commons;
public static class SHA256Helper
{
    // Método estático para calcular el hash SHA256 de una cadena
    public static string ComputeSHA256Hash(string input)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));  // Convertir cada byte a un valor hexadecimal
            }
            return builder.ToString();  // Retorna el hash como una cadena de 64 caracteres hexadecimales
        }
    }
}
