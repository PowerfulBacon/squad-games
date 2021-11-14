
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobileNetworking
{

    public static MobileNetworking Singleton;

    public static string username;

    public delegate void OnMessageRecieved(string message);
    public static OnMessageRecieved onMessageRecieved;

    private UdpClient client;
    IPAddress serverIpAddress;
    IPEndPoint serverEndPoint;
    
    public MobileNetworking(string ipAddress)
    {
        serverIpAddress = IPAddress.Parse(ipAddress);
        client = new UdpClient();
        serverEndPoint = new IPEndPoint(serverIpAddress, 27005);
        Thread recThread = new Thread(ReceivingThread);
        recThread.Start();
        //we joined :)
        SendMessageToServer($"JOIN {username}");
    }

    public void ReceivingThread()
    {
        //lol???????????
        while(true)
        {
            //Get the data
            Debug.Log($"Got message from {serverEndPoint.Address}");
            IPEndPoint recEndPoint = new IPEndPoint(serverIpAddress, 27005);
            byte[] recievedMessage = client.Receive(ref recEndPoint);
            //Do something
            //onMessageRecieved?.Invoke(Encoding.UTF8.GetString(recievedMessage));
            //lol
            string message = Encoding.UTF8.GetString(recievedMessage);
            switch(message)
            {
                case "accepted":
                    MobileAppManager.startGame = true;
                    break;
                case "dead":
                    DeathMessageManager.deathMessageManager.Die();
                    break;
            }
        }
    }

    public void SendMessageToServer(string mes)
    {
        byte[] message = Encoding.UTF8.GetBytes(mes);
        client.Send(message, message.Length, serverEndPoint);
    }

}
