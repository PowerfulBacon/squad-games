using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RlglPlayer : Player
{

    public override void Action()
    {
        if(client.dead) return;
        Debug.Log("yay");
        //Move forward 1 step
        transform.Translate(new Vector3(0, 0, 1));
        var controller = FindObjectOfType<rlglcontroller>();
        if(controller.counter <= 0)
        {
            controller.deathText.text = $"{client.username} was <color=#ff0000><b>ELIMINATED</b></color>!";
            client.KillPlayer();
            return;
        }
        controller.counter --;
        if(transform.position.z > 11)
        {
            //Win
            controller.winnerText.text = $"{client.username} WON!";
        }
    }
    
}
