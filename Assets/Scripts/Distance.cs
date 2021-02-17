using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Text DistanceText;
    private void Update()
    {
        DistanceText.text = player.position.z.ToString("0") + "m";
    }
}
