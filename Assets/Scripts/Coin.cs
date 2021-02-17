using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    float rotateSpeed = 90;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }
        // if player collide
        if (other.gameObject.name != "Player")
        {
            return;
        }
        // add score to player
        //GameManager.instance.IncrementScore();
        //Destroy coin
        Destroy(gameObject);
    }
}
