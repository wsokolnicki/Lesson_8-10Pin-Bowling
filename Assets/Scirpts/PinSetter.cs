using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSetter : MonoBehaviour
{
    [SerializeField]  GameObject pinsPrefab;
    [SerializeField] float distanceToRaise = 55f;
    Vector3 pinsPrefPosition;
    Animator animator;

    private void Start()
    {
        pinsPrefPosition = GameObject.Find("Pins").transform.position;
        animator = GetComponent<Animator>();
    }

    public void PerformAction (ActionMaster.Action action)
    {
        if (action == ActionMaster.Action.Tidy)
        {
            Debug.Log("Tidying Pins");
            animator.SetTrigger("tidyTrigger");
        }
        else if (action == ActionMaster.Action.EndTurn)
        {
            Debug.Log("End of a Turn");
            animator.SetTrigger("resetTrigger");
            GetComponent<PinCounter>().Reset();

        }
        else if (action == ActionMaster.Action.Reset)
        {
            Debug.Log("Reseting Pins");
            animator.SetTrigger("resetTrigger");
            GetComponent<PinCounter>().Reset();
        }
        else if (action == ActionMaster.Action.EndGame)
        {
            Debug.Log("Game Over. Thanks For playing");
        }
    }

    public void RaisingPins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                pin.transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
                pin.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }

    public void LoweringPins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
                pin.transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
                pin.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void RenewPins()
    {
        Destroy(GameObject.Find("Pins"));
        Destroy(GameObject.Find("Pins(Clone)"));
        Instantiate(pinsPrefab, new Vector3(0, distanceToRaise, pinsPrefPosition.z), Quaternion.identity);
        //Invoke("InstantiatePins", 1f);

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
            {
                pin.GetComponent<Rigidbody>().useGravity = false;
            }
    }

    private void InstantiatePins()
    {
        Instantiate(pinsPrefab, new Vector3(0, distanceToRaise, pinsPrefPosition.z), Quaternion.identity);
    }
}
