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

        #region Saving
        public static void Save(string DialogueName, DialogueGraphView graphView)
        {
            //Prepare Folder
            if (!AssetDatabase.AssetPathExists($"Assets/Prefabs/Dialogue/Dialogues/{DialogueName}")) {
                AssetDatabase.CreateFolder("Assets/Prefabs/Dialogue/Dialogues", DialogueName);
            }
            //Load in file
            DialogueData data = (DialogueData)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Dialogue/Dialogues/{DialogueName}/{DialogueName}.asset", typeof(DialogueData));
            if(data == null)
                data = ScriptableObject.CreateInstance<DialogueData>();
            data.Clear();

            //Save Elements
            SaveNodes(graphView, data);
            data.EntryElements = GetBranches(graphView.entryNode);


            //Save to file
            if (!AssetDatabase.AssetPathExists($"Assets/Prefabs/Dialogue/Dialogues/{DialogueName}/{DialogueName}.asset"))
                AssetDatabase.CreateAsset(data, $"Assets/Prefabs/Dialogue/Dialogues/{DialogueName}/{DialogueName}.asset");
            AssetDatabase.SaveAssets();
        }

        private static void SaveNodes(DialogueGraphView graphView, DialogueData data)
        {
            foreach (var node in graphView.NodeCache) {
                DialogueElement element = node.Value.GetElement();
                if (element == null)
                    continue;
                element.Branches = GetBranches(node.Value);
                data.Elements.Add(element);
            }
        }

        private static List<PriorityIDTuple> GetBranches(BaseNode curNode) {
            List<PriorityIDTuple> res = new();
            foreach (var port in curNode.BranchPorts)
                for (int i = 0; i < port.port.connections.Count(); i++)
                    res.Add(new (port.priority, ((BaseNode)port.port.connections.ToList()[i].input.node).UID));
            return res;
        }
        #endregion

        #region Loading
        public static void Load(DialogueData dialogueData, DialogueGraphView graphView) {
            //Load All Elements
            foreach (var element in dialogueData.Elements)
                AddNode(element, graphView);
            //Load all connections between nodes
            foreach (var element in dialogueData.Elements)
                LoadConnections(element, graphView);
            //Load Entry connections
            foreach (var item in dialogueData.EntryElements)
                MakeConnection(graphView.entryNode, graphView.NodeCache[item.ID], item.Priority, graphView);
        }
        public static void LoadConnections(DialogueElement elem, DialogueGraphView graphView) {
            foreach (var item in elem.Branches)
                MakeConnection(graphView.NodeCache[elem.ID], graphView.NodeCache[item.ID], item.Priority, graphView);
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
        #endregion
    }
}
