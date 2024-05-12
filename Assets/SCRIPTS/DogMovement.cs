using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public Transform ball;
    public Transform dog;
    public float movementSpeed = 5f;
    public float stoppingDistance = 1f;

    void Update()
    {
        // Calculate direction to the ball
        Vector3 directionToBall = (ball.position - dog.position).normalized;

        // Look towards the ball
        if (directionToBall != Vector3.zero)
        {
            dog.rotation = Quaternion.LookRotation(directionToBall);
        }

        // Raycast to detect obstacles
        RaycastHit hit;
        if (Physics.Raycast(dog.position, directionToBall, out hit, Mathf.Infinity))
        {
            // If the ray hits something, move the dog to a position just before the obstacle
            float distanceToObstacle = hit.distance;
            Vector3 targetPosition = dog.position + directionToBall * Mathf.Max(0, distanceToObstacle - stoppingDistance);
            dog.position = Vector3.MoveTowards(dog.position, targetPosition, movementSpeed * Time.deltaTime);

            // Visualize the raycast
            Debug.DrawLine(dog.position, hit.point, Color.red);
        }
        else
        {
            // Move the dog directly towards the ball
            dog.position = Vector3.MoveTowards(dog.position, ball.position, movementSpeed * Time.deltaTime);

            // Visualize the raycast
            Debug.DrawRay(dog.position, directionToBall * 1000f, Color.green);
        }
    }
}
