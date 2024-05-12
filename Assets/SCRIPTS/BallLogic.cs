using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isGrabbed = false;

    private void OnCollisionEnter(Collision other)
    {
        // Check if the object entering the trigger is a hand or grabbing object
        if (other.collider.CompareTag("Hand")) // Assuming "Hand" is the tag of the grabbing object
        {
            isGrabbed = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        // Check if the object exiting the trigger is a hand or grabbing object
        if (collision.collider.CompareTag("Hand"))
        {
            isGrabbed = false;
        }
    }



    public bool IsGrabbed()
    {
        return isGrabbed;
    }

}
