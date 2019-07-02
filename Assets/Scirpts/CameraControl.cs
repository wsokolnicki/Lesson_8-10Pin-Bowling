using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public BowlingBall bowlingBall;
    Vector3 offset;
    float cameraStopPoint = 15.5f;

	void Start ()
    { 
        offset = transform.position - bowlingBall.transform.position;
    }
	
	void Update ()
    {
        CameraPosition();
    }

    private void CameraPosition()
    {
        if (bowlingBall.transform.position.z < cameraStopPoint)
        {
            transform.position = bowlingBall.transform.position + offset;
        }
        else
        {
            return;
        }
    }
}
