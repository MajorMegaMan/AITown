using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using GoalStatus = BehaviourComponent.GoalStatus;

public abstract class AIBehaviour : GOAPBehaviour<GameObject>
{
    delegate GoalStatus FindGoalDelegate(GOAPWorldState agentWorldState, GOAPWorldState targetGoal, GoalStatus currentGoalStatus);
    List<FindGoalDelegate> findGoalList = new List<FindGoalDelegate>();

    delegate void UpdateDelegate(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds);
    UpdateDelegate updateDelegate;

    public AIBehaviour()
    {

    }

    public void AddBehaviourComponent(BehaviourComponent behaviourComponent)
    {
        foreach (var act in behaviourComponent.actionList)
        {
            if (!m_actions.Contains(act))
            {
                m_actions.Add(act);
            }
        }

        GOAPWorldState worldstate = behaviourComponent.requiredWorldStates;
        foreach (string name in worldstate.GetNames())
        {
            var data = worldstate.GetData(name);

            m_selfishNeeds.CreateElement(name, data.value);
        }

        if(behaviourComponent.HasFindGoal())
        {
            findGoalList.Add(behaviourComponent.FindGoal);
        }

        if (behaviourComponent.HasUpdate())
        {
            updateDelegate += behaviourComponent.Update;
        }
    }

    public override GOAPWorldState FindGoal(GOAPWorldState agentWorldState)
    {
        GOAPWorldState targetGoal = new GOAPWorldState();
        GoalStatus goalStatus = 0;

        foreach(var func in findGoalList)
        {
            goalStatus = func(agentWorldState, targetGoal, goalStatus);
            
            if(goalStatus == GoalStatus.foundHardGoal)
            {
                return targetGoal;
            }
        }

        if(goalStatus == GoalStatus.noGoalFound)
        {
            return null;
        }

        return targetGoal;
    }

    public override void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds)
    {
        updateDelegate(agent, agentSelfishNeeds);
    }
}
