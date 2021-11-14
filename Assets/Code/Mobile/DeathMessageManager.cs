using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMessageManager : MonoBehaviour
{

    public GameObject deathMessage;

    public static DeathMessageManager deathMessageManager;

    // Start is called before the first frame update
    void Start()
    {
        deathMessageManager = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(deathMessage);
        deathMessage.SetActive(false);
        MobileNetworking.onMessageRecieved += CheckDead;
    }

    private bool die = false;

    public void CheckDead(string message)
    {
        if(message == "dead")
        {
            die = true;
        }
    }

    private void Update()
    {
        if(die)
        {
            Die();
            die = false;
        }
    }

    public void Die()
    {
        deathMessage.SetActive(true);
    }

}
