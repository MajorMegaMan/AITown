using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreWood : GOAPAction
{
    public StoreWood()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.wood);

        preconditions.CreateElement(WorldValues.isHoldingItem, true);
        effects.CreateElement(WorldValues.isHoldingItem, false);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        effects.CreateElement(WorldValues.storedWood, 1);

        name = "Store Wood";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        var data = state.GetData(WorldValues.storedWood);
        int woodVal = data.ConvertValue<int>();
        woodVal++;
        data.value = woodVal;

        state.CreateElement(WorldValues.isHoldingItem, false);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        AddEffects(worldState);
        
        // Get held wood object and destroy it
        var data = worldState.GetData(WorldValues.holdItemObject);
        GameObject woodObject = (GameObject)(data.value);

        //GameObject woodObject = worldState.GetElementValue<GameObject>(WorldValues.holdItemObject);
        GameObject.Destroy(woodObject);

        data.value = null;

        return ActionState.completed;
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        // This is where the logic to find a tree would go but right now it is just using a debug value for testing
        agent.actionObject = agent.woodStoreTarget.gameObject;
        agent.m_actionTargetLocation = agent.woodStoreTarget.position;
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
    }
}
