using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

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
            if (targetGoal == null)
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
