using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public abstract class BehaviourComponent
{
    public List<GOAPAgentAction<GameObject>> actionList = new List<GOAPAgentAction<GameObject>>();
    public GOAPWorldState requiredWorldStates = new GOAPWorldState();

    public BehaviourComponent()
    {
        Init();
    }

    public enum GoalStatus
    {
        // no goal was found and therefore the target goal will be null
        noGoalFound,
        // soft goals will still continue to search for more elements in the target goal but will also result in the target goal to NOT be null
        foundSoftGoal,
        // hard goals will stop populating the target goal and result in the finalised target goal
        foundHardGoal
    }

    public abstract void Init();
    public abstract bool HasFindGoal();
    public virtual GoalStatus FindGoal(GOAPWorldState agentWorldState, GOAPWorldState targetGoal, GoalStatus currentGoalStatus)
    {
        return currentGoalStatus;
    }

    public abstract bool HasUpdate();
    public virtual void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds)
    {

    }


}
