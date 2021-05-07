using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class StoreWood : GOAPAgentAction<GameObject>
{
    public StoreWood()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.wood);

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
    }

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
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

    public override bool EnterAction(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // This is where the logic to find a tree would go but right now it is just using a debug value for testing
        aiAgent.actionObject = aiAgent.woodStoreTarget.gameObject;
        aiAgent.m_actionTargetLocation = aiAgent.woodStoreTarget.position;
        return true;
    }

    public override bool IsInRange(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        return (aiAgent.transform.position - aiAgent.actionObject.transform.position).magnitude < aiAgent.stoppingDistance;
    }
}
