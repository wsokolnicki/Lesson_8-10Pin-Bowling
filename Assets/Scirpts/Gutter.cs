using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gutter : MonoBehaviour
{
    float ballPositionZ;
    public bool IsInTheGutter = false;

    private void OnTriggerEnter(Collider other)
    {
        ballPositionZ = other.transform.position.z;

        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        other.GetComponent<Rigidbody>().useGravity = false;

        SettingBallInGutter(other);

        IsInTheGutter = true;
    }

    private void SettingBallInGutter(Collider other)
    {
        if (other.transform.position.x > 0)
        { other.transform.position = new Vector3(1.06f, 0, ballPositionZ); }
        else
        { other.transform.position = new Vector3(-1.06f, 0, ballPositionZ); }
    }
}
