using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAxe : GOAPAction
{
    public DropAxe()
    {
        preconditions.CreateElement(WorldValues.holdingAxe, true);
        effects.CreateElement(WorldValues.holdingAxe, false);
        effects.CreateElement(WorldValues.axeAvailable, true);

        name = "Drop Axe";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdingAxe, false);
        state.SetElementValue(WorldValues.axeAvailable, true);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        var axeItem = agent.actionObject.GetComponent<HoldableItem>();
        axeItem.DetachObject();

        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return true;
    }
}
