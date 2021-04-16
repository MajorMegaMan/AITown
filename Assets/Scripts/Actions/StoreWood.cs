using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreWood : GOAPAction
{
    public StoreWood()
    {
        preconditions.CreateElement(WorldValues.holdingWood, true);
        effects.CreateElement(WorldValues.holdingWood, false);
        effects.CreateElement(WorldValues.storedWood, 1);

        name = "Store Wood";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        base.AddEffects(state);
        state.SetElementValue(WorldValues.holdingWood, false);
        var data = state.GetData(WorldValues.storedWood);
        int woodVal = data.ConvertValue<int>();
        woodVal++;
        data.value = woodVal;
    }

    public override ActionState PerformAction(GOAPWorldState worldState)
    {
        AddEffects(worldState);
        return ActionState.completed;
    }

    public override void EnterAction(AIController agent)
    {
        // This is where the logic to find a tree would go but right now it is just using a debug value for testing
        agent.actionObject = agent.woodStoreTarget.gameObject;
        agent.m_actionTargetLocation = agent.woodStoreTarget.position;
    }

    public override bool IsInRange(AIController agent)
    {
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
    }
}
