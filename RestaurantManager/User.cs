

class User
{
    public string username;
    private string _password;
    private bool _isAdmin;

    public User(string username, string password, bool isAdmin = false)
    {
        this.username = username;
        _password = password;
        _isAdmin = isAdmin;
    }

    public static void Register(string username, string password,bool isAdmin, Database database)
    {
       User user = new User(username,password,isAdmin);
       database.SaveUser(user);
    }


    public bool Login(Database database)
    {
        bool userFound = Authenticator.ValidateUser(database.userData,username,_password);
        return userFound;
    }


}