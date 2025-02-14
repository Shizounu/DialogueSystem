using Dialogue.Helpers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

namespace Dialogue.Data
{
    [System.Serializable]
    public abstract class DialogueElement {
        public string ID;
        public List<(int, string)> Branches = new();  
        public abstract bool CanEnter();
        public abstract void OnEnter();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>If null is returned exit the dialogue</returns>
        public DialogueElement GetNextElement(DialogueData dialogue) {
            List<(int, string)> copy = new(Branches);
            copy.Sort((a, b) => (a.Item1 - b.Item1));

            while (copy.Count > 0) {
                List<(int, string)> curPrio = GetElementsWithPriority(copy.Max(ctx => ctx.Item1));
                foreach (var cur in curPrio)
                    if (dialogue.GetElement(cur.Item2).CanEnter())
                        return dialogue.GetElement(cur.Item2);
                    else
                        copy.Remove(cur);
            }


            return null;
        }

        private List<(int, string)> GetElementsWithPriority(int priority)
        {
            return Branches.Where(ctx => ctx.Item1 == priority).ToList();
        }

#if UNITY_EDITOR
        public Rect NodePosition;
#endif
    }
}
