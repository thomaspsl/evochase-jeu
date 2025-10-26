using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public CharacterController controller;
    public float speed = 18f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Audio Settings")]
    public AudioClip footStepSound;
    public float footStepDelay = 0.5f;
    private float nextFootstep = 0f;

    [Header("Win Detection")]
    public Transform ATM;
    private Transform Player;

    private Vector3 velocity;
    private bool isGrounded;

    // Propriétés
    private AudioSource AudioSource { get; set; }

    /*
    * Function to wake setup properties
    */
    public void Awake()
    {
        this.AudioSource = GetComponent<AudioSource>();
        this.Player = GetComponent<Transform>();
    }

    void Update()
    {
        HandleMovement();
        HandleFootsteps();
        HandleWinCondition();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleFootsteps()
    {
        bool isMoving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;

        if (isMoving && isGrounded) {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0) {
                this.AudioSource.PlayOneShot(footStepSound, 0.7f);
                nextFootstep = footStepDelay;
            }
        }
    }

    void HandleWinCondition()
    {
        // Debug.Log($"Player X: {this.Player.position.x} | ATM X: {this.ATM.position.x}");
        if (Vector3.Distance(this.Player.position, this.ATM.position) < 2f) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Win");
        }
    }
}
