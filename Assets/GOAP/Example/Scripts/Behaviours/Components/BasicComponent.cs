using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class BasicComponent : BehaviourComponent
{
    public override void Init()
    {
        // Your Initialisation code goes here

        // Add to actionList
        // actionList.Add(act);
        foreach(var act in ActionList.humanActions)
        {
            actionList.Add(act);
        }

        // Create requiredSelfishNeeds
        // requiredSelfishNeeds.CreateElement(string name, object value);
        requiredSelfishNeeds.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.nothing);
        requiredSelfishNeeds.CreateElement(WorldValues.holdItemObject, null);
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
        else
        {
            // dick around for now
            //return null;

            // Get food for storage
            int foodVal = agentWorldState.GetElementValue<int>(WorldValues.storedFood);
            foodVal++;

            targetGoal.CreateElement(WorldValues.storedFood, foodVal);
            return GoalStatus.foundSoftGoal;
        }
    }

    public override bool HasUpdate()
    {
        return false;
    }

    //public override void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds)
    //{
    //    // If HasUpdate() returns true then this should be filled with logic that would update the agentsSelfishNeeds
    //}
}
