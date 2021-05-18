using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class HungerComponent : BehaviourComponent
{
    float minHunger = 20.0f;
    float hungerSpeed = 5.0f;

    public override void Init()
    {
        foreach (var act in ActionList.humanFoodActions)
        {
            actionList.Add(act);
        }

        requiredWorldStates.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        requiredWorldStates.CreateElement(WorldValues.holdItemObject, null);

        requiredWorldStates.CreateElement(WorldValues.hunger, 100.0f);
        requiredWorldStates.CreateElement(WorldValues.hasProcessedHunger, false);
    }

    public override bool HasFindGoal()
    {
        return true;
    }
    public override GoalStatus FindGoal(GOAPWorldState agentWorldState, GOAPWorldState targetGoal, GoalStatus currentGoalStatus)
    {
        float currentHunger = agentWorldState.GetElementValue<float>(WorldValues.hunger);
        bool isHungry = currentHunger < minHunger;
        if (isHungry)
        {
            // This guy is hungry
            // Get food to eat
            targetGoal.CreateElement(WorldValues.hunger, 100.0f);

            return GoalStatus.foundHardGoal;
        }

        return currentGoalStatus;
    }

    public override bool HasUpdate()
    {
        return true;
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
