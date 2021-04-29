using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAxe : GOAPAction
{
    public DropAxe()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.axe);

        preconditions.CreateElement(WorldValues.isHoldingItem, true);
        effects.CreateElement(WorldValues.isHoldingItem, false);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        effects.CreateElement(WorldValues.axeAvailable, true);

        name = "Drop Axe";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        state.SetElementValue(WorldValues.axeAvailable, true);

        state.CreateElement(WorldValues.isHoldingItem, false);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        var axeItem = agent.actionObject.GetComponent<HoldableItem>();
        axeItem.DetachObject();

        worldState.SetElementValue(WorldValues.holdItemObject, null);

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
