using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class DropAxe : AIAgentAction
{
    static List<GameObject> instantiatedAxeObjects;

    public DropAxe()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.axe);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        effects.CreateElement(WorldValues.axeAvailable, true);
        effects.CreateElement(WorldValues.worldAxeCount, +1);

        name = "Drop Axe";
    }

    public static void SetAxeObjectsList(List<GameObject> axeObjects)
    {
        instantiatedAxeObjects = axeObjects;
    }

    public override void AddEffects(GOAPWorldState state)
    {
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        state.SetElementValue(WorldValues.axeAvailable, true);

        int axeCount = state.GetElementValue<int>(WorldValues.worldAxeCount);
        axeCount++;
        state.SetElementValue(WorldValues.worldAxeCount, axeCount);
    }

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        var axeItem = aiAgent.actionObject.GetComponent<HoldableItem>();
        axeItem.DetachObject();

        instantiatedAxeObjects.Add(aiAgent.actionObject);

        worldState.SetElementValue(WorldValues.holdItemObject, null);

        return ActionState.completed;
    }

    public override bool EnterAction(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        aiAgent.actionObject = agent.GetWorldState().GetElementValue<GameObject>(WorldValues.holdItemObject);
        //aiAgent.actionObject = agent.GetWorldState().GetElementValue<GameObject>(WorldValues.worldAxe);
        return true;
    }

    public override bool IsInRange(U_GOAPAgent agent)
    {
        return true;
    }
}
