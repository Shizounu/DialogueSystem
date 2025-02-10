using Dialogue;
using UnityEngine;

public class PlayerController : EntityController
{
    public float MoveSpeed;

    public Blackboard blackboard;
    /// <summary>
    /// To be called on Update
    /// </summary>
    /// <param name="direction"></param>
    public override void Move(Vector2 direction)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        rigidbody.MovePosition(pos + MoveSpeed * Time.deltaTime * direction);
        
    }

    private void Start()
    {
        blackboard.Facts["Test"] += 1;
        //blackboard.Facts.Add("Test2", 3);
    }

}
