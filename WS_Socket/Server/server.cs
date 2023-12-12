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
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 46154);
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
        try
        {
            string urlToSend = "https://github.com/RitterAntoine/CESI-A3-ProgSys-Prosit-7-WS";
            byte[] data = Encoding.UTF8.GetBytes(urlToSend);
            client.Send(data);

            Console.WriteLine("URL envoyée au client : " + urlToSend);

            // Ajouter une attente pour maintenir la connexion ouverte pendant un certain temps
            System.Threading.Thread.Sleep(5000);  // Attendez 5 secondes (ajustez selon vos besoins)
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors de l'envoi de l'URL : " + ex.Message);
        }
    }


    private static void Deconnecter(Socket socket)
    {
        socket.Close();
    }
}
