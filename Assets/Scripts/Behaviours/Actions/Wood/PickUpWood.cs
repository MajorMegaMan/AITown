using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class PickUpWood : AIAgentAction
{
    List<GameObject> instantiatedWoodObjects;

    public PickUpWood(List<GameObject> instantiatedWoodObjects)
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        preconditions.CreateElement(WorldValues.woodAvailable, true);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.wood);
        effects.CreateElement(WorldValues.woodAvailable, false);
        effects.CreateElement(WorldValues.worldWoodCount, -1);

        name = "Pick Up Wood";

        this.instantiatedWoodObjects = instantiatedWoodObjects;
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
    }

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        if (worldState.GetElementValue<bool>(WorldValues.woodAvailable))
        {
            AddEffects(worldState);
            HoldableItem woodItem = aiAgent.actionObject.GetComponent<HoldableItem>();
            woodItem.AttachObject(aiAgent.transform);

            instantiatedWoodObjects.Remove(aiAgent.actionObject);
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
        if(instantiatedWoodObjects.Count == 0)
        {
            return false;
        }

        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // find wood to pick up
        Vector3 agentPosition = aiAgent.transform.position;

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

        aiAgent.actionObject = closestWood;

        // currently just represented by a bool
        return true;
    }

    public override bool IsInRange(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        return (aiAgent.transform.position - aiAgent.actionObject.transform.position).magnitude < aiAgent.stoppingDistance;
        //return true;
    }

    public override bool CanPerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        bool canPerform = base.CanPerformAction(agent, worldState);

        if (canPerform)
        {
            GameObject agentGameObject = agent.GetAgentObject();
            AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

            canPerform = instantiatedWoodObjects.Contains(aiAgent.actionObject);
        }

        return canPerform;
    }
}
