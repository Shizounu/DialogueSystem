using CustomEditors.Dialgoue.Utilities;
using CustomEditors.Dialgoue.Windows;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CustomEditors.Dialgoue.Elements
{
    public class SentenceNode : BaseNode
    {
        public override void Initialize(Vector2 position, DialogueGraphView graphView)
        {
            base.Initialize(position, graphView);
            SlideName = "Sentence";
        }
        protected override void MakeOutput()
        {
            Port exitPort = this.CreatePort("Outgoing", Orientation.Horizontal, Direction.Output, Port.Capacity.Multi);
            outputContainer.Add(exitPort);
        }
    }
}
