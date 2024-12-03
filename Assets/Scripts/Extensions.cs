using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public static class Extensions 
{
    private static LayerMask layerMask  = LayerMask.GetMask ("Default");
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction) 
    {
        if (rigidbody.isKinematic) {
            return false;
        }

        float radious = 0.25f; 
        float distance = 0.375f; 

        RaycastHit2D hit = Physics2D.CircleCast (rigidbody.position, radious, direction, distance, layerMask );  
        return hit.collider != null; 
    }

    public static bool DoTTest(this Transform transform, Transform other, Vector2 testDirection) {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction, testDirection) > 0.5; 
    } 
}