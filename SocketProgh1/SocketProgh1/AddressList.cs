using System.Net;

public class AddressList
{
    public static List<User> Users = new();

    public AddressList()
    {
        Users.Add(new User() { Name = "Mikkel ",Address  = IPAddress.Parse("192.168.1.2")});

    }


    public class User
    {
        public string Name { get; set; }
        public IPAddress? Address { get; set; }
    }


}