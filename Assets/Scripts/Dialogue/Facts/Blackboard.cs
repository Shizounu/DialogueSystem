using System.Collections.Generic;
using UnityEngine;
using Dialogue.Helpers;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "new Blackboard", menuName = "Dialogue/Blackboard")]
    public class Blackboard : ScriptableObject
    {
        public SerializableDictionary<string, int> Facts = new();
    }
}
