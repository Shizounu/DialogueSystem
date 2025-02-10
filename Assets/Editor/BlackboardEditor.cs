using UnityEditor;
using Dialogue;
using System.Collections.Generic;
using UnityEngine;
using Dialogue.Helpers;
using System.Linq;
using UnityEngine.Rendering;
using Mono.Cecil;


namespace CustomEditors
{
    [CustomEditor(typeof(Blackboard))]
    public class BlackboardEditor : Editor {
        SimpleEditorTableView<KeyValuePair<string, int>> table;
        string newSignature = string.Empty;



        private void OnEnable()
        {
            table ??= CreateBlackboardTable();
            
        }

        public override void OnInspectorGUI()
        {
            
            table.DrawTableGUI(((Blackboard)target).Facts.ToArray(), EditorGUIUtility.singleLineHeight * 10);
            
            EditorGUILayout.BeginHorizontal();
            newSignature = EditorGUILayout.TextField("Signature", newSignature);
            
            if (GUILayout.Button("Add") && !IsKeyUnique(newSignature)) {
                ((Blackboard)target).Facts.Add(newSignature, 0);
            }
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
        
        private SimpleEditorTableView<KeyValuePair<string,int>> CreateBlackboardTable()
        {
            SimpleEditorTableView<KeyValuePair<string, int>> table = new();
            table.AddColumn("Signature", 20, (rect, item) =>
            {
                rect.xMin += 10;
               EditorGUI.LabelField(rect, item.Key);
            }).SetSorting((a, b) => string.Compare(a.Key, b.Key, System.StringComparison.Ordinal));

            table.AddColumn("Value", 100, (rect, item) =>
            {
                int temp = EditorGUI.DelayedIntField(rect, item.Value);
                if(temp != item.Value)
                {
                    
                    ((Blackboard)target).Facts[item.Key] = temp;

                }
            });

            table.AddColumn("Remove", 100, (rect, item) =>
            {
                if(GUI.Button(rect, "Remove")){
                    ((Blackboard)target).Facts.Remove(item);
                }
            }).SetAllowToggleVisibility(true);

            return table;
        }


        
        

        private bool IsKeyUnique(string key)
        {
            return ((Blackboard)target).Facts.ContainsKey(key);
        }
    }
}
