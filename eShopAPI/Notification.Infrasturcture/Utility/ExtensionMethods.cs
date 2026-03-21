
namespace Notification.Infrasturcture.Utility
{
    public static class ExtensionMethods
    {
        public static string Decrypt(this string cipherText, string encryptionKey)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(encryptionKey))
            {
                throw new ArgumentException("Cipher text and encryption key must be provided.");
            }
            try
            {
                // Implement your decryption logic here using the encryptionKey
                // This is a placeholder for demonstration purposes
                // In a real implementation, you would use a proper decryption algorithm
                // For example, if you are using AES encryption, you would use the Aes class from System
            }
            catch (Exception ex)
            {
                // Handle exceptions that may occur during decryption
                throw new InvalidOperationException("An error occurred during decryption.", ex);
            }

            return string.Empty; // Return the decrypted text
        }

        public static string Encrypt(this string cipherText, string encryptionKey)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(encryptionKey))
            {
                throw new ArgumentException("Cipher text and encryption key must be provided.");
            }
            try
            {
                // Implement your decryption logic here using the encryptionKey
                // This is a placeholder for demonstration purposes
                // In a real implementation, you would use a proper decryption algorithm
                // For example, if you are using AES encryption, you would use the Aes class from System
            }
            catch (Exception ex)
            {
                // Handle exceptions that may occur during decryption
                throw new InvalidOperationException("An error occurred during decryption.", ex);
            }

            return string.Empty; // Return the decrypted text
        }
    }
}
