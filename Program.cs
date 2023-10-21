using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel;

class Server
{
    public class User
    {
        public string Username { get; }
        public bool IsHost { get; }
        public Lobby UserLobby { get; }
        public TcpClient TcpClient { get; set; }


        public User(string username, bool isHost, Lobby lobby)
        {
            Username = username;
            IsHost = isHost;
            UserLobby = lobby;
        }
    }

    public class Lobby
    {
        public string LobbyName { get; }
        public List<User> Users { get; }
        public int MaxPlayers { get; }

        public Lobby(string lobbyName, int maxPlayers)
        {
            LobbyName = lobbyName;
            MaxPlayers = maxPlayers;
            Users = new List<User>();
        }

        public void AddUser(User user)
        {
            if (Users.Count < MaxPlayers)
            {
                Users.Add(user);
            }
            else
            {
                Console.WriteLine("Lobi dolu, kullanıcı eklenemedi.");
            }
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }
        public void SendMessage(User fromUser, string message)
        {
            foreach (User user in Users)
            {
                if (user != fromUser)
                {
                    try
                    {
                        TcpClient userTcpClient = user.TcpClient;
                        NetworkStream userStream = userTcpClient.GetStream();
                        byte[] messageBytes = Encoding.UTF8.GetBytes($"{fromUser.Username} -> {user.Username}: {message}");
                        userStream.Write(messageBytes, 0, messageBytes.Length);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hata oluştu: " + ex.Message);
                    }
                }
            }
        }


    }

    public class ServerInner
    {
        public List<Lobby> lobbies = new List<Lobby>();

        public void CreateLobby(string lobbyName, int maxPlayers)
        {
            Lobby lobby = new Lobby(lobbyName, maxPlayers);
            lobbies.Add(lobby);
        }

        public Lobby GetLobbyByName(string lobbyName)
        {
            return lobbies.FirstOrDefault(lobby => lobby.LobbyName == lobbyName);
        }

        public List<Lobby> ListLobbies()
        {
            return lobbies;
        }

        public void AddUserToLobby(string username, string lobbyName, bool isHost, TcpClient tcpClient)
        {
            Lobby lobby = GetLobbyByName(lobbyName);
            if (lobby != null)
            {
                User user = new User(username, isHost, lobby);
                user.TcpClient = tcpClient;
                lobby.AddUser(user);
            }
        }

        public void HandleMessage(string lobbyName, string username, string message)
        {
            Lobby lobby = GetLobbyByName(lobbyName);
            if (lobby != null)
            {
                User user = lobby.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    lobby.SendMessage(user, message);
                }
            }
        }
    }

    private static TcpListener server;
    private static readonly int port = 8080;

    static void Main(string[] args)
    {
        server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
        server.Start();
        Console.WriteLine($"Sunucu başlatıldı. Port: {port}");

        while (true)
        {
            Console.WriteLine("Bağlantı bekleniyor...");
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Yeni bir istemci bağlandı.");



            Thread clientThread = new Thread(HandleClient);
            clientThread.Start(client);
        }
    }
    public static ServerInner serverInner = new ServerInner();
    static void HandleClient(object clientObj)
    {
        TcpClient client = (TcpClient)clientObj;
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        while (true)
        {
            bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Alınan mesaj: " + message);

                string[] command = message.Split(':');
                if (command.Length >= 2)
                {
                    string action = command[0];
                    string[] parameters = new string[command.Length - 1];
                    for (int i = 0; i < command.Length - 1; i++)
                    {
                        parameters[i] = command[i + 1];
                    }
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        Console.WriteLine(parameters[i]);
                    }
                    switch (action)
                    {
                        case "CREATE_LOBBY":
                            // Lobiyi oluştur.
                            if (parameters.Length == 3)
                            {
                                string lobbyName = parameters[0];
                                int maxPlayers = int.Parse(parameters[1]);
                                string hostUsername = parameters[2];
                                serverInner.CreateLobby(lobbyName, maxPlayers);
                                serverInner.AddUserToLobby(hostUsername, lobbyName, true, client);
                                Console.WriteLine("Lobi oluşturuldu: " + lobbyName);
                            }
                            break;
                        case "JOIN_LOBBY":
                            // Lobiyi katıl.
                            if (parameters.Length == 2)
                            {
                                string lobbyName = parameters[0];
                                string username = parameters[1];
                                serverInner.AddUserToLobby(username, lobbyName, false, client);
                                Console.WriteLine(username + " lobiye katıldı: " + lobbyName);
                            }
                            break;
                        case "LEAVE_LOBBY":
                            // Lobiden ayrıl.
                            if (parameters.Length == 2)
                            {
                                string lobbyName = parameters[0];
                                string username = parameters[1];
                                Lobby lobby = serverInner.GetLobbyByName(lobbyName);
                                if (lobby != null)
                                {
                                    User user = lobby.Users.FirstOrDefault(u => u.Username == username);
                                    if (user != null)
                                    {
                                        lobby.RemoveUser(user);
                                        Console.WriteLine(username + " lobiden ayrıldı: " + lobbyName);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Kullanıcı bulunamadı: " + username);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Lobi bulunamadı: " + lobbyName);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Geçersiz parametre sayısı LEAVE_LOBBY komutu için.");
                            }
                            break;

                        // Diğer komutları da işleyin.
                        case "DELETE_LOBBY":
                            // Lobiyi sil (örnek şart: sadece lobi sahipleri silebilir).
                            if (parameters.Length == 1)
                            {
                                string lobbyName = parameters[0];
                                Lobby lobby = serverInner.GetLobbyByName(lobbyName);
                                if (lobby != null)
                                {
                                    // Örnek şart: Sadece lobi sahipleri silme işlemi yapabilir.
                                    if (lobby.Users.Count == 1 && lobby.Users[0].IsHost)
                                    {
                                        // Lobi sahibi olan son kullanıcı ise, lobiyi silebilir.
                                        serverInner.lobbies.Remove(lobby);
                                        Console.WriteLine("Lobi silindi: " + lobbyName);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Yalnızca lobi sahipleri lobiyi silebilir.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Lobi bulunamadı: " + lobbyName);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Geçersiz parametre sayısı DELETE_LOBBY komutu için.");
                            }
                            break;
                        case "SEND_MESSAGE":
                            if (parameters.Length == 3)
                            {
                                string lobbyName = parameters[0];
                                string senderUsername = parameters[1];
                                string messages = parameters[2];

                                // Lobiyi ve kullanıcıyı belirle
                                Lobby lobby = serverInner.GetLobbyByName(lobbyName);
                                if (lobby != null)
                                {
                                    User sender = lobby.Users.FirstOrDefault(u => u.Username == senderUsername);
                                    if (sender != null && (sender.IsHost || lobby.Users.Count == 1))
                                    {
                                        // Kullanıcı host ise veya lobi tek kullanıcıya sahipse, mesajı iletebilir.
                                        lobby.SendMessage(sender, messages);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Mesaj gönderme izniniz yok.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Lobi bulunamadı: " + lobbyName);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Geçersiz parametre sayısı send_message komutu için.");
                            }
                            break;


                        default:
                            Console.WriteLine("Geçersiz komut.");
                            break;
                    }
                }
            }
        }
    }

}