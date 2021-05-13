using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class WoodCutterUpdate : BehaviourUpdater
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
