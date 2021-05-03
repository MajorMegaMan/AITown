using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class EatFood : GOAPAction<GameObject>
{
    public EatFood()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.food);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        effects.CreateElement(WorldValues.hunger, 100.0f);
        effects.CreateElement(WorldValues.hasProcessedHunger, false);

        name = "Eat Food";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        state.SetElementValue(WorldValues.hunger, 100.0f);
        state.SetElementValue(WorldValues.hasProcessedHunger, false);
    }

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        AddEffects(worldState);

        // Get held wood object and destroy it
        var data = worldState.GetData(WorldValues.holdItemObject);
        GameObject foodObject = (GameObject)(data.value);

        //GameObject woodObject = worldState.GetElementValue<GameObject>(WorldValues.holdItemObject);
        GameObject.Destroy(foodObject);

        data.value = null;

        return ActionState.completed;
    }

    public override bool EnterAction(U_GOAPAgent agent)
    {
        return true;
    }

    public override bool IsInRange(U_GOAPAgent agent)
    {
        return true;
    }
}
