using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class WoodCutterComponent : BehaviourComponent
{
    public override void Init()
    {
        // Your Initialisation code goes here

        // Add to actionList
        // actionList.Add(act);

        foreach (var act in ActionList.humanWoodActions)
        {
            actionList.Add(act);
        }

        // Create requiredWorldStates
        // requiredWorldStates.CreateElement(string name, object value);

        requiredWorldStates.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        requiredWorldStates.CreateElement(WorldValues.holdItemObject, null);
    }

    public override bool HasFindGoal()
    {
        return true;
    }

    public override GoalStatus FindGoal(GOAPWorldState agentWorldState, GOAPWorldState targetGoal, GoalStatus currentGoalStatus)
    {
        if (agentWorldState.GetElementValue<bool>(WorldValues.axeAvailable) || agentWorldState.GetElementValue<bool>(WorldValues.woodAvailable))
        {
            // Get wood for storage
            int woodVal = agentWorldState.GetElementValue<int>(WorldValues.storedWood);
            woodVal++;

            targetGoal.CreateElement(WorldValues.storedWood, woodVal);

            return GoalStatus.foundHardGoal;
        }

        return currentGoalStatus;
    }

    public override bool HasUpdate()
    {
        return false;
    }
}
