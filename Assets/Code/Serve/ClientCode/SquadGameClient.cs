
using System;
using System.Net;
using System.Text;
using UnityEngine;

public class SquadGameClient
{

    public delegate void OnAction();
    public OnAction onAction;

    public IPEndPoint endPoint;

    public string username;

    //lol u died
    public bool dead = false;

    public Color color;

    public SquadGameClient()
    {
        SetRandomColor();
    }

    public void KillPlayer()
    {
        SendMessage("dead");
        if(PeopleMaker.gamers.ContainsKey(this)) PeopleMaker.gamers[this].Die();
    }

    public void SetRandomColor()
    {
        System.Random random = new System.Random();
        color = new Color(random.Next(0, 255) / 255.0f, random.Next(0, 255) / 255.0f, random.Next(0, 255) / 255.0f);
        Debug.Log($"Color changed to {color}");
    }

    public void SendMessage(string message)
    {
        
        try
        {
            byte[] sdsfgafgafga = Encoding.UTF8.GetBytes(message);
            ServerManager.udpListener.Send(sdsfgafgafga, sdsfgafgafga.Length, endPoint);
        }
        catch (Exception e) {
            Debug.LogError(e);
        }
    }

}
