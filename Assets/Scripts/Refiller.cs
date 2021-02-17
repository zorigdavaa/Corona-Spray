using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().IncreaseGunBy5();
            
            Destroy(gameObject);
        }
    }
}
