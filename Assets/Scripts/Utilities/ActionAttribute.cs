using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;

public class ActionAttribute : PropertyAttribute
{
    public Type actionType = typeof(AIAgentAction);
}


[CustomPropertyDrawer(typeof(ActionAttribute))]
public class ActionDrawer : PropertyDrawer
{
    int currentPickerWindow;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        Debug.Log("Created something");
        return base.CreatePropertyGUI(property);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ActionAttribute attrib = attribute as ActionAttribute;

        var classType = attrib.actionType;

        var obj = property.objectReferenceValue;

        EditorGUI.ObjectField(position, property, label);


        string commandName = Event.current.commandName;
        if (commandName == "ObjectSelectorUpdated")
        {
            //create a window picker control ID
            currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive) + 100;

            EditorGUIUtility.ShowObjectPicker<MonoScript>(null, false, "l:Action", currentPickerWindow);

            Debug.Log("Updated a window");
        }

        //if(obj != null)
        //{
        //    Debug.Log(obj);
        //    Debug.Log("this is a thing, hey add another axe");
        //}


    }
}