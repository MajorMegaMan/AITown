using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace GOAPAssetCreator
{
    static class GOAPAssetCreator
    {
        const string copyFileType = ".cs";

        [MenuItem("Assets/Create/GOAP/Behaviour Component", false, 1)]
        static void CreateComponent()
        {
            FindPathAndName(GOAPAssetConfig.globalPath + GOAPAssetConfig.behaviourCompPath, GOAPAssetConfig.behaviourCompName, out string finalCopyPath, out string finalName);

            List<string> lines = new List<string>();

            BehaviourComponentInitialiserTemplate(lines, finalName);

            CreateScriptAsset(lines, finalCopyPath);
        }

        [MenuItem("Assets/Create/GOAP/Agent Action", false, 0)]
        static void CreateAction()
        {
            FindPathAndName(GOAPAssetConfig.globalPath + GOAPAssetConfig.agentActionPath, GOAPAssetConfig.agentActionName, out string finalCopyPath, out string finalName);

            List<string> lines = new List<string>();

            AgentActionTemplate(lines, finalName);

            CreateScriptAsset(lines, finalCopyPath);
        }

        static void FindPathAndName(string path, string name, out string finalPath, out string finalName)
        {
            finalPath = path + name + copyFileType;
            finalName = name;

            bool canCreate = false;
            int tryCount = 0;
            while (!canCreate)
            {
                if (File.Exists(finalPath))
                {
                    tryCount++;
                    finalName = name + tryCount;
                    finalPath = path + finalName + copyFileType;
                }
                else
                {
                    canCreate = true;
                }
            }
        }

        static void CreateScriptAsset(List<string> lines, string path)
        {
            using (StreamWriter outFile = new StreamWriter(path))
            {
                foreach (string line in lines)
                {
                    outFile.WriteLine(line);
                }
            }

            AssetDatabase.Refresh();
        }

        static void BehaviourComponentInitialiserTemplate(List<string> lines, string name)
        {
            lines.Add("using System.Collections;");
            lines.Add("using System.Collections.Generic;");
            lines.Add("using UnityEngine;");
            lines.Add("using GOAP;");
            lines.Add("");
            lines.Add("public class " + name + " : BehaviourComponent");
            lines.Add("{");
            lines.Add("    public override void Init()");
            lines.Add("    {");
            lines.Add("        // Your Initialisation code goes here");
            lines.Add("");
            lines.Add("        // Add to actionList");
            lines.Add("        // actionList.Add(act);");
            lines.Add("");
            lines.Add("        // Create requiredWorldStates");
            lines.Add("        // requiredWorldStates.CreateElement(string name, object value);");
            lines.Add("    }");
            lines.Add("");
            lines.Add("    public override bool HasFindGoal()");
            lines.Add("    {");
            lines.Add("        return false;");
            lines.Add("    }");
            lines.Add("");
            lines.Add("    //public override GoalStatus FindGoal(GOAPWorldState agentWorldState, GOAPWorldState targetGoal, GoalStatus currentGoalStatus)");
            lines.Add("    //{");
            lines.Add("    //    // If HasFindGoal() returns true then this should be filled with logic that will populate the target goal");
            lines.Add("    //    // and then return the relevant goal status.");
            lines.Add("    //");
            lines.Add("    //    // return currentGoalStatus to continue searching for a targetGoal");
            lines.Add("    //    return currentGoalStatus;");
            lines.Add("    //}");
            lines.Add("");
            lines.Add("    public override bool HasUpdate()");
            lines.Add("    {");
            lines.Add("        return false;");
            lines.Add("    }");
            lines.Add("");
            lines.Add("    //public override void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds)");
            lines.Add("    //{");
            lines.Add("    //    // If HasUpdate() returns true then this should be filled with logic that would update the agentsSelfishNeeds");
            lines.Add("    //}");
            lines.Add("}");
        }

        static void AgentActionTemplate(List<string> lines, string name)
        {
            lines.Add("using System.Collections;");
            lines.Add("using System.Collections.Generic;");
            lines.Add("using UnityEngine;");
            lines.Add("using GOAP;");
            lines.Add("");
            lines.Add("using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;");
            lines.Add("");
            lines.Add("public class " + name + " : AIAgentAction");
            lines.Add("{");
            lines.Add("    public " + name + "()");
            lines.Add("    {");
            lines.Add("        // Initialisation goes here");
            lines.Add("");
            lines.Add("        //preconditions.CreateElement(string name, object value);");
            lines.Add("    ");
            lines.Add("        //effects.CreateElement(string name, object value);");
            lines.Add("    ");
            lines.Add("        //name = \"ExampleName\";");
            lines.Add("        //animTrigger = \"ExampleString\";");
            lines.Add("    }");
            lines.Add("    ");
            lines.Add("    public override void AddEffects(GOAPWorldState state)");
            lines.Add("    {");
            lines.Add("        // Logic for effects go here.");
            lines.Add("    }");
            lines.Add("    ");
            lines.Add("    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)");
            lines.Add("    {");
            lines.Add("        // Logic for performing actions goes here.");
            lines.Add("        // This could include waiting for an animation to finish before completeing");
            lines.Add("        return ActionState.completed;");
            lines.Add("    }");
            lines.Add("    ");
            lines.Add("    public override bool EnterAction(U_GOAPAgent agent)");
            lines.Add("    {");
            lines.Add("        // Logic that may be needed for the agent would go here.");
            lines.Add("        // This could be something like searching for the closest tree if an agent wanted to chop wood.");
            lines.Add("        // The agent would need to assign");
            lines.Add("    ");
            lines.Add("        // These may be helpful");
            lines.Add("        //GameObject agentGameObject = agent.GetAgentObject();");
            lines.Add("        //AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();");
            lines.Add("    ");
            lines.Add("        //aiAgent.actionObject = FoundTreeObject;");
            lines.Add("    ");
            lines.Add("        // returns true if setup was successful.");
            lines.Add("        // with this example, false could mean that no trees were available");
            lines.Add("        return true;");
            lines.Add("    }");
            lines.Add("    ");
            lines.Add("    public override bool IsInRange(U_GOAPAgent agent)");
            lines.Add("    {");
            lines.Add("        //Logic to check if an action is in range to perform an action goes here");
            lines.Add("        //GameObject agentGameObject = agent.GetAgentObject();");
            lines.Add("        //AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();");
            lines.Add("    ");
            lines.Add("        // return true if an agent is in range.");
            lines.Add("        // no Calculations are needed if the agent can perform an action from any range.");
            lines.Add("        return true;");
            lines.Add("    }");
            lines.Add("}");
        }
    }
}
