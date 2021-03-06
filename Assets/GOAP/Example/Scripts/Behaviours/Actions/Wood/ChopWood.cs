using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class ChopWood : AIAgentAction
{
    static List<GameObject> instantiatedWoodObjects;
    static GameObject woodPrefab;

    float chopTime = 4.0f;

    public ChopWood()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.axe);
        effects.CreateElement(WorldValues.woodAvailable, true);
        effects.CreateElement(WorldValues.worldWoodCount, +1);

        name = "Chop Wood";
        animTrigger = name;
    }

    public static void SetWoodObjectsList(List<GameObject> woodObjects)
    {
        instantiatedWoodObjects = woodObjects;
    }

    public static void SetWoodPrefab(GameObject prefab)
    {
        woodPrefab = prefab;
    }

    public override void AddEffects(GOAPWorldState state)
    {
        state.SetElementValue(WorldValues.woodAvailable, true);

        var woodCountData = state.GetData(WorldValues.worldWoodCount);
        int value = woodCountData.ConvertValue<int>();
        value++;
        woodCountData.value = value;
    }

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        if(aiAgent.actionTimer > chopTime)
        {
            // Instantiate wood object
            GameObject newWood = GameObject.Instantiate(woodPrefab);
            newWood.transform.position = aiAgent.transform.position + (Vector3.up * 2.5f);
            instantiatedWoodObjects.Add(newWood);
            return ActionState.completed;
        }
        else
        {
            aiAgent.actionTimer += Time.deltaTime;
            return ActionState.performing;
        }
    }

    public override bool EnterAction(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // find tree to chop
        // debug values at the moment
        aiAgent.actionObject = WorldValues.treeTarget.gameObject;
        aiAgent.m_actionTargetLocation = WorldValues.treeTarget.position;
        return true;
    }

    public override bool IsInRange(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // is tree in range
        return (aiAgent.transform.position - aiAgent.actionObject.transform.position).magnitude < aiAgent.stoppingDistance;
    }
}
