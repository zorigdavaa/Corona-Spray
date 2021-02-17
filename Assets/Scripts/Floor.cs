using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    GroundSpawner GroundSpawner;
    // Start is called before the first frame update
    void Start()
    {
        GroundSpawner = FindObjectOfType<GroundSpawner>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            GroundSpawner.spawnTIle();
        }
    }
}
