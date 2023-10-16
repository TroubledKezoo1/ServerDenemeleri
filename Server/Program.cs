using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public TcpClient TcpClient { get; set; }
    public NetworkStream Stream { get; set; }
    public Lobby Lobby { get; set; }

    public User(int id, string userName, TcpClient tcpClient)
    {
        Id = id;
        UserName = userName;
        TcpClient = tcpClient;
        Stream = tcpClient.GetStream();
        Lobby = null;
    }
}

public class Lobby
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; }

    public Lobby(int id, string name)
    {
        Id = id;
        Name = name;
        Users = new List<User>();
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void RemoveUser(User user)
    {
        Users.Remove(user);
    }

    public void BroadcastMessage(User sender, string message)
    {
        foreach (var user in Users)
        {
            if (user != sender)
            {
                SendMessage(user, $"{sender.UserName}: {message}");
            }
        }
    }

    private void SendMessage(User user, string message)
    {
        try
        {
            NetworkStream stream = user.TcpClient.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(message + "\n");
            stream.Write(data, 0, data.Length);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending message to user: " + ex.Message);
        }
    }
}

public class Server
{
    private List<Lobby> lobbies;
    private TcpListener listener;
    private int nextLobbyId = 1;
    private int nextUserId = 1;
    private Dictionary<string, Lobby> lobbiesByName;

    public Server(string ipAddress, int port)
    {
        lobbies = new List<Lobby>();
        listener = new TcpListener(IPAddress.Parse(ipAddress), port);
        lobbiesByName = new Dictionary<string, Lobby>();
    }

    public void Start()
    {
        listener.Start();
        Console.WriteLine($"Server started at IP address: {((IPEndPoint)listener.LocalEndpoint).Address}, port: {((IPEndPoint)listener.LocalEndpoint).Port}");
        Console.WriteLine("Waiting for connections...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Thread clientThread = new Thread(() => HandleClient(client));
            clientThread.Start();
        }
    }
    private Lobby GetOrCreateLobbyByName(string lobbyName)
    {
        if (lobbiesByName.ContainsKey(lobbyName))
        {
            return lobbiesByName[lobbyName];
        }
        else
        {
            Lobby lobby = new Lobby(nextLobbyId++, lobbyName);
            lobbiesByName[lobbyName] = lobby;
            return lobby;
        }
    }

    private void HandleClient(TcpClient client)
    {
        User user = null;

        try
        {
            Console.WriteLine("Client connected: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address);
            user = new User(nextUserId++, "User" + nextUserId, client);

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                if (user.Lobby == null)
                {
                    string[] parts = message.Split(':');
                    if (parts.Length != 2 || parts[0] != "JOIN_LOBBY")
                    {
                        Console.WriteLine("Invalid message format. Expected: JOIN_LOBBY:LobbyName");
                        continue;
                    }

                    string lobbyName = parts[1];
                    Lobby lobby = GetOrCreateLobbyByName(lobbyName);

                    lobby.AddUser(user);
                    user.Lobby = lobby;
                    Console.WriteLine($"{user.UserName} joined Lobby {lobby.Name}");
                }
                else
                {
                    // User is in a lobby, broadcast the message to that lobby
                    user.Lobby.BroadcastMessage(user, message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Client disconnected: " + user?.UserName + ", Error: " + ex.Message);
            if (user != null && user.Lobby != null)
                user.Lobby.RemoveUser(user);
        }
        finally
        {
            client.Close();
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter server IP address: ");
        string serverIp = Console.ReadLine();
        Console.Write("Enter server port: ");
        if (!int.TryParse(Console.ReadLine(), out int serverPort))
        {
            Console.WriteLine("Invalid port. Exiting.");
            return;
        }
        Server server = new Server(serverIp, serverPort);
        server.Start();
    }
}
