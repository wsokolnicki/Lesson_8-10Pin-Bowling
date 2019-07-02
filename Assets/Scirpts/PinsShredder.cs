using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsShredder : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<BowlingBall>())
        {
            Destroy(other.gameObject);
        }
        else { return; }
    }
}
