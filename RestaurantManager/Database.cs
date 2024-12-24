class Database{
    public List<User> userData = new List<User>();


    public void SaveUser(User user){
        userData.Add(user);
    }
    public User GetUser(string username)
    {
        foreach(User user in userData)
        {
            if(user.username == username){
                return user;
            }
        }
        return null;
    }
}