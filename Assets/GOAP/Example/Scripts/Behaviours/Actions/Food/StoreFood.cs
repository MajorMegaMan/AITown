using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class StoreFood : AIAgentAction
{
    public StoreFood()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.food);

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
    }

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        // Get held wood object and destroy it
        var data = worldState.GetData(WorldValues.holdItemObject);
        GameObject foodObject = (GameObject)(data.value);

        //GameObject woodObject = worldState.GetElementValue<GameObject>(WorldValues.holdItemObject);
        GameObject.Destroy(foodObject);

        data.value = null;

        return ActionState.completed;
    }

    public override bool EnterAction(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // This is where the logic to find a food storage would go but right now it is just using a debug value for testing
        aiAgent.actionObject = WorldValues.foodStoreTarget.gameObject;
        aiAgent.m_actionTargetLocation = WorldValues.foodStoreTarget.position;
        return true;
    }

    public override bool IsInRange(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        return (aiAgent.transform.position - aiAgent.actionObject.transform.position).magnitude < aiAgent.stoppingDistance;
    }
}
