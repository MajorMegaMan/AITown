using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class CollecterComponent : BehaviourComponent
{
    public override void Init()
    {
        // Your Initialisation code goes here

        // Add to actionList
        // actionList.Add(act);
        actionList.Add(ActionList.pickUpWood);
        actionList.Add(ActionList.storeWood);

        actionList.Add(ActionList.pickUpFood);
        actionList.Add(ActionList.storeFood);


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
        //targetGoal.CreateElement(WorldValues.foodAvailable, false);
        //targetGoal.CreateElement(WorldValues.woodAvailable, false);

        if(agentWorldState.GetElementValue<bool>(WorldValues.foodAvailable))
        {
            int foodVal = agentWorldState.GetElementValue<int>(WorldValues.storedFood);
            foodVal++;
            targetGoal.CreateElement(WorldValues.storedFood, foodVal);
        }

        if (agentWorldState.GetElementValue<bool>(WorldValues.woodAvailable))
        {
            int woodVal = agentWorldState.GetElementValue<int>(WorldValues.storedWood);
            woodVal++;
            targetGoal.CreateElement(WorldValues.storedWood, woodVal);
        }

        return GoalStatus.foundSoftGoal;
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
