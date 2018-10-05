using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    private int minSpeed = 1;

    [SerializeField]
    private int maxSpeed = 16;

    private int speed;

    private void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
