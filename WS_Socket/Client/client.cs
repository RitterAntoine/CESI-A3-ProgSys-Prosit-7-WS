using System;
using System.Diagnostics;
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
            byte[] data = new byte[1024];
            int size = client.Receive(data);
            string receivedMessage = Encoding.UTF8.GetString(data, 0, size);

            Console.WriteLine("Message reçu du serveur : " + receivedMessage);

            // Vérifier si le message reçu est une URL
            Uri uriResult;
            bool isUrl = Uri.TryCreate(receivedMessage, UriKind.Absolute, out uriResult) &&
                          (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (isUrl)
            {
                // Lancer le navigateur avec l'URL reçue
                Program(receivedMessage);
            }
            else
            {
                Console.WriteLine("Le message reçu n'est pas une URL valide.");
            }
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

    private static void Program(string url)
    {
        try
        {
            // Spécifiez le chemin complet de l'exécutable du navigateur et l'URL à ouvrir
            string defaultBrowserPath = GetDefaultBrowserPath();

            // Lance le processus du navigateur avec l'URL spécifiée
            Process.Start(defaultBrowserPath, url);
            Console.WriteLine("Default browser path : " + defaultBrowserPath);

            Console.WriteLine("Navigateur lancé avec l'URL : " + url);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors du lancement du navigateur : " + ex.Message);
        }
    }

    private static string GetDefaultBrowserPath()
    {
        // Clé du registre pour le navigateur par défaut sur les systèmes Windows
        const string registryKeyPath = @"HKEY_CLASSES_ROOT\http\shell\open\command";

        // Lecture de la valeur par défaut de la clé du registre
        string defaultBrowserCommand = (string)Microsoft.Win32.Registry.GetValue(registryKeyPath, "", null);

        if (!string.IsNullOrEmpty(defaultBrowserCommand))
        {
            // Extraction du chemin du navigateur depuis la commande du registre
            int exeStartIndex = defaultBrowserCommand.IndexOf("\"") + 1;
            int exeEndIndex = defaultBrowserCommand.IndexOf("\"", exeStartIndex);
            string defaultBrowserPath = defaultBrowserCommand.Substring(exeStartIndex, exeEndIndex - exeStartIndex);

            return defaultBrowserPath;
        }

        return null;
    }
}
