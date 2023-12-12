using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        Socket clientSocket = SeConnecter();
        EcouterReseau(clientSocket);
        Deconnecter(clientSocket);
    }

    private static Socket SeConnecter()
    {
        Console.OutputEncoding = Encoding.UTF8;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 46154);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            socket.Connect(ipep);
            Console.WriteLine("Connecté au serveur sur " + ipep.Address + " port " + ipep.Port);
            return socket;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur de connexion au serveur : " + ex.Message);
            return null;
        }
    }

    private static void EcouterReseau(Socket client)
    {
        try
        {
            string message = "Hello, serveur!";
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data);

            Program();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors de l'écoute du réseau : " + ex.Message);
        }
    }

    private static void Deconnecter(Socket socket)
    {
        try
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors de la déconnexion : " + ex.Message);
        }
    }

    private static void Program()
    {
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(i);
        }
    }
}
