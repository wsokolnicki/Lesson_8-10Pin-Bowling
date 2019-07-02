using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BowlingBall))]
public class DragLaunch : MonoBehaviour
{
    BowlingBall ball;
    float startTime, endTime;
    [SerializeField] float ballXBoundry = 0.65f;
    Vector3 dragStart, dragEnd;
    GameObject leftArrow, rightArrow;

	void Start ()
    {
        ball = GetComponent<BowlingBall>();
        leftArrow = GameObject.Find("Left Arrow");
        rightArrow = GameObject.Find("Right Arrow");
	}

    public void DragStart ()
    {
        startTime = Time.time;
        dragStart = Input.mousePosition;
    }

    public void DragEnd()
    {
            endTime = Time.time;
            dragEnd = Input.mousePosition;

            float dragDuration = endTime - startTime;
            float launchSpeedX = ((dragEnd.x - dragStart.x) / 3) / dragDuration;
            float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

            Vector3 velocity = new Vector3(launchSpeedX, 0, launchSpeedZ);
            ball.Launch(velocity / 350f);
    }

    public void MoveStart (float xNudge)
    {
        if (!ball.moving)
        {
            float xPos = ball.transform.position.x;
            if (xPos >= ballXBoundry)
            {
                rightArrow.SetActive(false);
                TransformBall(xNudge);
            }
            else if (xPos <= -ballXBoundry)
            { 
                leftArrow.SetActive(false);
                TransformBall(xNudge);
            }
            else
            {
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);
                TransformBall(xNudge);
            }
        }
    }

    private void TransformBall(float xNudge)
    {
        ball.transform.Translate(new Vector3(xNudge, 0, 0));
    }
}
