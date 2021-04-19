using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : GOAPAction
{
    public EatFood()
    {
        preconditions.CreateElement(WorldValues.holdingFood, true);
        effects.CreateElement(WorldValues.holdingFood, false);
        effects.CreateElement(WorldValues.hunger, 100.0f);
        effects.CreateElement(WorldValues.hasProcessedHunger, false);

        name = "Eat Food";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdingFood, false);
        state.SetElementValue(WorldValues.hunger, 100.0f);
        state.SetElementValue(WorldValues.hasProcessedHunger, false);
    }

    public override ActionState PerformAction(GOAPWorldState worldState)
    {
        AddEffects(worldState);
        return ActionState.completed;
    }

    public override void EnterAction(GOAPAgent agent)
    {
        Debug.Log(name);
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return true;
    }
}
