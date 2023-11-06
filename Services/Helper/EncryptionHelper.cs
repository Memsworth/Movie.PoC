namespace Services.Helper
{
    public static class EncryptionHelper
    {
        public static bool VerifyPassword(string userInput, string hashedInput) => BCrypt.Net.BCrypt.Verify(userInput, hashedInput);
        public static string GeneratePassword(string userInput) => BCrypt.Net.BCrypt.HashPassword(userInput);
    }
}
