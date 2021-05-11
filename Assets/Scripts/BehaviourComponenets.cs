using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public static class BehaviourComponenets
{
    public static BehaviourInitialiser hungerComponent;

    public static void Init()
    {

    }

    static void InitHungerComponent()
    {
        hungerComponent = new BehaviourInitialiser();

        foreach(var act in ActionList.humanFoodActions)
        {
            hungerComponent.actionList.Add(act);
        }

        hungerComponent.requiredWorldStates = new GOAPWorldState();

        var worldState = hungerComponent.requiredWorldStates;
        worldState.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        worldState.CreateElement(WorldValues.holdItemObject, null);

        worldState.CreateElement(WorldValues.hunger, 100.0f);
        worldState.CreateElement(WorldValues.hasProcessedHunger, false);

        hungerComponent.updater = new HungerUpdate();
    }
}

public class BehaviourInitialiser
{
    public List<GOAPAgentAction<GameObject>> actionList = new List<GOAPAgentAction<GameObject>>();
    public GOAPWorldState requiredWorldStates = null;
    public BehaviourUpdater updater = null;
}

public abstract class BehaviourUpdater
{
    public abstract GOAPWorldState FindGoal(GOAPWorldState agentWorldState);
    public abstract void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds);
}

public class HungerUpdate : BehaviourUpdater
{
    float minHunger = 20.0f;
    float hungerSpeed = 5.0f;

    public override GOAPWorldState FindGoal(GOAPWorldState agentWorldState)
    {
        GOAPWorldState targetGoal = null;

        if (agentWorldState.GetElementValue<float>(WorldValues.hunger) < minHunger)
        {
            // This guy is hungry
            // Get food to eat
            targetGoal = new GOAPWorldState();
            targetGoal.CreateElement(WorldValues.hunger, 100.0f);
        }

        //return goal;
        return targetGoal;
    }

    public override void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds)
    {
        var data = agentSelfishNeeds.GetData(WorldValues.hunger);
        float hungerVal = data.ConvertValue<float>();
        hungerVal -= Time.deltaTime * hungerSpeed;
        hungerVal = Mathf.Clamp(hungerVal, 0.0f, 100.0f);
        data.value = hungerVal;

        bool processedData = agentSelfishNeeds.GetElementValue<bool>(WorldValues.hasProcessedHunger);

        if (!processedData && hungerVal < minHunger)
        {
            // This guy is hungry
            agentSelfishNeeds.SetElementValue(WorldValues.hasProcessedHunger, true);
            agent.GetAgentObject().GetComponent<AIAgent>().StopNavigating();
            agent.FindPlan();
        }
    }
}
