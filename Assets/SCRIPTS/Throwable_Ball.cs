using System.Collections;
using UnityEngine;

public class Throwable_Ball : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform throwOrigin;
    public float throwForce = 5f;
    private bool hasThrown = false;

    void Update()
    {
        // Reset hasThrown flag in Update
        hasThrown = false;
    }

    void ThrowBall()
    {
        if (!hasThrown) // Only throw if a ball hasn't been thrown in this frame
        {
            GameObject ball = Instantiate(ballPrefab, throwOrigin.position, throwOrigin.rotation);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = throwOrigin.forward * throwForce;
            }
            else
            {
                Debug.LogWarning("Rigidbody not found in ball prefab.");
            }
            hasThrown = true; // Set hasThrown to true after throwing
        }
    }

    void FixedUpdate()
    {
        if (!hasThrown && (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0)))
        {
            ThrowBall();
        }
    }
}
