using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class DropAxe : AIAgentAction
{
    public DropAxe()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.axe);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        effects.CreateElement(WorldValues.axeAvailable, true);

        name = "Drop Axe";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        state.SetElementValue(WorldValues.axeAvailable, true);
    }

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        var axeItem = aiAgent.actionObject.GetComponent<HoldableItem>();
        axeItem.DetachObject();

        worldState.SetElementValue(WorldValues.holdItemObject, null);

        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        aiAgent.actionObject = agent.GetWorldState().GetElementValue<GameObject>(WorldValues.worldAxe);
        return true;
    }

    public override bool IsInRange(U_GOAPAgent agent)
    {
        return true;
    }
}
