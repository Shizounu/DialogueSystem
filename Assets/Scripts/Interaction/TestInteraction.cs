using UnityEngine;

public class TestInteraction : InteractionPoint
{
    public override float InteractionRange => 1;

    public override void Interact(IInteracter entityController)
    {
        Debug.Log("Interacted with");
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Position, InteractionRange);
    }
}
