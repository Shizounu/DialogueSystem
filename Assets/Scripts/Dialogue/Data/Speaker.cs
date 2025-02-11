using UnityEngine;

namespace Dialogue.Data {
    [CreateAssetMenu(fileName = "new Speaker", menuName = "Dialogue/Speaker")]
    public class Speaker : ScriptableObject
    {
        public string Name = "new Speaker";
        public Color NameColor = Color.white; 
    }
}
