using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAxe : GOAPAction
{
    public PickUpAxe()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItem.nothing);
        preconditions.CreateElement(WorldValues.axeAvailable, true);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItem.axe);
        effects.CreateElement(WorldValues.axeAvailable, false);

        name = "Pick Up Axe";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItem.axe);
        state.SetElementValue(WorldValues.axeAvailable, false);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        if(!worldState.GetElementValue<bool>(WorldValues.axeAvailable))
        {
            return ActionState.interrupt;
        }

        var axeItem = agent.actionObject.GetComponent<HoldableItem>();
        axeItem.AttachObject(agent.gameObject.transform);

        worldState.SetElementValue(WorldValues.holdItemObject, agent.actionObject);

        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        // This is where the logic to find an axe would go
        GOAPWorldState agentState = agent.GetWorldState();

        // as the previous journey may have taken too long and an axe is now not available
        if(agentState.GetElementValue<bool>(WorldValues.axeAvailable))
        {
            //HoldableItem axe = agentState.GetElementValue<HoldableItem>(WorldValues.worldAxe);
            //HoldableItem axe = (HoldableItem)(agentState.GetElementValue(WorldValues.worldAxe));
            var obj = agentState.GetElementValue<GameObject>(WorldValues.worldAxe);
            agent.actionObject = obj;
            agent.m_actionTargetLocation = agent.actionObject.transform.position;
            return true;
        }
        else
        {
            agent.actionObject = null;
            agent.m_actionTargetLocation = agent.transform.position;
            return false;
        }
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
    }
}
