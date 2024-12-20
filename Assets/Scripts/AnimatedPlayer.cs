using UnityEngine;

public class AnimatedPlayer : MonoBehaviour
{
    public Sprite[] sprites; 
    public float frameRate = 1f / 6f;

    private SpriteRenderer spriteRenderer; 
    private int frame; 

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        InvokeRepeating(nameof(Animate), frameRate, frameRate);
    }

    private void OnDisable() {
        CancelInvoke(); 
    }

    private void Animate() {
        frame++; 

        if (frame >= sprites.Length) {
            frame = 0;
        }   
        if (frame >= 0f && frame < sprites.Length) {
            spriteRenderer.sprite = sprites[frame];
        }    
    }
}
