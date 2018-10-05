using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private CharacterController player;
    private PlayerLook lookScript;
    private PlayerMove moveScript;
    private Collider obstacle;
    private bool isClimbing;

    [SerializeField]
    private float movementSpeed = 6f;

    [SerializeField]
    private KeyCode climbKey = KeyCode.Space;

    [SerializeField]
    private new Camera camera;

    private void Awake()
    {
        player = GetComponentInParent<CharacterController>();
        lookScript = camera.GetComponent<PlayerLook>();
        moveScript = GetComponentInParent<PlayerMove>();
    }

    private void Update()
    {
        if (IsClimable())
            StartCoroutine(Climb());
    }

    private void OnTriggerEnter(Collider other)
    {
        obstacle = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isClimbing)
            obstacle = null;
    }

    private bool IsClimable()
    {
        if (!isClimbing &&
            obstacle != null &&
            Input.GetKey(climbKey) &&
            obstacle.bounds.max.y - player.bounds.max.y <= 0.75f)
        {
            return true;
        }

        return false;
    }

    private void ToggleControls(bool isEnabled)
    {
        lookScript.enabled = isEnabled;
        moveScript.IsEnabled = isEnabled;
        isClimbing = !isEnabled;
    }

    private IEnumerator Climb()
    {
        ToggleControls(false);
        
        float currentSpeed = movementSpeed;
        bool changeSpeed = false;

        Vector3 point = obstacle.ClosestPointOnBounds(player.bounds.max);
        float halfDistance = (point.y + player.bounds.min.y) / 2;

        while (player.bounds.min.y < point.y)
        {
            player.transform.Translate(player.transform.TransformDirection(Vector3.up) * Time.deltaTime * currentSpeed);
            if (player.bounds.min.y < halfDistance)
            {
                camera.transform.Rotate(Vector3.right * Time.deltaTime * 30f * movementSpeed, Space.Self);
                player.transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * 0.15f);
            }
            else
            {
                camera.transform.Rotate(Vector3.left * Time.deltaTime * 30f * currentSpeed, Space.Self);
                player.transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * 1.1f);
            }

            if (!changeSpeed && currentSpeed > movementSpeed / 2)
                currentSpeed -= movementSpeed * 0.03f;
            else
                changeSpeed = true;
            
            if (changeSpeed && currentSpeed < movementSpeed * 2)
                currentSpeed += movementSpeed * 0.015f;
            
            yield return null;
        }

        obstacle = null;
        ToggleControls(true);
    }
}
