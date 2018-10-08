using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0, 0);
    }
}
