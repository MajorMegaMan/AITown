using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class GathererComponent : BehaviourComponent
{
    public override void Init()
    {
        // Your Initialisation code goes here

        // Add to actionList
        // actionList.Add(act);

        actionList.Add(ActionList.gatherFood);
        actionList.Add(ActionList.pickUpFood);
        actionList.Add(ActionList.storeFood);

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
        targetGoal.CreateElement(WorldValues.worldFoodCount, 5);
        return GoalStatus.foundHardGoal;
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
