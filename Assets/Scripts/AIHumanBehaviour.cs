using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHumanBehaviour : GOAPBehaviour
{
    float minHunger = 90.0f;

    public AIHumanBehaviour()
    {
        // Initialise Action List
        m_actions.Add(new PickUpWood());
        m_actions.Add(new StoreWood());

        m_actions.Add(new PickUpFood());
        m_actions.Add(new EatFood());

        m_actions.Add(new PickUpAxe());
        m_actions.Add(new DropAxe());

        // Initialise WorldStateNeeds
        m_selfishNeeds.CreateElement(WorldValues.holdingWood, false);
        m_selfishNeeds.CreateElement(WorldValues.holdingFood, false);
        m_selfishNeeds.CreateElement(WorldValues.hunger, 100.0f);
        m_selfishNeeds.CreateElement(WorldValues.hasProcessedHunger, false);

        m_selfishNeeds.CreateElement(WorldValues.holdingAxe, false);
    }

    public override GOAPWorldState FindGoal(GOAPWorldState agentWorldState)
    {
        GOAPWorldState goal = new GOAPWorldState(agentWorldState);

        if (agentWorldState.GetElementValue<float>(WorldValues.hunger) < minHunger)
        {
            // This guy is hungry
            // Get food to eat
            goal.SetElementValue(WorldValues.hunger, 100.0f);
            goal.SetElementValue(WorldValues.hasProcessedHunger, false);
        }
        else
        {
            // Get wood for storage
            var data = goal.GetData(WorldValues.storedWood);
            int woodVal = data.ConvertValue<int>();
            woodVal++;
            data.value = woodVal;
        }

        return goal;
    }

    public override void Update(GOAPAgent agent, GOAPWorldState agentSelfishNeeds)
    {
        var data = agentSelfishNeeds.GetData(WorldValues.hunger);
        float hungerVal = data.ConvertValue<float>();
        hungerVal -= Time.deltaTime;
        hungerVal = Mathf.Clamp(hungerVal, 0.0f, 100.0f);
        data.value = hungerVal;

        bool processedData = agentSelfishNeeds.GetElementValue<bool>(WorldValues.hasProcessedHunger);

        if (!processedData && hungerVal < minHunger)
        {
            // This guy is hungry
            agentSelfishNeeds.SetElementValue(WorldValues.hasProcessedHunger, true);
            agent.FindPlan();
        }
    }
}
