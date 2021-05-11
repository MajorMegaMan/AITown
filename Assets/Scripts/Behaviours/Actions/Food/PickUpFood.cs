using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class PickUpFood : AIAgentAction
{
    List<GameObject> instantiatedFoodObjects;

    public PickUpFood(List<GameObject> instantiatedFoodObjects)
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        preconditions.CreateElement(WorldValues.foodAvailable, true);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.food);
        effects.CreateElement(WorldValues.foodAvailable, false);
        effects.CreateElement(WorldValues.worldFoodCount, -1);

        name = "Pick Up Food";

        this.instantiatedFoodObjects = instantiatedFoodObjects;
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
    }

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        if (worldState.GetElementValue<bool>(WorldValues.foodAvailable))
        {
            GameObject agentGameObject = agent.GetAgentObject();
            AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

            AddEffects(worldState);
            HoldableItem foodItem = aiAgent.actionObject.GetComponent<HoldableItem>();
            foodItem.AttachObject(aiAgent.gameObject.transform);

            instantiatedFoodObjects.Remove(aiAgent.actionObject);
            worldState.SetElementValue(WorldValues.holdItemObject, aiAgent.actionObject);
            return ActionState.completed;
        }
        else
        {
            return ActionState.interrupt;
        }
    }

    public override bool EnterAction(U_GOAPAgent agent)
    {
        if (instantiatedFoodObjects.Count == 0)
        {
            return false;
        }

        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // find wood to pick up
        Vector3 agentPosition = aiAgent.transform.position;

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

        aiAgent.actionObject = closestFood;

        // currently just represented by a bool
        return true;
    }

    public override bool IsInRange(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        return (aiAgent.transform.position - aiAgent.actionObject.transform.position).magnitude < aiAgent.stoppingDistance;
    }

    public override bool CanPerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        bool canPerform = base.CanPerformAction(agent, worldState);

        if(canPerform)
        {
            GameObject agentGameObject = agent.GetAgentObject();
            AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

            canPerform = instantiatedFoodObjects.Contains(aiAgent.actionObject);
        }

        return canPerform;
    }
}
