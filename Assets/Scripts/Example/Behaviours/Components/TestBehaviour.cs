using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class TestBehaviour : GOAPBehaviour<GameObject>
{
    delegate void FindGoalDelegate(GOAPWorldState agentWorldState, ref GOAPWorldState targetGoal);
    FindGoalDelegate findGoalDelegate;

    delegate void UpdateDelegate(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds);
    UpdateDelegate updateDelegate;

    public TestBehaviour()
    {

    }

    void AddBehaviourComponent(BehaviourComponentInitialiser behaviourComponent)
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

        BehaviourUpdater bUpdater = behaviourComponent.updater;
        if (bUpdater != null)
        {
            findGoalDelegate += bUpdater.FindGoal;
            updateDelegate += bUpdater.Update;
        }
    }

    public override GOAPWorldState FindGoal(GOAPWorldState agentWorldState)
    {
        GOAPWorldState targetGoal = null;

        findGoalDelegate(agentWorldState, ref targetGoal);

        return targetGoal;
    }

    public override void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds)
    {
        updateDelegate(agent, agentSelfishNeeds);
    }
}
