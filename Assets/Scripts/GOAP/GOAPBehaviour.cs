using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPBehaviour
{
    protected List<GOAPAction> m_actions = new List<GOAPAction>();
    protected GOAPWorldState m_selfishNeeds = new GOAPWorldState();

    public GOAPBehaviour()
    {
        // Initialise Action List
    }

    public abstract GOAPWorldState FindGoal(GOAPWorldState agentWorldState);

    public List<GOAPAction> GetActions()
    {
        return m_actions;
    }

    public virtual void Update(GOAPAgent agent, GOAPWorldState agentSelfishNeeds)
    {
        // This is purposely empty just so this can be called without errors.
        // This should be overridden if you want inherited behaviours to have logic outside of planning aswell.
        // eg. A human gets hungry while chopping wood and rather than finishing get wood, he will immediatly get a plan for getting food
    }

    public GOAPWorldState GetSelfishNeeds()
    {
        return new GOAPWorldState(m_selfishNeeds);
    }

    public Queue<GOAPAction> CalcPlan(GOAPWorldState agentWorldState)
    {
        return GOAPPlanner.CalcPlan(agentWorldState, FindGoal(agentWorldState), GetActions());
    }
}
