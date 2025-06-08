using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [Header("GroundCheck")]
    // hier speichern wir das Transform des GroundCheck-objekts zwischen (muss im Inspektor zugewiesen sein)
    [SerializeField] private Transform transformGroundCheck;
    
    // in einer LayerMask können wir Ebenen aus unserem Projekt zuweisen für spätere Verwendung im Script
    [SerializeField] private LayerMask layerGround;
    
    [Header("Manager")] 
    [SerializeField] private CollectablesManager collectManager;
    [SerializeField] private UIManager uiManager;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpforce = 3f;
    public float direction = 0f;

    private Rigidbody2D rb;                                    // created a rigidbody component to be able to use in the script  

    public bool canMove = false;                               // created a bool to determine if the player can move -> false/ true


    void Start()                                               // this function is called only once when the game starts //                               
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveCountdown());
    }


    void Update()                                                // this function updates ever frame //
    {
        if (canMove)
        {
            direction = 0f;

            if (Keyboard.current.aKey.isPressed)
            {
                direction = -1;
                gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }

            if (Keyboard.current.dKey.isPressed)
            {
                direction = 1;
                gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

            if (Keyboard.current.spaceKey.isPressed)
            {
                Jump();
            }

            rb.linearVelocity = new Vector2(direction * speed, y: rb.linearVelocity.y);
        }
    }

    void Jump()
    {
        if (Physics2D.OverlapCircle(transformGroundCheck.position, 0.05f, layerGround))
        {
            rb.linearVelocity = new Vector2(x: 0, y: jumpforce);
        }
    }
    
    public IEnumerator MoveCountdown(){
        for (int i = 3; i > 0; --i)
        {
            canMove = false;
            yield return new WaitForSeconds(1f);
        } 
        canMove = true;
    }


    //----collider------//
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("We collided with something");

        if (other.CompareTag("coin"))
        {
            Debug.Log("Coin collected");
            Destroy(other.gameObject);
            collectManager.AddCoin();
        }

        if (other.CompareTag("diamond"))
        {
            Debug.Log("Diamond collected");
            Destroy(other.gameObject);
            collectManager.AddDiamond();
        }


        else if (other.CompareTag("enemy"))
        {
            Debug.Log("It was an enemy");
            uiManager.ShowLosingPanel();
            rb.linearVelocity = Vector2.zero;
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