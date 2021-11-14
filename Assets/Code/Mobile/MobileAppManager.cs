using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobileAppManager : MonoBehaviour
{

    public TextMeshProUGUI textMeshMessage;

    public void ConnectToServer(string ipAddress)
    {
        try
        {
            //Make the mobile networker
            MobileNetworking.Singleton = new MobileNetworking(ipAddress);
        }
        catch(Exception e)
        {
            textMeshMessage.text = "Server not found!";
        }
    }

    public void SetUsername(string name)
    {
        MobileNetworking.username = name;
    }

    public static bool startGame = false;

    private void Update()
    {
        if(startGame)
        {
            SceneManager.LoadScene("THE BUTTON");
            startGame = false;
        }
    }

}
