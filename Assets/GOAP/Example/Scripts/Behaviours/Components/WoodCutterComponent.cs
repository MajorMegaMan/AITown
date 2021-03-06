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

        requiredSelfishNeeds.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        requiredSelfishNeeds.CreateElement(WorldValues.holdItemObject, null);
    }

    public override bool HasFindGoal()
    {
        return true;
    }

    public override GoalStatus FindGoal(GOAPWorldState agentWorldState, GOAPWorldState targetGoal, GoalStatus currentGoalStatus)
    {
        // If there's an axe, make wood for others to possibly carry
        if (agentWorldState.GetElementValue<bool>(WorldValues.axeAvailable))
        {
            // Get wood for storage
            int woodVal = agentWorldState.GetElementValue<int>(WorldValues.worldWoodCount);
            woodVal += 5;

            targetGoal.CreateElement(WorldValues.worldWoodCount, woodVal);

            return GoalStatus.foundHardGoal;
        }

        // if there is wood available, but no axe. store the wood in storage
        if (agentWorldState.GetElementValue<bool>(WorldValues.woodAvailable))
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
