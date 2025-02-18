using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Shizounu.Library.Editor
{
    [CustomEditor(typeof(ScriptableArchitecture.ScriptableEvent), editorForChildClasses:true)]
    public class ScriptableEventEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            ScriptableArchitecture.ScriptableEvent e = target as ScriptableArchitecture.ScriptableEvent;
            if(GUILayout.Button("Invoke Event"))
                e.Invoke();    
        }
    }
 
   
}
