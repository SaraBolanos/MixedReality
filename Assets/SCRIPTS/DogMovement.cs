using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public Transform ball;
    public Transform dog;
    public float movementSpeed = 5f;
    public float stoppingDistance = 1f;
    public Transform returnPosition;
    public Transform waitPosition; // Position where the dog waits after dropping the ball
    public float ballPickupDistance = 1.5f;

    private enum DogState
    {
        MovingToBall,
        TakingBall,
        Returning,
        Waiting,
        MovingToWaitPosition
    }

    private DogState currentState = DogState.Waiting;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = dog.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case DogState.MovingToBall:
                MoveToBall();
                break;
            case DogState.TakingBall:
                TakeBall();
                break;
            case DogState.Returning:
                ReturnWithBall();
                break;
            case DogState.Waiting:
                Wait();
               // In this state, dog waits at the designated position
                break;
            case DogState.MovingToWaitPosition:
                MoveToWaitPosition();
                break;
        }

        if ( currentState == DogState.Waiting && ball.GetComponent<BallLogic>().IsGrabbed() == false &&  Vector3.Distance(ball.position, returnPosition.position) > 2f)
        {
            currentState = DogState.MovingToBall;
        }
    }

    private void Wait()
    {
        Vector3 directionToBall = (ball.position - dog.position).normalized;
        //do nothing
        dog.rotation = Quaternion.LookRotation(directionToBall);
    }

    public void MoveToBall()
    {
        // Calculate direction to the ball
        Vector3 directionToBall = (ball.position - dog.position).normalized;

        // Look towards the ball
        if (directionToBall != Vector3.zero)
        {
            dog.rotation = Quaternion.LookRotation(directionToBall);
        }

        // Move the dog directly towards the ball
        dog.position = Vector3.MoveTowards(dog.position, ball.position, movementSpeed * Time.deltaTime);

        // Check if the dog is close enough to the ball to take it
        if (Vector3.Distance(dog.position, ball.position) <= ballPickupDistance)
        {
            currentState = DogState.TakingBall;
        }
    }

    public void TakeBall()
    {
        // Dog takes the ball (you can implement any specific behavior here)
        ball.parent = dog;

        // Change state to returning
        currentState = DogState.Returning;
    }

    public void ReturnWithBall()
    {
        // Calculate direction to return position
        Vector3 directionToReturn = (returnPosition.position - dog.position).normalized;

        // Look towards the return position
        if (directionToReturn != Vector3.zero)
        {
            dog.rotation = Quaternion.LookRotation(directionToReturn);
        }

        // Move the dog directly towards the return position
        dog.position = Vector3.MoveTowards(dog.position, returnPosition.position, movementSpeed * Time.deltaTime);

        // Check if the dog has reached the return position
        if (Vector3.Distance(dog.position, returnPosition.position) <= stoppingDistance)
        {

            ball.position = returnPosition.position;
            ball.GetComponent<Rigidbody>().position = returnPosition.position;
            // Dog drops the ball (you can implement any specific behavior here)
            ball.parent = null;

            // Change state to moving to wait position
            currentState = DogState.MovingToWaitPosition;
        }
    }

    public void MoveToWaitPosition()
    {
        // Calculate direction to wait position
        Vector3 directionToWait = (waitPosition.position - dog.position).normalized;

        // Look towards the wait position
        if (directionToWait != Vector3.zero)
        {
            dog.rotation = Quaternion.LookRotation(directionToWait);
        }

        // Move the dog directly towards the wait position
        dog.position = Vector3.MoveTowards(dog.position, waitPosition.position, movementSpeed * Time.deltaTime);

        // Check if the dog has reached the wait position
        if (Vector3.Distance(dog.position, waitPosition.position) <= stoppingDistance)
        {

            // Change state to waiting
            dog.position = waitPosition.position;

            Vector3 directionToBall = (ball.position - dog.position).normalized;

            // Look towards the ball
            if (directionToBall != Vector3.zero)
            {
                dog.rotation = Quaternion.LookRotation(directionToBall);
            }

            currentState = DogState.Waiting;
        }
        
    }
}
