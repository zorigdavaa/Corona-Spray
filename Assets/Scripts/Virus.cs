using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    PlayerMovement player;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.Die();
        }
        
    }
}
