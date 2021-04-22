using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFood : GOAPAction
{
    public PickUpFood()
    {
        preconditions.CreateElement(WorldValues.holdingFood, false);
        effects.CreateElement(WorldValues.holdingFood, true);

        name = "Pick Up Food";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdingFood, true);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        // This is where the logic to find a foodSource would go but right now it is just using a debug value for testing
        agent.actionObject = agent.foodBushTarget.gameObject;
        agent.m_actionTargetLocation = agent.foodBushTarget.position;
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
    }
}
