using CustomEditors.Dialgoue.Windows;
using UnityEditor;
using UnityEngine;
using Dialogue.Data;
using System.Linq;
using CustomEditors.Dialgoue.Elements;
using System.Collections.Generic;
namespace CustomEditors.Dialgoue.Utilities
{
    public static class SavingUtility  {
        

        public static void Save(string DialogueName, DialogueGraphView graphView)
        {
            if (!AssetDatabase.AssetPathExists($"Assets/Prefabs/Dialogue/Dialogues/{DialogueName}")) {
                AssetDatabase.CreateFolder("Assets/Prefabs/Dialogue/Dialogues", DialogueName);
            }

            DialogueData data = ScriptableObject.CreateInstance<DialogueData>();

            if (!graphView.entryNode.BranchPorts[0].connected)
            {
                Debug.LogWarning("Starting node unconnected, not saving");
                return;
            }
            data.StartingElement = GetPopulatedTree((BaseNode)graphView.entryNode.BranchPorts[0].connections.ToList()[0].input.node); 
            

            AssetDatabase.CreateAsset(data, $"Assets/Prefabs/Dialogue/Dialogues/{DialogueName}/{DialogueName}.asset");
            AssetDatabase.SaveAssets();
        }

        private static List<BaseNode> GetBranches(BaseNode curNode) {
            List<BaseNode> res = new();
            foreach (var port in curNode.BranchPorts) {
                for (int i = 0; i < port.connections.Count(); i++)
                    res.Add((BaseNode)port.connections.ToList()[i].input.node);
            }
            return res;
        }
        private static DialogueElement GetPopulatedTree(BaseNode curNode) {
            DialogueElement elem = curNode.GetElement();
            List<BaseNode> branchNodes = GetBranches(curNode);
            foreach (var node in branchNodes)
                elem.Branches.Add(GetPopulatedTree(node));
            return elem;
        }

        
    }
}
