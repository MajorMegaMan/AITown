using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWood : GOAPAction
{
    public PickUpWood()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItem.nothing);
        preconditions.CreateElement(WorldValues.woodAvailable, true);
        //preconditions.CreateElement(WorldValues.holdingAxe, true);
        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItem.wood);
        effects.CreateElement(WorldValues.woodAvailable, false);

        name = "Pick Up Wood";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItem.wood);
        state.SetElementValue(WorldValues.woodAvailable, false);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        // find wood to pick up
        // currently just represented by a bool
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        //return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
        return true;
    }
}
