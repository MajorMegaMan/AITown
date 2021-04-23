using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopWood : GOAPAction
{
    public ChopWood()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItem.axe);
        effects.CreateElement(WorldValues.woodAvailable, true);

        name = "Chop Wood";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        state.SetElementValue(WorldValues.woodAvailable, true);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        // Instantiate wood object

        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        // find tree to chop
        // debug values at the moment
        agent.actionObject = agent.treeTarget.gameObject;
        agent.m_actionTargetLocation = agent.treeTarget.position;
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        // is tree in range
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
    }
}
