using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    //Animation personnage
    Actions actions;

    //Vitesse de déplacement
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;


    public Vector3 jumpSpeed;
    CapsuleCollider playerColider;

    public bool estMort=false;

    // Start is called before the first frame update
    void Start()
    {
        actions = gameObject.GetComponent<Actions>();
        playerColider = gameObject.GetComponent<CapsuleCollider>();
    }

    bool Ausol= true; 
    
    async void Update()
    {
        //mort
        if (!estMort)
        {
            //Avancer
            if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);
                actions.Walk();//Personnage avance

            }
            //Sprint
            if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                actions.Run();//Personnage Cours
            }
            //Reculer
            if (Input.GetKey(inputBack))
            {
                transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);
                actions.Walk();//Personnage avance

            }
            //Gauche
            if (Input.GetKey(inputLeft))
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            }
            //Droite
            if (Input.GetKey(inputRight))
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            }
            //Aucun Mouvement
            if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack))
            {
                actions.Stay();
            }
            //Saut
            if (Input.GetKeyDown(KeyCode.Space) && Ausol)
            {
                Ausol = false;
                actions.Jump();

                //Preparation Saut
                Vector3 v = gameObject.GetComponent<Rigidbody>().linearVelocity;
                v.y = jumpSpeed.y;

                //Saut
                gameObject.GetComponent<Rigidbody>().linearVelocity = jumpSpeed;
                await Task.Delay(1000);
                Ausol = true;
            }
        }
    }
}
