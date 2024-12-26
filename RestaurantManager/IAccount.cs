/// <summary>
/// An interface to ensure each account type has a definition for certain Functions.
/// </summary>
interface IAccount
{
  static void Register(string username, string password, Database database)
  {
    new DataSerializer<Database>(database).SaveData();
  }
}