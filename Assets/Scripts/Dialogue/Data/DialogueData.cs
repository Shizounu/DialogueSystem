using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.Linq;

namespace Dialogue.Data
{
    public class DialogueData : ScriptableObject
    {
        [SerializeReference] public List<PriorityIDTuple> EntryElements = new(); 
        [SerializeReference] public List<DialogueElement> Elements = new();

        public DialogueElement GetElement(string ID) {
            return Elements.Find(ctx => ctx.ID == ID);
        }
        public static string GetID() {
            return GUID.Generate().ToString();
        }

        public DialogueElement GetStartingElement()
        {
            List<PriorityIDTuple> copy = new(EntryElements);
            copy.Sort((a, b) => (a.Priority - b.Priority));

            while (copy.Count > 0)
            {
                List<PriorityIDTuple> curPrio = GetElementsWithPriority(copy.Max(ctx => ctx.Priority));
                foreach (var cur in curPrio)
                    if (GetElement(cur.ID).CanEnter())
                        return GetElement(cur.ID);
                    else
                        copy.Remove(cur);
            }


            return null;
        }

        private List<PriorityIDTuple> GetElementsWithPriority(int priority)
        {
            return EntryElements.Where(ctx => ctx.Priority == priority).ToList();
        }
    }

    [Serializable]
    public class PriorityIDTuple {
        public PriorityIDTuple(int prio, string ID) {
            this.Priority = prio;
            this.ID = ID;
        }
        public int Priority;
        public string ID; 
    }
}
