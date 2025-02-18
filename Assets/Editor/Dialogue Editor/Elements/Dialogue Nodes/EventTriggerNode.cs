using CustomEditors.Dialgoue.Utilities;
using CustomEditors.Dialgoue.Windows;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Dialogue.Data;
using UnityEngine;
using ScriptableArchitecture;

namespace CustomEditors.Dialgoue.Elements
{
    public class EventTriggerNode : BaseNode
    {
        public ScriptableEvent scriptableEvent;

        public override void Initialize(Vector2 position, DialogueGraphView graphView)
        {
            base.Initialize(position, graphView);
            SlideName = "Event Trigger";
        }
        protected override void MakeOutput()
        {
            foreach (var item in BranchPorts) {
                outputContainer.Add(item.port);
            }
        }

        protected override void MakeExtension()
        {
            Button addPrioPort = ElementUtility.CreateButton("Add Priority", () => CreatePriorityPort(0));
            extensionContainer.Add(ElementUtility.CreateSOField<ScriptableEvent>("Event", scriptableEvent, ctx => scriptableEvent = (ScriptableEvent)ctx.newValue));
        }

        public override DialogueElement GetElement()
        {
            return new EventTrigger() 
            { 
                ID = this.UID, 
                scriptableEvent = this.scriptableEvent, 
                NodePosition = this.GetPosition() 
            };
        }

        public override void LoadData(DialogueElement element)
        {
            scriptableEvent = ((EventTrigger)element).scriptableEvent;
        }
    }
}
