using UnityEngine;

namespace Dialogue.Data
{
    public class Sentence : DialogueElement
    {
        public Speaker Speaker;
        public string Text;
        

        public override bool CanEnter()
        {
            return true; 
        }

        public override void OnEnter(DialogueManager manager)
        {
            manager.ShowSentence(this);
        }
    }
}
