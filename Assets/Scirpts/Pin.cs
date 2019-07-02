using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public float standingTreshold = 1f;

    public bool IsStanding()
    {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;
        float tiltInX = Mathf.Abs(rotationInEuler.x) + 90f;
        float tiltInZ = Mathf.Abs(rotationInEuler.y);

        if ((tiltInX < standingTreshold || tiltInX > 360f - standingTreshold)
            && (tiltInZ < standingTreshold || tiltInZ > 360f - standingTreshold))
        { return true; }
        else
        { return false; }
    }


}
