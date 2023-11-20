namespace Services.Helper
{
    public static class EncryptionHelper
    {
        public static string GeneratePassword(string userInput) => BCrypt.Net.BCrypt.HashPassword(userInput);
        //public static bool VerifyPassword(string userInput, string hashedInput) => BCrypt.Net.BCrypt.Verify(userInput, hashedInput);

    }
}
