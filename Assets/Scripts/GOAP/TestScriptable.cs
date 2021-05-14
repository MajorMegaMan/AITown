using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "TestyBoy", menuName = "Tests/FirstGuy")]
public class TestScriptable : ScriptableObject
{
    public MonoScript script;

    public List<MonoScript> actions;
    public MonoScript behaviourUpdater;

    private void OnValidate()
    {
        ValidateUpdater();
        ValidateActions();
    }

    void ValidateActions()
    {
        if(actions.Count == 0)
        {
            return;
        }

        for(int i = 0; i < actions.Count; i++)
        {
            if (actions[i] == null)
            {
                return;
            }

            var type = actions[i].GetClass();

            if (!type.IsSubclassOf(typeof(AIAgentAction)))
            {
                Debug.LogWarning("Script is not " + typeof(AIAgentAction));
                actions[i] = default;
            }
        }
    }

    void ValidateUpdater()
    {
        ValidateMonoScript(ref behaviourUpdater, typeof(BehaviourUpdater), "Script is not BehaviourUpdater");
    }

    void ValidateMonoScript(ref MonoScript mono, System.Type classType, string message)
    {
        if (mono == null)
        {
            return;
        }

        var type = mono.GetClass();

        if (!type.IsSubclassOf(classType))
        {
            Debug.LogWarning(message);
            mono = default;
        }
    }
}
