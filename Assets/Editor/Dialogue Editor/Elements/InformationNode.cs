using CustomEditors.Dialgoue.Utilities;
using CustomEditors.Dialgoue.Windows;
using Dialogue.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomEditors.Dialgoue.Elements
{
    public class InformationNode : BaseNode {
        public Dialogue.Data.Blackboard Blackboard;
        public string FactKey;
        public ComparisonOperator ConditionOperator;
        public int Value;

        public override void Initialize(Vector2 position, DialogueGraphView graphView)
        {
            base.Initialize(position, graphView);
            SlideName = "Information";
        }
        protected override void MakeOutput()
        {
            foreach (var item in BranchPorts)
            {
                outputContainer.Add(item.port);
            }
        }

        protected override void MakeExtension()
        {
            Button addPrioPort = ElementUtility.CreateButton("Add Priority", () => CreatePriorityPort(0));
            extensionContainer.Add(addPrioPort);
            extensionContainer.Add(ElementUtility.CreateSOField<Dialogue.Data.Blackboard>("Blackboard",Blackboard, ctx => Blackboard = (Dialogue.Data.Blackboard)ctx.newValue));
            extensionContainer.Add(ElementUtility.CreateTextField(FactKey, "Fact Key", ctx => FactKey = ctx.newValue));
            extensionContainer.Add(ElementUtility.CreateEnumField<ComparisonOperator>(ConditionOperator, "Operator", ctx => ConditionOperator = (ComparisonOperator)ctx.newValue));
            extensionContainer.Add(ElementUtility.CreateIntField(Value, "Value", ctx => Value = ctx.newValue));
        }

        public override DialogueElement GetElement()
        {
            return new Information()
            {
                ID = DialogueData.GetID(),
                Blackboard = this.Blackboard,
                FactKey = this.FactKey,
                Operator = this.ConditionOperator,
                Value = this.Value,
                NodePosition = this.GetPosition()
            };
        }
    }
}
