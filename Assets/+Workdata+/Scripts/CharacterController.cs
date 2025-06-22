using System.Collections;                            // the different imports, necessary for some parts of the script to work
using System.Threading;
using UnityEngine;                                   // -> "using Unity.Engine" defines you're working with unity 
using UnityEngine.Events;
using UnityEngine.InputSystem;                                

public class CharacterController : MonoBehaviour                       // a new class is created whenever you're making a new script. The name of the class must be the same as in Unity for it to work
{
    [Header("GroundCheck")]                                            // a header lets you organize your inspector. It creates a title.
    [SerializeField] private Transform transformGroundCheck;           // a serializeField lets you assign the variables in the inspector  
                                                                       // here I saved the Transform of the GroundCheck- object
    //[Header("Events")]
    //public UnityEvent onLanding;
    
    
    [SerializeField] private LayerMask layerGround;                     // you can assign a layer from your project to a layerMask for later use in the script 
    
    [Header("Manager")] 
    [SerializeField] private CollectablesManager collectManager;        // to connect the scripts. Done in the inspector needed to use Functions and variables from other scripts
    [SerializeField] private UIManager uiManager;
    
    [SerializeField] private float speed = 5f;                          // sets the default speed of the character to 5 -> serializeField to be able to test and change the speed in the inspector
    [SerializeField] private float jumpforce = 3f;                      // used float to make using decimals possible 
    public float direction = 0f;                                        // public -> makes it assignable in different scripts as well as outside of scripts

    private Rigidbody2D rb;                                             // created a rigidbody variable to be able to use in the script  

    public bool canMove = false;                                        // created a bool to determine if the player can move -> false/ true

    public Animator animator;
    
    public AudioClip jumpSound;                                         // created variables to add sound effects 
    public AudioClip collectSound;
    private AudioSource audioSource;
    
    //public event System.Action OnLand;
    
    
    void Start()                                                              // this function is called only once when the game starts                            
    {
        rb = GetComponent<Rigidbody2D>();                                     // giving the variable the same values as in the inspector 
        StartCoroutine(MoveCountdown());                               // Starting a Coroutine 
        audioSource = GetComponent<AudioSource>();                            // assigns the audio variables
    }


    void Update()                                                             // everything in this function updates every frame 
    {
        if (canMove)
        {
            animator.SetFloat("Speed",Mathf.Abs(direction));             // for the running animation. Speed -> the used parameter. Mathf.Abs -> to make it work with minus as well

            direction = 0f;                                                    // speed when no key is pressed == player stands still

            if (Keyboard.current.aKey.isPressed)                               // if current -> At this moment A is pressed do:
            {
                direction = -1;                                                // move to the left (minus on the x axes) 
                gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);  // rotates the gameObject
            }

            if (Keyboard.current.dKey.isPressed)
            {
                direction = 1;
                gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();                                                                            //calls the jump function that is made separately in Update to let it work all the time
            }

            rb.linearVelocity = new Vector2(direction * speed, y: rb.linearVelocity.y);            //rb -> for physics -> adds velocity instead of making object jump to coordinates
        }
    }

    void Jump()                                                                                     // void -> creates a new function. Jump -> is given name (the name doesn't affect the functions function)
    {
        if (Physics2D.OverlapCircle(transformGroundCheck.position, 0.05f, layerGround))             // checks if there is a ground to jump from in the radius of 0.05 -> so that the player cant jump in the air  
        {
            rb.linearVelocity = new Vector2(x: 0, y: jumpforce);                                    // makes the player jump on the y axes
            audioSource.PlayOneShot(jumpSound);
            //animator.SetBool("isJumping", true);
        }
    }

  // public void OnLanding()
  // {
  //     animator.SetBool("isJumping", false);
  // }
    
    public IEnumerator MoveCountdown(){                                     //IEnumerator is the declaration for a coroutine. This one is for the starting countdown 
        for (int i = 3; i > 0; --i)                                         // for -> creates loop. int i -> variable (any can be used i for int is default) = 3 -> sets int to 3. i > 0 -> repeats the loop until i is O. --i -> subtracts 1 ever time the loop ends
        {                                                                   // player cant move while countdown
            canMove = false;
            yield return new WaitForSeconds(1f);                            // the loop waits one second before continuing 
        } 
        canMove = true;                                                     // after loop has ended player can move
    }


    //----collider------//
    private void OnTriggerEnter2D(Collider2D other)                         //creates function for the collider 
    {
        //Debug.Log("We collided with something");

        if (other.CompareTag("coin"))                                       // if an object has this tag this condition will be called
        {
            Debug.Log("Coin collected");                                    // shows if the player is collided with the object in the Debug Log
            Destroy(other.gameObject);                                      // object gets destroyed after collision 
            audioSource.PlayOneShot(collectSound);                          // plays sound when coin is collected
            collectManager.AddCoin();                                       // Add coin function form collectables Manager is called (adds a 1 to the coin counter)
        }

        if (other.CompareTag("diamond"))
        {
            Debug.Log("Diamond collected");
            Destroy(other.gameObject);
            audioSource.PlayOneShot(collectSound);
            collectManager.AddDiamond();
        }


        else if (other.CompareTag("enemy"))                                 // else if -> allows for multiple conditions to be checked in the same function
        {
            Debug.Log("It was an enemy");
            uiManager.ShowLosingPanel();                                    // shows Losing panel after collision 
            rb.linearVelocity = Vector2.zero;                               // stops the velocity (takes away speed) 
            canMove = false;                                            
        }
        
        else if (other.CompareTag("DeathZone"))
        {
            Debug.Log("Fallen into Death Zone");
            uiManager.ShowLosingPanel();
            rb.linearVelocity = Vector2.zero;
            canMove = false;
        }
        
    }

}

