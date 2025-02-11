using Meryel.UnityCodeAssist.Synchronizer.Model;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using CustomEditors.Dialgoue.Utilities;
using CustomEditors.Dialgoue.Windows;

namespace CustomEditors.Dialgoue.Elements {
    public abstract class BaseNode : Node
    {
        public string SlideName;
        protected DialogueGraphView graphView;

        public virtual void Initialize(Vector2 position, DialogueGraphView graphView)
        {
            SlideName = "SlideName";

            SetPosition(new Rect(position, Vector2.zero));

            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");

            this.graphView = graphView;
        }

        public virtual void Draw()
        {
            MakeTitle();

            MakeMain();

            MakeInput();

            MakeOutput();

            MakeExtension();

            //Redraws visuals
            RefreshExpandedState();
        }

        #region Section Constructors
        protected virtual void MakeTitle()
        {
            TextField slideNameTextField = ElementUtility.CreateTextField(SlideName);
            titleContainer.Insert(0, slideNameTextField);

            slideNameTextField.AddClasses(
                "ds-node__text-field",
                "ds-node__text-field__hidden",
                "ds-node__filename-text-field"
            );
        }

        protected virtual void MakeInput()
        {
            Port inputPort = this.CreatePort("Incoming", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);
            inputContainer.Add(inputPort);
        }
        protected virtual void MakeMain() { }
        protected virtual void MakeOutput() { }
        protected virtual void MakeExtension() { }
        #endregion
    }
}
