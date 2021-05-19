using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;

public class PickUpAxe : AIAgentAction
{
    static List<GameObject> instantiatedAxeObjects;

    public PickUpAxe()
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        preconditions.CreateElement(WorldValues.axeAvailable, true);

        effects.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.axe);
        effects.CreateElement(WorldValues.axeAvailable, false);
        effects.CreateElement(WorldValues.worldAxeCount, -1);

        name = "Pick Up Axe";
    }

    public static void SetAxeObjectsList(List<GameObject> axeObjects)
    {
        instantiatedAxeObjects = axeObjects;
    }

    public override void AddEffects(GOAPWorldState state)
    {
        state.SetElementValue(WorldValues.holdItemType, WorldValues.HoldItemType.axe);

        int axeCount = state.GetElementValue<int>(WorldValues.worldAxeCount);
        axeCount--;
        state.SetElementValue(WorldValues.worldAxeCount, axeCount);

        bool isAxeAvailable = axeCount > 0;
        state.SetElementValue(WorldValues.axeAvailable, isAxeAvailable);
    }

    public override ActionState PerformAction(U_GOAPAgent agent, GOAPWorldState worldState)
    {
        if(!worldState.GetElementValue<bool>(WorldValues.axeAvailable))
        {
            return ActionState.interrupt;
        }

        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        HoldableItem axeItem = aiAgent.actionObject.GetComponent<HoldableItem>();
        axeItem.AttachObject(aiAgent.transform);

        instantiatedAxeObjects.Remove(aiAgent.actionObject);
        worldState.SetElementValue(WorldValues.holdItemObject, aiAgent.actionObject);

        return ActionState.completed;
    }

    public override bool EnterAction(U_GOAPAgent agent)
    {
        // check if there is still an axe to pick up as the journey to the object may have taken too long
        if (instantiatedAxeObjects.Count == 0)
        {
            return false;
        }

        GameObject agentGameObject = agent.GetAgentObject();
        AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

        // find axe to pick up
        Vector3 agentPosition = aiAgent.transform.position;

        GameObject closestAxe = instantiatedAxeObjects[0];
        float closestDist = (closestAxe.transform.position - agentPosition).magnitude;

        for (int i = 1; i < instantiatedAxeObjects.Count; i++)
        {
            GameObject axe = instantiatedAxeObjects[i];
            float dist = (axe.transform.position - agentPosition).magnitude;

            if (dist < closestDist)
            {
                closestAxe = axe;
                closestDist = dist;
            }
        }

        aiAgent.actionObject = closestAxe;

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

        if (canPerform)
        {
            GameObject agentGameObject = agent.GetAgentObject();
            AIAgent aiAgent = agentGameObject.GetComponent<AIAgent>();

            canPerform = instantiatedAxeObjects.Contains(aiAgent.actionObject);
        }

        return canPerform;
    }
}
