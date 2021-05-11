using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPBehaviour = GOAP.GOAPBehaviour<UnityEngine.GameObject>;

public class WoodCutterBehaviour : U_GOAPBehaviour
{
    public WoodCutterBehaviour()
    {
        // Initialise Action List
        foreach (var act in ActionList.humanActions)
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

        bool holdingAxe = agentWorldState.GetElementValue<WorldValues.HoldItemType>(WorldValues.holdItemType) == WorldValues.HoldItemType.axe;

        if (holdingAxe || agentWorldState.GetElementValue<bool>(WorldValues.axeAvailable))
        {
            // Get wood for storage
            int woodVal = agentWorldState.GetElementValue<int>(WorldValues.worldWoodCount);
            woodVal++;

            targetGoal.CreateElement(WorldValues.worldWoodCount, woodVal);
        }

        else
        {
            // dick around for now
            return null;
        }

        //return goal;
        return targetGoal;
    }

    public override void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds)
    {

    }
}
