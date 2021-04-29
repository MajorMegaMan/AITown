using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWood : GOAPAction
{
    List<GameObject> instantiatedWoodObjects;

    public PickUpWood(object instantiatedWoodObjects)
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        preconditions.CreateElement(WorldValues.woodAvailable, true);

        preconditions.CreateElement(WorldValues.isHoldingItem, false);
        effects.CreateElement(WorldValues.isHoldingItem, true);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.wood);
        effects.CreateElement(WorldValues.woodAvailable, false);
        effects.CreateElement(WorldValues.worldWoodCount, -1);

        name = "Pick Up Wood";

        this.instantiatedWoodObjects = (List <GameObject>)instantiatedWoodObjects;
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.wood);
        
        var woodCountData = state.GetData(WorldValues.worldWoodCount);
        int value = woodCountData.ConvertValue<int>();
        value--;
        woodCountData.value = value;

        bool isWoodAvailable = value > 0;
        state.SetElementValue(WorldValues.woodAvailable, isWoodAvailable);

        state.CreateElement(WorldValues.isHoldingItem, true);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        if(worldState.GetElementValue<bool>(WorldValues.woodAvailable))
        {
            AddEffects(worldState);
            HoldableItem woodItem = agent.actionObject.GetComponent<HoldableItem>();
            woodItem.AttachObject(agent.gameObject.transform);

            instantiatedWoodObjects.Remove(agent.actionObject);
            worldState.SetElementValue(WorldValues.holdItemObject, agent.actionObject);
            return ActionState.completed;
        }
        else
        {
            return ActionState.interrupt;
        }
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        if(instantiatedWoodObjects.Count == 0)
        {
            return false;
        }

        // find wood to pick up
        Vector3 agentPosition = agent.gameObject.transform.position;

        GameObject closestWood = instantiatedWoodObjects[0];
        float closestDist = (closestWood.transform.position - agentPosition).magnitude;

        for(int i = 1; i < instantiatedWoodObjects.Count; i++)
        {
            GameObject wood = instantiatedWoodObjects[i];
            float dist = (wood.transform.position - agentPosition).magnitude;

            if(dist < closestDist)
            {
                closestWood = wood;
                closestDist = dist;
            }
        }

        agent.actionObject = closestWood;

        // currently just represented by a bool
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
        //return true;
    }
}
