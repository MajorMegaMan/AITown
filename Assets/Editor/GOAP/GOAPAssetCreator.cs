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

        [MenuItem("Assets/Create/GOAP/Behaviour Initialiser", false, 0)]
        static void CreateInitialiser()
        {
            FindPathAndName(GOAPAssetConfig.globalPath + GOAPAssetConfig.behaviourCompPath, GOAPAssetConfig.behaviourCompName, out string finalCopyPath, out string finalName);

            List<string> lines = new List<string>();

            BehaviourComponentInitialiserTemplate(lines, finalName);

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
            lines.Add("using U_GOAPAgentAction = GOAP.GOAPAgentAction<UnityEngine.GameObject>;");
            lines.Add("");
            lines.Add("public class " + name + " : BehaviourComponentInitialiser");
            lines.Add("{");
            lines.Add("    public " + name + "()");
            lines.Add("    {");
            lines.Add("        // Your Initialisation code goes here");
            lines.Add("");
            lines.Add("        // Add to actionList");
            lines.Add("        // actionList.Add(act);");
            lines.Add("");
            lines.Add("        // Create requiredWorldStates");
            lines.Add("        // requiredWorldStates = new GOAPWorldState();");
            lines.Add("        // requiredWorldStates.CreateElement(string name, object value);");
            lines.Add("");
            lines.Add("        // Initialise Updater");
            lines.Add("        // updater = new BehaviourUpdater();");
            lines.Add("    }");
            lines.Add("}");
        }
    }
}
