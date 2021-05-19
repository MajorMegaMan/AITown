﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPBehaviour = GOAP.GOAPBehaviour<UnityEngine.GameObject>;

public class AIHumanBehaviour : U_GOAPBehaviour
{
    float minHunger = 20.0f;
    float hungerSpeed = 5.0f;

    public AIHumanBehaviour()
    {
        // Initialise Action List
        foreach(var act in ActionList.humanActions)
        {
            m_actions.Add(act);
        }

        // Initialise WorldStateNeeds
        m_selfishNeeds.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        m_selfishNeeds.CreateElement(WorldValues.holdItemObject, null);

        m_selfishNeeds.CreateElement(WorldValues.hunger, 100.0f);
        m_selfishNeeds.CreateElement(WorldValues.hasProcessedHunger, false);

    }

    public override GOAPWorldState FindGoal(GOAPWorldState agentWorldState)
    {
        GOAPWorldState targetGoal = new GOAPWorldState();

        if (agentWorldState.GetElementValue<float>(WorldValues.hunger) < minHunger)
        {
            // This guy is hungry
            // Get food to eat
            targetGoal.CreateElement(WorldValues.hunger, 100.0f);
        }

        else if(agentWorldState.GetElementValue<bool>(WorldValues.axeAvailable) || agentWorldState.GetElementValue<bool>(WorldValues.woodAvailable))
        {
            // Get wood for storage
            int woodVal = agentWorldState.GetElementValue<int>(WorldValues.storedWood);
            woodVal++;

            targetGoal.CreateElement(WorldValues.storedWood, woodVal);
        }

        else
        {
            // dick around for now
            //return null;

            // Get food for storage
            int foodVal = agentWorldState.GetElementValue<int>(WorldValues.storedFood);
            foodVal++;
            
            targetGoal.CreateElement(WorldValues.storedFood, foodVal);
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