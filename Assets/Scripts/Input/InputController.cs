using UnityEngine;
using Input;

//[RequireComponent(typeof(EntityController))]
//[RequireComponent(typeof(InteractableController))]
public class InputController : MonoBehaviour
{
    public InputActions actions;
    [SerializeField] private EntityController entityController;
    [SerializeField] private InteractableController interactableController;
    [SerializeField] private DialogueManager dialogueManager;
    private void Awake()
    {
        //entityController = GetComponent<EntityController>();
        //interactableController = GetComponent<InteractableController>();
        actions = new InputActions();


        actions.Player.Interact.performed += _ => interactableController.InteractWithFocused();
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
