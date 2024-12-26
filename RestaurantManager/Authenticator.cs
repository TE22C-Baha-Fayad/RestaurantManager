using System.Security.Cryptography;
using System.Text;

/// <summary>
/// An authenticator class to Manage Security Tasks
/// </summary>
class Authenticator
{
    /// <summary>
    /// Validates an account in list of accounts.
    /// </summary>
    /// <param name="users"></param>
    /// <param name="username"></param>
    /// <param name="passwordTxt"></param>
    /// <returns>Account if an account is found and null if the account isn't found.</returns>
    public static Account ValidateUser(List<Account> users, string username, string passwordTxt)
    {
        //look for each account in users
        foreach (Account account in users)
        {
            //if username and passwordhash matches the input
            if (account.Username == username && HashPassword(passwordTxt) == account.PasswordHash)
            {
                //return the account that is found
                return account;
            }
        }
        //else if no account is found in the account list; return null
        return null;
    }
    /// <summary>
    /// A basic function that converts a string to Hash with SHA256 hashing algorithm.
    /// </summary>
    /// <param name="password"></param>
    /// <returns>Hash string</returns> <summary>
    /// 
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Verifies if the input string is equal to a hash
    /// </summary>
    /// <param name="input"></param>
    /// <param name="hash"></param>
    /// <returns>True if they are equal and False if not equal.</returns> <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="hash"></param>
    /// <returns></returns>
    public static bool VerifyPassword(string input, string hash)
    {

        // Hash the input.
        var hashOfInput = HashPassword(input);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        return comparer.Compare(hashOfInput, hash) == 0;
    }

}