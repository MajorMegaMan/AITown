using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWood : GOAPAction
{
    public PickUpWood()
    {
        preconditions.CreateElement(WorldValues.holdingWood, false);
        preconditions.CreateElement(WorldValues.holdingAxe, true);
        effects.CreateElement(WorldValues.holdingWood, true);

        name = "Pick Up Wood";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdingWood, true);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        // This is where the logic to find a tree would go but right now it is just using a debug value for testing
        agent.actionObject = agent.treeTarget.gameObject;
        agent.m_actionTargetLocation = agent.treeTarget.position;
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
    }
}
