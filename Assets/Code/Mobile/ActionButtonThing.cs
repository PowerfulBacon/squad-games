using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ActionButtonThing : MonoBehaviour
{
    
    public void DoTheThing()
    {
        MobileNetworking.Singleton.SendMessageToServer("BEEP");
    }

}
