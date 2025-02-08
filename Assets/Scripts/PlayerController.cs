using UnityEngine;

public class PlayerController : EntityController
{
    public float MoveSpeed;

    /// <summary>
    /// To be called on Update
    /// </summary>
    /// <param name="direction"></param>
    public override void Move(Vector2 direction)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        rigidbody.MovePosition(pos + MoveSpeed * Time.deltaTime * direction);
        
    }


}
