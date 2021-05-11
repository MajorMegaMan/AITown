using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class PickUpAxe : AIAgentAction
{
    public PickUpAxe()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        preconditions.CreateElement(WorldValues.axeAvailable, true);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.axe);
        effects.CreateElement(WorldValues.axeAvailable, false);

        name = "Pick Up Axe";
    }

    public override void AddEffects(GOAPWorldState state)
    {
        //base.AddEffects(state);
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.axe);
        state.SetElementValue(WorldValues.axeAvailable, false);
    }

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        if(!worldState.GetElementValue<bool>(WorldValues.axeAvailable))
        {
            return ActionState.interrupt;
        }

        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        var axeItem = aiAgent.actionObject.GetComponent<HoldableItem>();
        axeItem.AttachObject(aiAgent.transform);

        worldState.SetElementValue(WorldValues.holdItemObject, aiAgent.actionObject);

        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // This is where the logic to find an axe would go
        GOAPWorldState agentState = agent.GetWorldState();

        // as the previous journey may have taken too long and an axe is now not available
        if(agentState.GetElementValue<bool>(WorldValues.axeAvailable))
        {
            //HoldableItem axe = agentState.GetElementValue<HoldableItem>(WorldValues.worldAxe);
            //HoldableItem axe = (HoldableItem)(agentState.GetElementValue(WorldValues.worldAxe));
            var obj = agentState.GetElementValue<GameObject>(WorldValues.worldAxe);
            aiAgent.actionObject = obj;
            aiAgent.m_actionTargetLocation = aiAgent.actionObject.transform.position;
            return true;
        }
        else
        {
            aiAgent.actionObject = null;
            aiAgent.m_actionTargetLocation = aiAgent.transform.position;
            return false;
        }
    }

    public override bool IsInRange(U_GOAPAgent agent)
    {
        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        return (aiAgent.transform.position - aiAgent.actionObject.transform.position).magnitude < aiAgent.stoppingDistance;
    }
}
