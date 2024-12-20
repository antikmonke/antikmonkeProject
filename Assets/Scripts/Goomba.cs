using Unity.Hierarchy;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite; 

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (collision.transform.DoTTest(transform, Vector2.down)) {
                Flatten(); 
            }
        }
    }

    private void Flatten() {
        GetComponent<Collider2D>().enabled = false; 
        GetComponent<EntityMovement>().enabled = false;  
        GetComponent<AnimatedPlayer>().enabled = false; 
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f); 
    }

}
