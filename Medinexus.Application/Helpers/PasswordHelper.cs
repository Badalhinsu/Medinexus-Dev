namespace Medinexus.API.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            // WorkFactor 12 is a good balance between security & performance
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
