using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -15f;

    [Header("Chequeo de Suelo (Raycast)")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckDistance = 0.35f;
    [SerializeField] private LayerMask groundLayer;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeedModifier = 1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing)
            return;

        CheckGroundedWithRaycast();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        float finalSpeed = baseSpeed * currentSpeedModifier;
        controller.Move(move * finalSpeed * Time.deltaTime);

        // Salto condicionado exclusivamente al Raycast
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CheckGroundedWithRaycast()
    {
        Vector3 origin = groundCheckPoint != null ? groundCheckPoint.position : transform.position;
        Ray ray = new Ray(origin, Vector3.down);

        isGrounded = Physics.Raycast(ray, groundCheckDistance, groundLayer, QueryTriggerInteraction.Ignore);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Debug.DrawRay(origin, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }

    public void SetSpeedModifier(float modifier)
    {
        currentSpeedModifier = modifier;
    }
}