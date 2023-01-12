using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    IPAddress serverIP = IPAddress.Parse("192.168.2.2");
    
    public void StartClient()
    {
        Console.CursorLeft = 0;
        Console.Write("Input name: ");
        string? name = Console.ReadLine();

        int i = 0;
        foreach (var user in AddressList.Users)
        {
            Console.CursorLeft = 0;
            Console.WriteLine($"{i++} Name: {user.Name} IP: {user.Address} ");
        }

        int c = int.Parse(Console.ReadKey().KeyChar.ToString());
        serverIP = AddressList.Users[c].Address;

        while (true)
        {
            string input = GetInput();
            ExecuteClient(name + "|" + input, serverIP);
        }
    }

    public string? GetInput()
    {
        Console.CursorLeft = 0; 
        Console.Write("Msg: ");
        return Console.ReadLine();
    }

    public void ExecuteClient(string? msg, IPAddress serverIP)
    {
        IPEndPoint serverEndPoint = new IPEndPoint(serverIP, 11111);
        Socket socket = new Socket(serverIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(serverEndPoint);

        Console.CursorLeft = 0;
        Console.WriteLine($"Socket connected to -> {socket.RemoteEndPoint}");

        byte[] messageSent = Encoding.ASCII.GetBytes(msg + "<EOM>");
        socket.Send(messageSent);

        byte[] messageReceived = new byte[1024];

        int byteRecv = socket.Receive(messageReceived);
        string msgServer = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);
        Console.CursorLeft = 0;
        Console.WriteLine($"Message from Server -> {msgServer}");

        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}