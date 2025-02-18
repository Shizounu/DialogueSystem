using UnityEngine;
using System.Collections.Generic;

public class InteractableController : MonoBehaviour, IInteracter
{
    public Vector3 Position => transform.position;
    [SerializeField] private float _InteractionRange = 3;
    public float InteractionRange => _InteractionRange;
    
    [SerializeField] private List<InteractionPoint> InteractablesInRange = new();
    [SerializeField] private InteractionPoint FocusedInteractable;

    public void UpdateInteractablesInRange()
    {
        //Remove out of Range
        for (int i = 0; i < InteractablesInRange.Count; i++)
        {
            if (!InteractablesInRange[i].CanInteract(this))
            {
                if(FocusedInteractable == InteractablesInRange[i]) {
                    if(i + 1 < InteractablesInRange.Count)
                        FocusedInteractable = InteractablesInRange[i + 1];
                    else 
                        FocusedInteractable = null;
                }

                InteractablesInRange.Remove(InteractablesInRange[i]);
            }
            
        }

        //Add new in range
        foreach (var Interactable in InteractionPoints.Instance.Interactables) {
            if(Interactable.CanInteract(this))
                if(!InteractablesInRange.Contains(Interactable))
                    InteractablesInRange.Add(Interactable);
        }

        InteractablesInRange.Sort((a, b) => Mathf.RoundToInt(Vector2.Distance(a.Position, Position) - Vector2.Distance(b.Position, Position)));
        if (FocusedInteractable == null && InteractablesInRange.Count > 0)
            FocusedInteractable = InteractablesInRange[0];
    }
    public void InteractWithFocused()
    {
        if (FocusedInteractable == null)
            return;
        FocusedInteractable.Interact(this);
    }
    public void IncrementFocused()
    {
        int indx = InteractablesInRange.FindIndex(a => a == FocusedInteractable);
        if (indx + 1 < InteractablesInRange.Count)
            FocusedInteractable = InteractablesInRange[indx + 1];
        else
            FocusedInteractable = InteractablesInRange[0];
    }
    public void DecrementFocused()
    {
        int indx = InteractablesInRange.FindIndex(a => a == FocusedInteractable);
        if (indx - 1 >= 0)
            FocusedInteractable = InteractablesInRange[indx - 1];
        else
            FocusedInteractable = InteractablesInRange[^1];
    }

    private void Update()
    {
        UpdateInteractablesInRange();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, InteractionRange);

        Gizmos.color = Color.red;
        foreach (var item in InteractablesInRange)
        {
            Gizmos.DrawLine(transform.position, item.Position);
        }
        if(FocusedInteractable != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Position,FocusedInteractable.Position);
        }
    }

}
