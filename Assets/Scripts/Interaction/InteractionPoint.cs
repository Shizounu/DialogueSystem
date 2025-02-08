using System;
using UnityEngine;

public abstract class InteractionPoint : MonoBehaviour
{
    public Vector3 Position { get => transform.position; }
    public abstract float InteractionRange { get; }
    
    public abstract void Interact(IInteracter entityController);
    public bool CanInteract(IInteracter interacter)
    {
        return interacter.InteractionRange + InteractionRange >= Vector2.Distance(transform.position, interacter.Position);
    }
    public void RegisterManager()
    {
        InteractionPoints.Instance.Interactables.Add(this);
    }

    public void UnregisterManager()
    {
        InteractionPoints.Instance.Interactables.Remove(this);
    }

    private void OnEnable() => RegisterManager();
    private void OnDisable() => UnregisterManager();
}
