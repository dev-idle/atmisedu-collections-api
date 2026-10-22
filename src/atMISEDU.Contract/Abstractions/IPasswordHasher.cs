namespace atMISEDU.Contract.Abstractions
{
    /// <summary>
    /// Provide service hashing for password
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="password">Pass to hash</param>
        /// <returns>Hashed password</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verify password 
        /// </summary>
        /// <param name="password">Password to verify</param>
        /// <param name="hashedPassword">Hashed password to verify with</param>
        /// <returns>True if hashed password is hashed from password, otherwise False</returns>
        bool VerifyPassword(string password, string hashedPassword);
    }
}