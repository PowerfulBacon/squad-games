using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviour
{

    public TextMeshProUGUI logText;
    public TextMeshProUGUI peopleInTheGameText;

    public static UdpClient udpListener;

    public static Dictionary<IPAddress, SquadGameClient> connectedPeople = new Dictionary<IPAddress, SquadGameClient>();

    // Start is called before the first frame update
    void Start()
    {
        //Try to make the server lol
        udpListener = new UdpClient(27005);
        //Start listening thread
        Thread listeningThread = new Thread(ListeningThread);
        listeningThread.Start();
        logText.text = $"HEY! TO JOIN THE GAME DOWNLOAD THE APP AT (HERE), CONNECT TO THE SAME WIFI NETWORK AS ME AND CONNECT TO {GetIPAddress()}";
    }

    //test comment

    public void StartTheSquadGames()
    {
        //Stop listening for colour change events
        foreach(SquadGameClient client in connectedPeople.Values)
        {
            client.onAction -= client.SetRandomColor;
        }
        SceneManager.LoadScene("RedLightGreenLight");
    }

    //
    private string GetIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach(var ip in host.AddressList)
        {
            if(ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "error lol just try all ips";
    }

    private string peopleInTheGameTextText;
    private string emergencyText;

    private void Update()
    {
        peopleInTheGameTextText = "";
        foreach(SquadGameClient client in connectedPeople.Values)
        {
            peopleInTheGameTextText = $"{peopleInTheGameTextText}    ><color=#{ColorUtility.ToHtmlStringRGB(client.color)}>{client.username}</color><";
        }
        peopleInTheGameText.text = peopleInTheGameTextText;
        if(emergencyText != null)
        {
            logText.text = emergencyText;
        }
    }

    public void ListeningThread()
    {
        while(true)
        {
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 27005);
                byte[] rec = udpListener.Receive(ref ipEndPoint);
                string message = Encoding.UTF8.GetString(rec);
                Debug.Log($"{ipEndPoint.ToString()} sent {message}");
                //logText.text = $"{ipEndPoint.ToString()} sent {message}";
                //Do something
                switch(message.Substring(0, 4))
                {
                    case "JOIN":
                        //Make the squad game client
                        SquadGameClient sgClient = new SquadGameClient();
                        sgClient.username = message.Substring(5, message.Length - 5);
                        sgClient.endPoint = ipEndPoint;
                        sgClient.onAction += sgClient.SetRandomColor;
                        sgClient.SendMessage("accepted");
                        connectedPeople.Add(ipEndPoint.Address, sgClient);
                        break;
                    case "BEEP":
                        //get the thing
                        connectedPeople[ipEndPoint.Address]?.onAction?.Invoke();
                        break;
                }
            }
            catch(Exception e)
            {
                Debug.LogError(e);
                emergencyText = $"BRUHHHHHHH {e.ToString()} XD";
            }
        }
    }

}
