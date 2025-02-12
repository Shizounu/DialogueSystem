using CustomEditors.Dialgoue.Elements;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine;
using System;
using UnityEditor;

namespace CustomEditors.Dialgoue.Utilities
{
    public static class ElementUtility {
        public static Button CreateButton(string text, System.Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = text
            };

            return button;
        }

        public static Foldout CreateFoldout(string title, bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = title,
                value = !collapsed
            };

            return foldout;
        }

        public static Port CreatePort(this BaseNode node, string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));
            port.portName = portName;
            if(direction == Direction.Output)
                node.BranchPorts.Add(port);
            return port;
        }

        public static TextField CreateTextField(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textField = new TextField()
            {
                value = value,
                label = label
            };

            if (onValueChanged != null)
                textField.RegisterValueChangedCallback(onValueChanged);

            return textField;
        }

        public static TextField CreateTextArea(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textArea = CreateTextField(value, label, onValueChanged);

            textArea.multiline = true;

            return textArea;
        }

        public static ObjectField CreateSOField<T>(string label, EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null) where T : ScriptableObject
        {
            ObjectField objectField = new ObjectField()
            {
                label = label,
                objectType = typeof(T),
                allowSceneObjects = false
            };
            if (onValueChanged != null)
                objectField.RegisterValueChangedCallback(onValueChanged);
            

            return objectField;
        }
        public static IntegerField CreateIntField(int value = 0, string label = null, EventCallback<ChangeEvent<int>> onValueChanged = null)
        {
            IntegerField intField = new()
            {
                label = label,
                value = value,
            };

            if (onValueChanged != null)
                intField.RegisterValueChangedCallback(onValueChanged);

            return intField;
        }
        public static EnumField CreateEnumField<T>(T value, string label = null, EventCallback<ChangeEvent<Enum>> onValueChanged = null) where T : Enum
        {
            EnumField enumField = new()
            {
                label = label,
                value = value,
                
            };
            enumField.Init(value, true);

            
            

            if (onValueChanged != null)
                enumField.RegisterValueChangedCallback(onValueChanged);

            return enumField;
        }
    }
}

