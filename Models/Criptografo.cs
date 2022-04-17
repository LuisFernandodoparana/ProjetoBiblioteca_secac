using System.Security.Cryptography;
using System.Text;
namespace Biblioteca
{
    public class Criptografo
    {
        public static string TextoCriptografado(string textoSemFormatacao)
        {
            MD5 MD5Hasher = MD5.Create();
            // criando um novo objeto encripitado 

            byte[] By = Encoding.Default.GetBytes(textoSemFormatacao);
            byte[] bytesCritografo = MD5Hasher.ComputeHash(By);
            // esta fazendo a conversão da senha para md5
            StringBuilder SB = new StringBuilder();
            // esta contruindo a nova senha encrittada

            foreach (byte b in bytesCritografo)
            {
                string DebugB = b.ToString("x2");
                SB.Append(DebugB);
            } 
            return SB.ToString();
            // retornando a senha encriptada
        }
    }
    
}