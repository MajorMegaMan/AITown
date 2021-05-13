using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "TestyBoy", menuName = "Tests/FirstGuy")]
public class TestScriptable : ScriptableObject
{
    
    public MonoScript script;

    private void OnValidate()
    {
        Thing();
    }

    void Thing()
    {
        if(script == null)
        {
            return;
        }

        var type = script.GetClass();

        if(type.IsSubclassOf(typeof(AIAgentAction)))
        {
            // AIAction
            Debug.Log("all good");
        }
        else
        {
            Debug.LogWarning("Script is not AIAgentActionType");
            script = default;
        }
    }

}
