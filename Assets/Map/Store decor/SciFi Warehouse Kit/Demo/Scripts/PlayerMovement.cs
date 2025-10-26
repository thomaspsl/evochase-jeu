using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Vector3 camPos;
    public Vector3 winPos;

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

        if (isMoving && isGrounded)
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                this.AudioSource.PlayOneShot(footStepSound, 0.7f);
                nextFootstep = footStepDelay;
            }
        }
    }

    void HandleWinCondition()
    {
        camPos = GameObject.Find("Camera").transform.position;
        winPos = GameObject.Find("ATM").transform.position;

        if (Vector3.Distance(camPos, winPos) < 2f)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Win");
        }
    }
}
