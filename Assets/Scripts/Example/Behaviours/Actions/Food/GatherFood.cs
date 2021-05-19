using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class GatherFood : AIAgentAction
{
    static List<GameObject> instantiatedFoodObjects;
    static GameObject foodPrefab;

    public GatherFood()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        effects.CreateElement(WorldValues.foodAvailable, true);
        effects.CreateElement(WorldValues.worldFoodCount, +1);

        name = "Gather Food";
    }

    public static void SetFoodObjectsList(List<GameObject> foodObjects)
    {
        instantiatedFoodObjects = foodObjects;
    }

    public static void SetFoodPrefab(GameObject prefab)
    {
        foodPrefab = prefab;
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

        return ActionState.completed;
    }

    public override bool EnterAction(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // find food to chop
        // debug values at the moment
        aiAgent.actionObject = WorldValues.foodBushTarget.gameObject;
        aiAgent.m_actionTargetLocation = WorldValues.foodBushTarget.position;
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
