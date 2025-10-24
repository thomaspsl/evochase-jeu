using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 18f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    public AudioClip footStepSound;
    public float footStepDelay;
 
    private float nextFootstep = 0;

    public Vector3 camPos;
    public Vector3 winPos;

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        //Debug.Log(isGrounded);
        Win();

        if (isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 motion = transform.right * x + transform.forward * z;
        controller.Move(motion * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Z) && isGrounded)
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0) 
            {
                GameObject.Find("footstep").GetComponent<AudioSource>().PlayOneShot(footStepSound, 0.7f);
                nextFootstep += footStepDelay;
            }
        }
    }

    void Win()
    {
        camPos = GameObject.Find("Camera").transform.position;
        winPos = GameObject.Find("ATM").transform.position;
        if (Vector3.Distance(camPos, winPos) < 2)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(sceneName: "Win");
        }
    }
}