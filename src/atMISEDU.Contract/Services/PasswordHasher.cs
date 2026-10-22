using atMISEDU.Contract.Abstractions;
using BCrypt.Net;
using BCrypt1 = BCrypt.Net.BCrypt;

namespace atMISEDU.Contract.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Using <see cref="BCrypt"/> to hash password
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <returns>Hashed password</returns>
        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return string.Empty;
            return BCrypt1.EnhancedHashPassword(password, HashType.SHA384);
        }

        /// <summary>
        /// Using <see cref="BCrypt"/> to verify password
        /// </summary>
        /// <param name="password">Password to verify</param>
        /// <param name="hashedPassword">Hashed password use for verify</param>
        /// <returns>True if hashed password is hashed from password, otherwise False</returns>
        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword)) return false;

            if (BCrypt1.EnhancedVerify(password, hashedPassword))
                return true;

            return BCrypt1.Verify(password, hashedPassword);
        }
    }
}