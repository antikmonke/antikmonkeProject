using System; 
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f; 
    public Vector2 direction = Vector2.left;
    private new Rigidbody2D rigidbody;
    private new Vector2 velocity; 
     
    private void OnBecameVisible() {
        enabled = true;
        Console.Write("Seen");  
    }

    private void OnBecameInVisisble() {
        enabled = false;
    }

    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
    }



    private void OnEnable() {
        rigidbody.WakeUp(); 
    }

    private void OnDisable() {
        rigidbody.velocity = Vector2.zero; 
        rigidbody.Sleep(); 
    }

    private void FixedUpdate() {
        velocity.x = direction.x * speed; 
        velocity.y = Physics2D.gravity.y * Time.fixedDeltaTime;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);  
        if (rigidbody.Raycast(direction)) {
            direction = -direction; 
        }

        if(rigidbody.Raycast(Vector2.down)) {
            velocity.y = Mathf.Max(velocity.y, 0f); 
        }
    }


}
