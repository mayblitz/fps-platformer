using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private string horizontalInputName = "Horizontal";

    [SerializeField]
    private string verticalInputName = "Vertical";

    [SerializeField]
    private float movementSpeed = 6f;

    [SerializeField]
    private float gravity = 20f;

    [SerializeField]
    private float jumpForce = 8f;

    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;

    private float verticalVelocity;
    private bool hasJumpedTwice;

    private CharacterController charController;

    public bool IsEnabled { get; set; }

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        IsEnabled = true;
    }

    private void Update()
    {
        if (IsEnabled)
            PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

        if (charController.isGrounded)
        {
            hasJumpedTwice = false;
            verticalVelocity = -5;

            if (Input.GetKeyDown(jumpKey))
                verticalVelocity = jumpForce;
        }
        else if (!hasJumpedTwice && Input.GetKeyDown(jumpKey))
        {
            verticalVelocity = jumpForce;
            hasJumpedTwice = true;
        }
        else
            verticalVelocity -= gravity * Time.deltaTime;
        
        Vector3 jumpMovement = transform.up * verticalVelocity;
        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;
        Vector3 moveVector = forwardMovement + rightMovement + jumpMovement;

        charController.Move(moveVector * Time.deltaTime);
    }
}
