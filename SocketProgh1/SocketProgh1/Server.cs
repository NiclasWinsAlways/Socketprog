using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    public void ExecuteServer()
    {
        IPAddress serverIP = IPAddress.Parse("192.168.2.2");
        IPEndPoint serverEndPoint = new IPEndPoint(serverIP, 11111);
        Socket socket = new Socket(serverIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(serverEndPoint);
        socket.Listen(10);

        while (true)
        {
            Console.CursorLeft = 50;
            Console.WriteLine("Waiting connection on " + serverEndPoint);
            Socket clientSocket = socket.Accept();
            byte[] bytes = new Byte[1024];
            string? data = null;

            while (true)
            {
                int numByte = clientSocket.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, numByte);
                if (data.IndexOf("<EOM>") > -1) break;
            }
            Console.CursorLeft = 50;
            Console.WriteLine($"Text received -> {data} from {clientSocket.RemoteEndPoint}" );
            byte[] message = Encoding.ASCII.GetBytes("Test Server");

            clientSocket.Send(message);

            IPAddress clientReturnIP = ((IPEndPoint)clientSocket.RemoteEndPoint).Address;
            
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();

           // new Client().ExecuteClient("Testing return answer", clientReturnIP);
        }
    }
}