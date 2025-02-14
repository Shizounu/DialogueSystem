using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;

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
