using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class TestWindow : EditorWindow
{
    public static TestWindow Instance { get; private set; }

    private ObjectField m_TestField;

    [MenuItem("Window/Test Window")]
    public static void ShowWindow()
    {
        Instance = CreateWindow<TestWindow>("Test Window");
        Instance.minSize = new Vector2(200, 200);
    }

    private void DrawField()
    {
        Toolbar toolbar = new Toolbar();
        m_TestField = new ObjectField("Whateverthefuck") { objectType = typeof(MonoScript) };

        m_TestField.RegisterValueChangedCallback(ctx =>
        {
            if (m_TestField.value != null)
            {
                MonoScript myObject = (MonoScript)m_TestField.value;
                Type myClass = myObject.GetClass();

                if (!myClass.IsSubclassOf(typeof(AIAgentAction)))
                {
                    m_TestField.value = null;
                    Debug.LogWarning("Fuck you.");
                }
            }
        });
        toolbar.Add(m_TestField);

        rootVisualElement.Add(toolbar);
    }

    private void OnGUI()
    {

    }

    private void OnEnable()
    {
        DrawField();
    }

    private void OnDisable()
    {

    }
}
