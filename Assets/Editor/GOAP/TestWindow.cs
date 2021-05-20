using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using GOAP;

public class TestWindow : EditorWindow
{
    public static TestWindow Instance { get; private set; }

    private ObjectField m_TestField;
    private TextField m_textField;

    private ToolbarButton m_someButton;

    MonoScript m_monoObject;
    MonoScript m_previous;

    object m_currentDisplayObject = null;
    Type m_currentObjectType;

    List<object> m_additionalLayoutNeeds = null;

    [MenuItem("Window/GOAP Inspector")]
    public static void ShowWindow()
    {
        Instance = CreateWindow<TestWindow>("GOAP Inspector");
        Instance.minSize = new Vector2(200, 200);
    }

    private void OnGUI()
    {
        GUILayout.Label("Select an action or behaviour component", EditorStyles.boldLabel);

        m_monoObject = (MonoScript)EditorGUILayout.ObjectField("GOAP Script", m_monoObject, typeof(MonoScript), false);

        SetupGOAPScript();
        DisplayGOAPScript();

        if (GUILayout.Button("Test Me"))
        {
            Debug.Log("did a thing");
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    void SetupGOAPScript()
    {
        if (m_monoObject == null)
        {
            m_previous = null;
            m_currentObjectType = null;
            return;
        }

        if(m_previous == m_monoObject)
        {
            return;
        }

        var monoType = m_monoObject.GetClass();
        if (monoType.IsSubclassOf(typeof(AIAgentAction)))
        {
            // is an action
            var instance = System.Activator.CreateInstance(monoType);
            AIAgentAction action = (AIAgentAction)instance;
            m_currentDisplayObject = action;
            m_currentObjectType = typeof(AIAgentAction);
        }
        else if (monoType.IsSubclassOf(typeof(BehaviourComponent)))
        {
            InitialiseBehaviour(monoType);
        }
        else
        {
            m_currentObjectType = null;
        }

        if(m_previous != null)
        {
            if (m_previous.GetClass().IsSubclassOf(typeof(BehaviourComponent)))
            {
                //ClearBehaviour();
            }
        }

        m_previous = m_monoObject;
    }

    void DisplayGOAPScript()
    {
        if(m_currentObjectType == typeof(AIAgentAction))
        {
            DisplayAIAgentAction((AIAgentAction)m_currentDisplayObject);
        }
        else if(m_currentObjectType == typeof(BehaviourComponent))
        {
            DisplayBehaviour((BehaviourComponent)m_currentDisplayObject);
        }
    }

    void DisplayWorldState(GOAPWorldState worldState)
    {
        foreach (string name in worldState.GetNames())
        {
            var value = worldState.GetElementValue(name);

            GUILayout.BeginHorizontal();
            GUILayout.Label(name, GUILayout.MaxWidth(150.0f));
            GUILayout.Label(": " + value);
            GUILayout.EndHorizontal();
        }
    }

    void DisplayAIAgentAction(AIAgentAction action)
    {
        GUILayout.Label(action.GetName(), EditorStyles.boldLabel);

        int spaceSize = 10;

        GUILayout.BeginHorizontal();
        GUILayout.Label("Weight", GUILayout.MaxWidth(150.0f));
        GUILayout.Label(action.GetWeight().ToString());
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("AnimTrigger", GUILayout.MaxWidth(150.0f));
        GUILayout.Label(action.GetAnimTrigger());
        GUILayout.EndHorizontal();

        GUILayout.Space(spaceSize);
        GUILayout.Label("Preconditions", EditorStyles.boldLabel, GUILayout.MaxWidth(150.0f));
        var preconditions = action.GetPreconditions();
        DisplayWorldState(preconditions);

        GUILayout.Space(spaceSize);
        GUILayout.Label("Effects", EditorStyles.boldLabel, GUILayout.MaxWidth(150.0f));
        var effects = action.GetEffects();
        DisplayWorldState(effects);
    }

    void InitialiseBehaviour(Type monoType)
    {
        var instance = System.Activator.CreateInstance(monoType);
        BehaviourComponent behaviour = (BehaviourComponent)instance;
        m_currentDisplayObject = behaviour;
        m_currentObjectType = typeof(BehaviourComponent);

        m_additionalLayoutNeeds = new List<object>();

        List<bool> foldoutStatus = new List<bool>();
        m_additionalLayoutNeeds.Add(foldoutStatus);

        foreach(var act in behaviour.actionList)
        {
            foldoutStatus.Add(false);
        }
    }

    void DisplayBehaviour(BehaviourComponent behaviour)
    {
        List<bool> foldOutStatus = (List<bool>)m_additionalLayoutNeeds[0];

        int spaceSize = 10;

        GUILayout.Label("Required Selfish Needs", EditorStyles.boldLabel);
        GOAPWorldState worldState = behaviour.requiredSelfishNeeds;
        DisplayWorldState(worldState);

        GUILayout.Space(spaceSize);

        // display actions
        for(int i = 0; i < behaviour.actionList.Count; i++)
        {
            var action = behaviour.actionList[i];
            foldOutStatus[i] = EditorGUILayout.Foldout(foldOutStatus[i], action.GetName());

            if(foldOutStatus[i])
            {
                DisplayAIAgentAction((AIAgentAction)action);
            }
            //GUILayout.Label(action.GetName(), EditorStyles.boldLabel);
        }
    }    
}
