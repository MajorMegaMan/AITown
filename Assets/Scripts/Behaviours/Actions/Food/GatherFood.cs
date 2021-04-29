using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherFood : GOAPAction
{
    List<GameObject> instantiatedFoodObjects;
    GameObject foodPrefab;

    public GatherFood(List<GameObject> instantiatedFoodObjects, GameObject foodPrefab)
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        effects.CreateElement(WorldValues.foodAvailable, true);
        effects.CreateElement(WorldValues.worldFoodCount, +1);

        name = "Gather Food";

        this.instantiatedFoodObjects = instantiatedFoodObjects;
        this.foodPrefab = foodPrefab;
    }

    public override void AddEffects(GOAPWorldState state)
    {
        state.SetElementValue(WorldValues.foodAvailable, true);

        var foodCountData = state.GetData(WorldValues.worldFoodCount);
        int value = foodCountData.ConvertValue<int>();
        value++;
        foodCountData.value = value;
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        // Instantiate wood object
        GameObject newFood = GameObject.Instantiate(foodPrefab);
        newFood.transform.position = agent.transform.position + (Vector3.up * 2.5f);
        instantiatedFoodObjects.Add(newFood);
        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        // find food to chop
        // debug values at the moment
        agent.actionObject = agent.foodBushTarget.gameObject;
        agent.m_actionTargetLocation = agent.foodBushTarget.position;
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        // is food in range
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
    }
}
