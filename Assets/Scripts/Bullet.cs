using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject visualEffect;
    private void OnCollisionEnter(Collision collision)
    {
        var createdEffect = Instantiate(visualEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(createdEffect, 1f);
        if (collision.gameObject.tag == "Virus")
        {
            GameManager.instance.DisInfected++;
            Destroy(collision.gameObject, 0.3f);
        }
    }
}
