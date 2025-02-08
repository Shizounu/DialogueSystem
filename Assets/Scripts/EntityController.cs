using UnityEngine;


[RequireComponent (typeof(Rigidbody2D)), RequireComponent(typeof(Collider2D))]
public abstract class EntityController : MonoBehaviour
{


    protected new Rigidbody2D rigidbody;
    protected new Collider2D collider; 
    
    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    public abstract void Move(Vector2 direction);
}
