using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFood : GOAPAction
{
    List<GameObject> instantiatedFoodObjects;

    public PickUpFood(object instantiatedFoodObjects)
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        preconditions.CreateElement(WorldValues.foodAvailable, true);

        preconditions.CreateElement(WorldValues.isHoldingItem, false);
        effects.CreateElement(WorldValues.isHoldingItem, true);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.food);
        effects.CreateElement(WorldValues.foodAvailable, false);
        effects.CreateElement(WorldValues.worldFoodCount, -1);

        name = "Pick Up Food";

        this.instantiatedFoodObjects = (List<GameObject>)instantiatedFoodObjects;
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.food);

        var foodCountData = state.GetData(WorldValues.worldFoodCount);
        int value = foodCountData.ConvertValue<int>();
        value--;
        foodCountData.value = value;

        bool isFoodAvailable = value > 0;
        state.SetElementValue(WorldValues.foodAvailable, isFoodAvailable);

        state.CreateElement(WorldValues.isHoldingItem, true);
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        if (worldState.GetElementValue<bool>(WorldValues.foodAvailable))
        {
            AddEffects(worldState);
            HoldableItem foodItem = agent.actionObject.GetComponent<HoldableItem>();
            foodItem.AttachObject(agent.gameObject.transform);

            instantiatedFoodObjects.Remove(agent.actionObject);
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
        if (instantiatedFoodObjects.Count == 0)
        {
            return false;
        }

        // find wood to pick up
        Vector3 agentPosition = agent.gameObject.transform.position;

        GameObject closestFood = instantiatedFoodObjects[0];
        float closestDist = (closestFood.transform.position - agentPosition).magnitude;

        for (int i = 1; i < instantiatedFoodObjects.Count; i++)
        {
            GameObject food = instantiatedFoodObjects[i];
            float dist = (food.transform.position - agentPosition).magnitude;

            if (dist < closestDist)
            {
                closestFood = food;
                closestDist = dist;
            }
        }

        agent.actionObject = closestFood;

        // currently just represented by a bool
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
    }
}
