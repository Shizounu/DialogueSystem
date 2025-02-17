using Dialogue.Data;
using UnityEngine;

public class DialogueInteractible : InteractionPoint
{
    public DialogueData Dialogue;

    public float Range = 3f;
    public override float InteractionRange => Range;

    public override void Interact(IInteracter entityController)
    {
        DialogueManager.Instance.EnableDialogueControl();
        DialogueManager.Instance.DoDialogue(Dialogue);
    }
}
