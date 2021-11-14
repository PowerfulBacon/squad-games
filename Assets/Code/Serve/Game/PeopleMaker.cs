using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMaker : MonoBehaviour
{

    public Player baby;

    public static Dictionary<SquadGameClient, Player> gamers = new Dictionary<SquadGameClient, Player>();

    public List<Transform> spawns;

    // Start is called before the first frame update
    void Start()
    {
        gamers.Clear();
        MakeThePeople();
    }

    /// <summary>
    /// where babies come from
    /// </summary>
    public void MakeThePeople()
    {
        foreach(SquadGameClient sgClient in ServerManager.connectedPeople.Values)
        {
            if(sgClient.dead) continue;
            //make them
            Player player = Instantiate<Player>(baby, spawns[Random.Range(0, spawns.Count)].position, Quaternion.identity);
            player.SetClient(sgClient);
            gamers.Add(sgClient, player);
        }
    }

    public int GetAlivePlayerCount()
    {
        int i = 0;
        foreach(SquadGameClient client in gamers.Keys)
        {
            if(client.dead) continue;
            i ++;
        }
        return i;
    }

}
