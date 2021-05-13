using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public static class BehaviourComponenets
{
    public static BehaviourInitialiser hungerComponent;
    public static BehaviourInitialiser woodCutterComponent;

    public static void Init()
    {
        InitHungerComponent();
        InitWoodCutter();
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

    static void InitWoodCutter()
    {
        woodCutterComponent = new BehaviourInitialiser();

        foreach(var act in ActionList.humanWoodActions)
        {
            hungerComponent.actionList.Add(act);
        }

        woodCutterComponent.requiredWorldStates = new GOAPWorldState();

        var worldState = woodCutterComponent.requiredWorldStates;
        worldState.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        worldState.CreateElement(WorldValues.holdItemObject, null);

        woodCutterComponent.updater = new WoodCutterUpdater();
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
    public abstract void FindGoal(GOAPWorldState agentWorldState, ref GOAPWorldState targetGoal);
    public abstract void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds);
}

public class HungerUpdate : BehaviourUpdater
{
    float minHunger = 20.0f;
    float hungerSpeed = 5.0f;

    public override void FindGoal(GOAPWorldState agentWorldState, ref GOAPWorldState targetGoal)
    {
        float currentHunger = agentWorldState.GetElementValue<float>(WorldValues.hunger);
        bool isHungry = currentHunger < minHunger;
        if (isHungry)
        {
            // This guy is hungry
            // Get food to eat
            if(targetGoal == null)
            {
                targetGoal = new GOAPWorldState();
            }
            targetGoal.CreateElement(WorldValues.hunger, 100.0f);
        }
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

public class WoodCutterUpdater : BehaviourUpdater
{
    public override void FindGoal(GOAPWorldState agentWorldState, ref GOAPWorldState targetGoal)
    {
        if (agentWorldState.GetElementValue<bool>(WorldValues.axeAvailable) || agentWorldState.GetElementValue<bool>(WorldValues.woodAvailable))
        {
            if (targetGoal == null)
            {
                targetGoal = new GOAPWorldState();
            }

            // Get wood for storage
            int woodVal = agentWorldState.GetElementValue<int>(WorldValues.storedWood);
            woodVal++;

            targetGoal.CreateElement(WorldValues.storedWood, woodVal);
        }
    }

    public override void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds)
    {

    }
}
