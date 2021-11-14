using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public ParticleSystem explosion;

    public SquadGameClient client;

    public void SetClient(SquadGameClient c)
    {
        client = c;
        client.onAction += HandleAction;
        gameObject.name = c.username;
        GetComponent<MeshRenderer>().material.color = client.color;
    }

    private void OnDestroy()
    {
        client.onAction -= HandleAction;
    }

    private bool acting = false;

    private void HandleAction()
    {
        acting = true;
    }

    private void Update()
    {
        if(acting)
        {
            Action();
            acting = false;
        }
    }

    public virtual void Action()
    {

    }

    public virtual void Die()
    {
        transform.localScale = new Vector3(1.0f, 0.01f, 1.0f);
        explosion.Play();
    }

}
