using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAxe : GOAPAction
{
    public DropAxe()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItem.axe);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItem.nothing);
        effects.CreateElement(WorldValues.axeAvailable, true);

        name = "Drop Axe";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItem.nothing);
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
        agent.actionObject = agent.GetWorldState().GetElementValue<GameObject>(WorldValues.worldAxe);
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return true;
    }
}
