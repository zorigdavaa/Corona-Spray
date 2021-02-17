using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] GroundSpawner groundSpawner;
    [SerializeField] int further = 300;
    GameManager gameManager;
    PlayerMovement playerMovement;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.LastBariaBullet = playerMovement.RemainingBullet;
            gameManager.LastBariaDisInfect = gameManager.DisInfected;
            Invoke("MoveTriggerToNextPosition", 0.2f);
        }
    }

    private void MoveTriggerToNextPosition()
    {
        GameManager.instance.Level++;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + further);
    }
}
