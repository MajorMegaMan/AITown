using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAxe : GOAPAction
{
    public PickUpAxe()
    {
        preconditions.CreateElement(WorldValues.holdingAxe, false);
        preconditions.CreateElement(WorldValues.axeAvailable, true);
        effects.CreateElement(WorldValues.holdingAxe, true);
        effects.CreateElement(WorldValues.axeAvailable, false);

        name = "Pick Up Axe";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdingAxe, true);
        state.SetElementValue(WorldValues.axeAvailable, false);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        var axeItem = agent.actionObject.GetComponent<HoldableItem>();
        axeItem.AttachObject(agent.gameObject.transform);

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

            agent.actionObject = agentState.GetElementValue<HoldableItem>(WorldValues.worldAxe).gameObject;
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
