using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

public class GOAPAgent
{
    StateMachine m_stateMachine = new StateMachine();

    // GOAP
    GOAPWorldState m_combinedWorldState;
    GOAPWorldState m_agentWorldState = null;
    Queue<GOAPAction> m_plan = new Queue<GOAPAction>();
    GOAPBehaviour m_behaviour;

    GOAPAction m_currentAction;

    public enum State
    {
        PLANNING,
        MOVE_TO,
        PERFORM_ACTION
    }

    private void Init()
    {
        // initialise states
        m_stateMachine.AddStates(3);
        m_stateMachine.SetStateFunc(0, DecideState);
        m_stateMachine.SetStateFunc(1, MoveTo);
        m_stateMachine.SetStateFunc(2, PerformAction);

        m_stateMachine.SetState(0);
    }

    // Start is called before the first frame update
    public void SetSelfishWorldState()
    {
        m_agentWorldState = m_behaviour.GetSelfishNeeds();
    }

    // Update is called once per frame
    void Update()
    {
        m_behaviour.Update(this, m_agentWorldState);
        m_stateMachine.CallState();
    }

    public bool SetWorldState(GOAPWorldState worldState)
    {
        if(m_agentWorldState != null)
        {
            m_combinedWorldState = GOAPWorldState.CombineWithReferences(worldState, m_agentWorldState);
            return true;
        }
        return false;
    }

    // returns a new worldstate from the cobined world and this agents selfish needs
    public GOAPWorldState GetWorldState()
    {
        return new GOAPWorldState(m_combinedWorldState);
    }

    public void SetBehaviour(GOAPBehaviour behaviour)
    {
        m_behaviour = behaviour;
        //m_agentWorldState = m_behaviour.GetSelfishNeeds();
        // new behaviour will most likely not contain the actions remaining in the queue
        // so just simply clear them all and then the agent will find a new plan.
        m_plan.Clear();
    }

    public void SetState(State desiredState)
    {
        m_stateMachine.SetState((int)desiredState);
    }

    void MoveTo()
    {
        if(!m_currentAction.CanPerformAction(m_combinedWorldState))
        {
            // cannot perform current action, so stop moving to it and find a new plan
            FindPlan();
            StopNavigating();
            return;
        }
        else
        {
            // StartNavigating
            m_actionTargetLocation = actionObject.transform.position;
            SetTargetPosition(m_actionTargetLocation);
        }

        // if agent is calculating a path need to wait until it has finished
        if (navAgent.pathPending)
        {
            return;
        }

        // Check distance
        if (navAgent.remainingDistance < stoppingDistance)
        {
            // reached stopping point
            StopNavigating();

            // Start performing actions here
            m_stateMachine.SetState(2);
        }
    }

    void StopNavigating()
    {

    }

    void UpdateNavigation()
    {

    }

    public void FindPlan()
    {
        // Get GOAPplan
        // need to find goal
        m_plan = m_behaviour.CalcPlan(m_combinedWorldState);
        m_stateMachine.SetState(0);
    }

    void DecideState()
    {
        if(m_plan.Count > 0)
        {
            // plan was found
            m_currentAction = m_plan.Dequeue();

            // Check if the action target was assigned correctly
            if(!m_currentAction.EnterAction(this))
            {
                // Plan is garbage now find a new one
                FindPlan();
                return;
            }
            // if in range of action
            if(m_currentAction.IsInRange(this))
            {
                // state = perform
                m_stateMachine.SetState(2);
            }
            else
            {
                // else 
                // state = moveto
                SetTargetPosition(m_actionTargetLocation);
            }
        }
        else
        {
            // there is no plan
            // find a new one
            FindPlan();
        }
    }

    void PerformAction()
    {
        // Check result of performing action
        switch (m_currentAction.PerformAction(this, m_combinedWorldState))
        {
            case GOAPAction.ActionState.completed:
                {
                    // action was completed progress to the next action
                    m_stateMachine.SetState(0);
                    break;
                }
            case GOAPAction.ActionState.performing:
                {
                    // still performing the action. Nothing needs to be done?
                    // maybe still need to check if in range
                    break;
                }
            case GOAPAction.ActionState.interrupt:
                {
                    // action was interrupted and as a result was not completed therefore a new plan may be needed
                    FindPlan();
                    break;
                }
        }
    }
}
