using CustomEditors.Dialgoue.Utilities;
using CustomEditors.Dialgoue.Windows;
using Dialogue.Data;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomEditors.Dialgoue.Elements
{
    public class SentenceNode : BaseNode
    {
        public string Text;
        public Speaker Speaker;
        public override void Initialize(Vector2 position, DialogueGraphView graphView)
        {
            base.Initialize(position, graphView);
            SlideName = "Sentence";
            Text = "";
        }
        protected override void MakeOutput()
        {
            Port exitPort = this.CreatePort("Outgoing", Orientation.Horizontal, Direction.Output, Port.Capacity.Multi);
            outputContainer.Add(exitPort);
        }

        protected override void MakeExtension()
        {
            //Extension container
            VisualElement customDataContainer = new();
            customDataContainer.AddToClassList("ds-node__custom-data-container");
            Foldout textFoldout = ElementUtility.CreateFoldout("Sentence Text");
            ObjectField speakerField = ElementUtility.CreateSOField<Speaker>("Speaker", ctx => Speaker = (Speaker)ctx.newValue);
            TextField textTextField = ElementUtility.CreateTextArea(Text, null, ctx => Text = ctx.newValue);

            textTextField.AddClasses(
                "ds-node__text-field",
                "ds-node__quote-text-field"
            );

            textFoldout.Add(speakerField);
            textFoldout.Add(textTextField);
            customDataContainer.Add(textFoldout);
            extensionContainer.Add(customDataContainer);
        }
    }
}
