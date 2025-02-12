using CustomEditors.Dialgoue.Utilities;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


namespace CustomEditors.Dialgoue.Windows
{
    public class DialogueEditorWindow : EditorWindow
    {
        private const string defaultEventName = "new Dialogue";
        private string curEventName = defaultEventName;
        private DialogueGraphView graphView;

        [MenuItem("Shizounu/Dialgoue Editor")]
        public static void Open()
        {
            GetWindow<DialogueEditorWindow>("Event Editor");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddToolbar();
            AddStyles();
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();

            toolbar.Add(ElementUtility.CreateTextField(curEventName, "FileName:", change => curEventName = change.newValue));
            toolbar.Add(ElementUtility.CreateButton("Save", () => DoSave()));

            toolbar.AddStyleSheets("EventEditor/ToolbarStyle.uss");

            rootVisualElement.Add(toolbar);
        }
        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("EventEditor/Variables.uss");
        }

        private void AddGraphView()
        {
            graphView = new DialogueGraphView(this);

            graphView.StretchToParentSize();

            rootVisualElement.Add(graphView);
        }
        private void DoSave()
        {
            SavingUtility.Save(curEventName, graphView);
        }
    }
}
