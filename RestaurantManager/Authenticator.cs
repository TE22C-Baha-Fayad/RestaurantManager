using System.Security.Cryptography;
using System.Text;

class Authenticator
{
    /// <summary>
    /// Validates a user account in list of users.
    /// </summary>
    /// <param name="users"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>true when a user is found or false if not found</returns>
    public static bool ValidateUser(List<User> users, string username, string password)
    {
        foreach (User user in users)
        {
            if (user.username == username && VerifyPassword(password, HashPassword(password)))
            {
                return true;
            }
        }
        return false;
    }
    public static string HashPassword(string password)
    {
        HashAlgorithm hashAlgorithm = SHA256.Create();
        // Convert the input string to a byte array and compute the hash.
        byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        var sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }
    public static bool VerifyPassword(string input, string hash)
    {

        // Hash the input.
        var hashOfInput = HashPassword(input);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        return comparer.Compare(hashOfInput, hash) == 0;
    }

}