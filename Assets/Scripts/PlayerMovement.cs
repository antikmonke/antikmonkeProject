using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody; 
    private float inputAxis; 
    private Vector2 velocity; 
    public float movespeed = 8f; 
    public float maxJumpHight = 5f; 
    public float maxJumpTime = 1f; 
    public float jumpForce => (2f * maxJumpHight) / (maxJumpTime / 2f); 
    public float gravity => (-2f * maxJumpHight) / Mathf.Pow((maxJumpTime / 2f), 2); 
    public bool grounded { get; private set;} 
    public bool jumping { get; private set;} 
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f; 
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f); 
 
    private void Awake() 
    {
        rigidbody  = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {     
        HorizontalMovement();
        grounded = rigidbody.Raycast(Vector2.down); 
        
        if (grounded) {
            GroundedMovement(); 
        }

        ApplyGravity(); 
    }

    private void HorizontalMovement() 
    {
        inputAxis = Input.GetAxis("Horizontal"); 
        velocity.x = Mathf.MoveTowards (velocity.x, inputAxis * movespeed, movespeed * Time.deltaTime * 2); 
        if (velocity.x >0f) {
            transform.eulerAngles = Vector2.zero; 
        } else if (velocity.x < 0f) {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    private void GroundedMovement() {
        jumping = velocity.y >0; 
        velocity.y = Mathf.Max(velocity.y, 0f); 
        if (Input.GetButtonDown("Jump")) {
            velocity.y = jumpForce; 
            jumping = true; 

        }
    }

    private void ApplyGravity() {
        bool falling = velocity.y <0 || !Input.GetButton( "Jump");
        float multiplier = falling ? 4f : 1f;
        velocity.y +=  gravity * multiplier * Time.deltaTime; 
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void FixedUpdate() 
    {
        Vector2 position = rigidbody.position; 
        position += velocity * Time.fixedDeltaTime;

        rigidbody.MovePosition(position); 
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
            if (transform.DoTTest(collision.transform, Vector2.down)) {
                velocity.y = jumpForce / 2; 
                jumping = true; 
            }
        }
        

        else if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp")) {
            if (transform.DoTTest(collision.transform, Vector2.up)) {
                velocity.y = 0f;
            }
        }
    }
}
