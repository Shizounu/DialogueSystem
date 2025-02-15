using CustomEditors.Dialgoue.Utilities;
using CustomEditors.Dialgoue.Windows;
using Dialogue.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

namespace CustomEditors.Dialgoue.Elements
{
    public class ConditionalNode : BaseNode
    {
        public Dialogue.Data.Blackboard Blackboard;
        public string FactKey; 
        public ConditionOperator ConditionOperator;
        public int Value;
        public override void Initialize(Vector2 position, DialogueGraphView graphView)
        {
            base.Initialize(position, graphView);
            SlideName = "Conditional";
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
            extensionContainer.Add(ElementUtility.CreateTextField(FactKey, "Fact Key", ctx =>  FactKey = ctx.newValue));
            extensionContainer.Add(ElementUtility.CreateEnumField<ConditionOperator>(ConditionOperator, "Operator", ctx => ConditionOperator = (ConditionOperator)ctx.newValue));
            extensionContainer.Add(ElementUtility.CreateIntField(Value, "Value", ctx => Value = ctx.newValue));
        }

        public override DialogueElement GetElement()
        {
            return new Conditional()
            {
                ID = DialogueData.GetID(),
                Blackboard = this.Blackboard,
                FactKey = this.FactKey,
                Operator = this.ConditionOperator,
                Value = this.Value,
                NodePosition = this.GetPosition()
            };
        }

        public override void LoadData(DialogueElement element)
        {
            Blackboard = ((Conditional)element).Blackboard;
            FactKey = ((Conditional)element).FactKey;
            ConditionOperator = ((Conditional)element).Operator;
            Value = ((Conditional)element).Value;
        }
    }

}
