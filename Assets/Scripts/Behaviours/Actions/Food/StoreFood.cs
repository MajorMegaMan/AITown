using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreFood : GOAPAction
{
    public StoreFood()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.food);

        preconditions.CreateElement(WorldValues.isHoldingItem, true);
        effects.CreateElement(WorldValues.isHoldingItem, false);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        effects.CreateElement(WorldValues.storedFood, 1);

        name = "Store Food";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        var data = state.GetData(WorldValues.storedFood);
        int foodVal = data.ConvertValue<int>();
        foodVal++;
        data.value = foodVal;

        state.CreateElement(WorldValues.isHoldingItem, false);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
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

    public override bool EnterAction(GOAPAgent agent)
    {
        // This is where the logic to find a food storage would go but right now it is just using a debug value for testing
        agent.actionObject = agent.foodStoreTarget.gameObject;
        agent.m_actionTargetLocation = agent.foodStoreTarget.position;
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
    }
}
