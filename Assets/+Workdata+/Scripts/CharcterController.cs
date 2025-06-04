using UnityEngine;
using UnityEngine.InputSystem;

public class CharcterController : MonoBehaviour
{
   [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpforce = 3f;
    private float direction = 0f;

    private Rigidbody2D rb;

    [Header("GroundCheck")] 
    // hier speichern wir das Transform des GroundCheck-objekts zwischen (muss im Inspektor zugewiesen sein)
    [SerializeField] private Transform transformGroundCheck;
    
    // in einer LayerMask können wir Ebenen aus unserem Projekt zuweisen für spätere Verwendung im Script
    [SerializeField] private LayerMask layerGround;
    
    
    [Header("Manager")] 
    [SerializeField] private CollectablesManager collectManager;
    [SerializeField] private UIManager uiManager;
    
    private bool canMove = true;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (canMove)
        {
            direction = 0f;

            if (Keyboard.current.aKey.isPressed)
            {
                direction = -1;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                direction = 1;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }

            //transform.position += direction.normalized * speed * Time.deltaTime;

            rb.linearVelocity = new Vector2(direction * speed, y:rb.linearVelocity.y);
        }
    }

    void Jump()
    {
        if (Physics2D.OverlapCircle(transformGroundCheck.position, 0.2f, layerGround))
        {
            rb.linearVelocity = new Vector2(x:0, y: jumpforce);
        }
    }
    
    
    //----collider------
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log("Wir sind mit etwas kollidiert");
        
        if (other.CompareTag("coin"))
        {
            Debug.Log("Es war einen Münze");  
            Destroy(other.gameObject);
            collectManager.AddCoin();
        } 
        
        if (other.CompareTag("diamond"))
        {
            Debug.Log("Es war einen Diamond");  
            Destroy(other.gameObject);
            collectManager.AddDiamond();
        } 
        
        
        
        else if (other.CompareTag("enemy"))
        {
            Debug.Log("Es war einen Gegner");
            uiManager.ShowLosingPanel();
            rb.linearVelocity = Vector2.zero;
            canMove = false;
        }
    }
}
