using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class rlglcontroller : MonoBehaviour
{

    public PeopleMaker peopleMaker;

    public TextMeshProUGUI counterText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI deathText;

    public int counter = 0;

    public float secondsLeft = 120;

    public float timer = 10;

    public void SetCounter()
    {
        timer = Random.Range(20.0f, 40.0f);
        counter = (int)Random.Range(1.0f * peopleMaker.GetAlivePlayerCount(), 6.0f * peopleMaker.GetAlivePlayerCount());
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        secondsLeft -= Time.deltaTime;
        timerText.text = $"00:{secondsLeft}";
        counterText.text = $"{counter}";
        if(secondsLeft < 0) EndTheGame();
        if(timer < 0) SetCounter();
    }

    public void EndTheGame()
    {

    }
    
}
