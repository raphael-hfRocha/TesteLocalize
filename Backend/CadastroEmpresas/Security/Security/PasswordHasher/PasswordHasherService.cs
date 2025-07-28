using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Security.PasswordHasher
{
    public class PasswordHasherService
    {
        /// <summary>
        /// Gera um hash seguro da senha usando SHA256 com salt
        /// </summary>
        /// <param name="senha">Senha em texto plano</param>
        /// <returns>Hash da senha com salt</returns>
        public string HashPassword(string senha)
        {
            // Gerar um salt aleatório
            byte[] salt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Combinar senha com salt e gerar hash
            byte[] senhaBytes = Encoding.UTF8.GetBytes(senha);
            byte[] senhaComSalt = new byte[senhaBytes.Length + salt.Length];

            Array.Copy(senhaBytes, 0, senhaComSalt, 0, senhaBytes.Length);
            Array.Copy(salt, 0, senhaComSalt, senhaBytes.Length, salt.Length);

            // Gerar hash SHA256
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(senhaComSalt);

                // Combinar salt + hash para armazenar
                byte[] hashComSalt = new byte[salt.Length + hash.Length];
                Array.Copy(salt, 0, hashComSalt, 0, salt.Length);
                Array.Copy(hash, 0, hashComSalt, salt.Length, hash.Length);

                return Convert.ToBase64String(hashComSalt);
            }
        }
    }
}
