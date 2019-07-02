using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    Rigidbody rigbody;
    [SerializeField] Vector3 ballVelocity;
    AudioSource audioSource;
    Vector3 startPosition;
    public bool moving = false;
    public bool inPlay = false;

    Gutter[] gutters;
    

    void Start()
    {
        rigbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        gutters = FindObjectsOfType<Gutter>();
        rigbody.useGravity = false;
        startPosition = transform.position;
    }

    private void Update()
    {
        foreach (Gutter gutter in gutters)
        {
            if (gutter.IsInTheGutter)
            {
                BallMovingInTheGutter();
            }
        }
    }

    private void BallMovingInTheGutter()
    {
        rigbody.angularVelocity = new Vector3(10, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(transform.position.x, transform.position.y, 20f), 5 * Time.deltaTime);
        if(transform.position.z >=20f)
        {
            rigbody.angularVelocity = Vector3.zero;
            foreach (Gutter gutter in gutters)
            {
                gutter.IsInTheGutter = false;
            }
        }
    }

    public void Launch (Vector3 velocity)
    {
        if (!moving)
        {
            inPlay = true;
            moving = true;
            rigbody.useGravity = true;
            rigbody.velocity = velocity;
            audioSource.Play();
        }
    }

    public void Reset()
    {
        inPlay = false;
        transform.position = startPosition;
        transform.eulerAngles = new Vector3(0,0,0);
        rigbody.velocity = new Vector3(0,0,0);
        rigbody.angularVelocity = Vector3.zero;
        rigbody.useGravity = false;
        moving = false;
    }
}
