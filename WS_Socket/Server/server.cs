using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static IPEndPoint clientep;

    static void Main()
    {
        Socket serverSocket = SeConnecter();
        Socket clientSocket = AccepterConnexion(serverSocket);
        EcouterReseau(clientSocket);
        Deconnecter(serverSocket);
    }

    private static Socket SeConnecter()
    {
        Console.OutputEncoding = Encoding.UTF8;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 46154);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind(ipep);
        socket.Listen(10);
        Console.WriteLine("Le serveur est en attente d'une connexion...");
        return socket;
    }

    private static Socket AccepterConnexion(Socket socket)
    {
        Socket client = socket.Accept();
        clientep = (IPEndPoint)client.RemoteEndPoint;
        Console.WriteLine("Connexion acceptée : " + clientep.Address + " port " + clientep.Port);
        return client;
    }

    private static void EcouterReseau(Socket client)
    {
        byte[] data = new byte[1024];
        int size = client.Receive(data);
        string message = Encoding.UTF8.GetString(data, 0, size);
        Console.WriteLine("Message reçu = " + message);
    }

    private static void Deconnecter(Socket socket)
    {
        socket.Close();
    }
}
