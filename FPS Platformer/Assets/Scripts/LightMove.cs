using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private float timer = 0f;

    private Vector3 destination = Vector3.up;

    private void Update()
    {
        if (CheckIfFinished())
            SetDestination();

        Vector3 verticalDirection = destination * Time.deltaTime * speed;
        transform.position += verticalDirection;
    }

    private bool CheckIfFinished()
    {
        timer += Time.deltaTime;
        int seconds = (int)(timer % 60);

        if (seconds > 10)
        {
            timer = 0f;
            return true;
        }

        return false;
    }

    private void SetDestination()
    {
        if (destination == Vector3.up)
            destination = Vector3.down;
        else
            destination = Vector3.up;
    }
}
