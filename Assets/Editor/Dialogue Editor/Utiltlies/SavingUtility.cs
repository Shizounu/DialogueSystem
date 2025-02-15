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

            //Validate that the tree is connected at all to the entry
            bool res = false; 
            foreach (var item in graphView.entryNode.BranchPorts)
                res = res || item.port.connected;
            if (!res)
            {
                Debug.LogWarning("Starting node unconnected, not saving");
                return;
            }

            
            List<(int, BaseNode)> nodes = GetBranches(graphView.entryNode);
            foreach (var node in nodes)
                data.EntryElements.Add(new (node.Item1, GetElements(node.Item2, ref data.Elements)));                
            
            AssetDatabase.CreateAsset(data, $"Assets/Prefabs/Dialogue/Dialogues/{DialogueName}/{DialogueName}.asset");
            AssetDatabase.SaveAssets();
        }

        private static List<(int, BaseNode)> GetBranches(BaseNode curNode) {
            List<(int, BaseNode)> res = new();
            foreach (var port in curNode.BranchPorts) {
                for (int i = 0; i < port.port.connections.Count(); i++)
                    res.Add((port.priority,(BaseNode)port.port.connections.ToList()[i].input.node));
            }
            return res;
        }
        private static string GetElements(BaseNode curNode, ref List<DialogueElement> elements) {
            DialogueElement elem = curNode.GetElement();
            List<(int, BaseNode)> branches = GetBranches(curNode);
            elements.Add(elem);
            foreach (var node in branches)
                elem.Branches.Add((node.Item1, GetElements(node.Item2, ref elements)));
            return elem.ID;
        }



        public static void Load(DialogueData dialogueData, DialogueGraphView graphView) {
            foreach (var element in dialogueData.EntryElements) {
                BaseNode node = LoadTree(dialogueData.GetElement(element.ID), dialogueData, graphView);
                MakeConnection(graphView.entryNode, node, element.Priority, graphView);
            }
        }
        public static BaseNode LoadTree(DialogueElement curElement, DialogueData data, DialogueGraphView view)
        {
            BaseNode node = AddNode(curElement, view);
            foreach (var item in curElement.Branches) {
                BaseNode node2 = LoadTree(data.GetElement(item.Item2), data, view);
                MakeConnection(node, node2, item.Item1, view);
            }
            return node;
        }
        public static BaseNode AddNode(DialogueElement elem, DialogueGraphView graphView) {
            switch (elem)
            {
                case Information information:
                    InformationNode infoNode = (InformationNode)graphView.CreateNode(NodeType.Information, information.NodePosition.position, information);

                    infoNode.Blackboard = information.Blackboard;
                    infoNode.FactKey = information.FactKey;
                    infoNode.ConditionOperator = information.Operator;
                    infoNode.Value = information.Value;

                    graphView.AddElement(infoNode);
                    return infoNode;
                case Conditional conditional:
                    ConditionalNode condNode = (ConditionalNode)graphView.CreateNode(NodeType.Condition, conditional.NodePosition.position, conditional);

                    condNode.Blackboard = conditional.Blackboard;
                    condNode.FactKey = conditional.FactKey;
                    condNode.ConditionOperator = conditional.Operator;
                    condNode.Value = conditional.Value;

                    graphView.AddElement(condNode);
                    return condNode;
                case Sentence sentence:
                    SentenceNode sentNode = (SentenceNode)graphView.CreateNode(NodeType.SentenceNode, sentence.NodePosition.position, sentence);

                    sentNode.Speaker = sentence.Speaker;
                    sentNode.Text = sentence.Text;
                    

                    graphView.AddElement(sentNode);
                    return sentNode;

                default:
                    return null;
            }
        }

        public static void MakeConnection(BaseNode from, BaseNode to, int priority, DialogueGraphView graphView)
        {
            graphView.AddElement(from.GetPortWithPriority(priority).port.ConnectTo(to.inputPort));
        }
        
    }
}
