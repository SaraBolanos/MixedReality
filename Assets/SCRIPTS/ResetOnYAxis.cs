using UnityEngine;

public class ResetOnYAxis : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Check if the Y position of the object is less than -2
        if (transform.position.y < -2f)
        {
            // If so, reset the position to the initial position
            transform.position = initialPosition;
        }
    }
}
