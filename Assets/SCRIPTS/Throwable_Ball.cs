using System.Collections;
using UnityEngine;

public class Throwable_Ball : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform throwOrigin;
    public float throwForce = 10f;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
        {
            ThrowBall();
        }
    }

    void ThrowBall()
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
    }
}