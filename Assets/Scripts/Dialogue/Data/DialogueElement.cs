using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

#endif

namespace Dialogue.Data
{
    [System.Serializable]
    public abstract class DialogueElement {
        public List<DialogueElement> Branches = new();    
        public abstract bool CanEnter();
        public abstract void OnEnter();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>If null is returned exit the dialogue</returns>
        public DialogueElement GetNextElement() {
            foreach (var Branch in Branches)
                if(Branch.CanEnter())
                    return Branch;
            return null; 
        }

#if UNITY_EDITOR
        public Vector2Int NodePosition;
#endif
    }
}
