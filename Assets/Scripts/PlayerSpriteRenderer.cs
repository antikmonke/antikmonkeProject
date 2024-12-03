using UnityEngine;
using UnityEngine.Scripting;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; 
    private PlayerMovement movement; 
    public Sprite idle; 
    public AnimatedPlayer run;
    public Sprite jump;
    public Sprite slide; 


    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        movement = GetComponentInParent<PlayerMovement>();
    }



    private void LateUpdate() {
         

        if (movement.jumping) {
            spriteRenderer.sprite = jump;} 
        else if (movement.sliding) {
            spriteRenderer.sprite = slide;
        }
        
        else if (!movement.running) {
            spriteRenderer.sprite = idle;
        }
        run.enabled = movement.running; 
        


    }
}
