using UnityEngine;
using Input;

[RequireComponent(typeof(EntityController))]
[RequireComponent(typeof(InteractableController))]
public class InputController : MonoBehaviour
{
    private InputActions actions;
    private EntityController entityController;
    private InteractableController interactableController;

    private void Awake()
    {
        entityController = GetComponent<EntityController>();
        interactableController = GetComponent<InteractableController>();
        actions = new InputActions();


        actions.Player.Interact.performed += _ => { interactableController.InteractWithFocused(); Debug.Log("Try interact"); };
        actions.Player.Next.performed += _ => interactableController.IncrementFocused();
        actions.Player.Previous.performed += _ => interactableController.DecrementFocused();
    }
    private void Update()
    {
        if (actions.Player.Move.IsPressed()) {
            entityController.Move(actions.Player.Move.ReadValue<Vector2>());
        }
    }





    private void OnEnable() => actions.Player.Enable();
    private void OnDisable() => actions.Player.Disable();
}
