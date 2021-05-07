using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class GatherFood : GOAPAgentAction<GameObject>
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

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // Instantiate wood object
        GameObject newFood = GameObject.Instantiate(foodPrefab);
        newFood.transform.position = aiAgent.transform.position + (Vector3.up * 2.5f);
        instantiatedFoodObjects.Add(newFood);
        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // find food to chop
        // debug values at the moment
        aiAgent.actionObject = aiAgent.foodBushTarget.gameObject;
        aiAgent.m_actionTargetLocation = aiAgent.foodBushTarget.position;
        return true;
    }

    public override bool IsInRange(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // is food in range
        return (aiAgent.transform.position - aiAgent.actionObject.transform.position).magnitude < aiAgent.stoppingDistance;
    }
}
