using UnityEngine;

namespace Dialogue.Data
{
    public class DialogueData : ScriptableObject
    {
        [SerializeReference] public DialogueElement StartingElement;

    }
}
