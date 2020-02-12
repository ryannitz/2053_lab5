using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{
    float circleRadius;
    float circleSpeed;
    float circleAngle;
    float selfRotationSpeed;
    Vector3 lastDirection;

    public bool colliding = false;
    public bool timerStarted = false;
    public bool halfway = false;

    public GameController gameController;

    void Start()
    {
        circleRadius = 10;
        circleSpeed = 0.0f;
        circleAngle = 0f;
        selfRotationSpeed = 0f;

        lastDirection = new Vector3(1, 0, 0);
        lastDirection.Normalize();
    }

    void Update()
    {

        if (!timerStarted && Input.anyKeyDown)
        {
            gameController.StartTimer();
            timerStarted = true;
        }
        {

        }
        if (Input.GetKey(KeyCode.UpArrow) && !colliding)
        {
            circleSpeed = 0.5f;
        }
        else
        {
            circleSpeed = 0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            selfRotationSpeed = 10f;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            selfRotationSpeed = -10f;
        }
        else
        {
            selfRotationSpeed = 0f;
        }


        circleAngle += circleSpeed * Time.deltaTime;
        circleAngle = (circleAngle + 360) % 360;

        float newPositionX = circleRadius * (float)Mathf.Cos(circleAngle);
        float newPositionZ = circleRadius * (float)Mathf.Sin(circleAngle);

        Vector3 newPosition = new Vector3(newPositionX, transform.position.y, newPositionZ);
        Vector3 newDirection = newPosition - transform.position;

        newDirection.Normalize();

        float rotationAngle = -Vector3.Angle(lastDirection, newDirection);
        transform.Rotate(Vector3.up, rotationAngle, Space.World);
        transform.Rotate(Vector3.forward, selfRotationSpeed * Time.deltaTime, Space.Self);

        transform.position = newPosition;
        lastDirection = newDirection;

        if (newPositionX < 0f)
        {
            halfway = true;
        }
        
        if (halfway && newPositionX >= 9.5f)
        {
            gameController.Win();
            //end game or restart on lower time
        }

      
    }

    private void OnTriggerStay(Collider other)
    {
        colliding = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        colliding = false;
    }
}

