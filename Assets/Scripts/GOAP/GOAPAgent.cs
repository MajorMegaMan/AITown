using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GOAPAgent : MonoBehaviour
{
    public float stoppingDistance = 0.5f;

    [HideInInspector] public NavMeshAgent navAgent;

    Vector3 m_targetPosition = Vector3.zero;

    StateMachine m_stateMachine = new StateMachine();

    // GOAP
    GOAPWorldState m_combinedWorldState;
    GOAPWorldState m_agentWorldState;
    Queue<GOAPAction> m_plan = new Queue<GOAPAction>();
    GOAPBehaviour m_behaviour;

    GOAPAction m_currentAction;
    // Action var
    public Vector3 m_actionTargetLocation = Vector3.zero;
    public GameObject actionObject = null;

    //Debugging
    [Header("Debugging")]
    public Transform treeTarget;
    public Transform woodStoreTarget;
    public Transform foodBushTarget;

    public float hunger = 100.0f;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();

        // initialise states
        m_stateMachine.AddStates(3);
        m_stateMachine.SetStateFunc(0, DecideState);
        m_stateMachine.SetStateFunc(1, MoveTo);
        m_stateMachine.SetStateFunc(2, PerformAction);

        m_stateMachine.SetState(0);

        // Create World State
        //m_agentWorldState.CreateElement(WorldValues.holdingWood, false);
        //m_agentWorldState.CreateElement(WorldValues.storedWood, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_agentWorldState = m_behaviour.GetSelfishNeeds();
    }

    // Update is called once per frame
    void Update()
    {
        m_behaviour.Update(this, m_agentWorldState);
        m_stateMachine.CallState();

        // debugging
        hunger = m_agentWorldState.GetElementValue<float>(WorldValues.hunger);
    }

    public void SetWorldState(GOAPWorldState worldState)
    {
        m_combinedWorldState = GOAPWorldState.CombineWithReferences(worldState, m_agentWorldState);
    }

    public void SetBehaviour(GOAPBehaviour behaviour)
    {
        m_behaviour = behaviour;
        // new behaviour will most likely not contain the actions remaining in the queue
        // so just simply clear them all and then the agent will find a new plan.
        m_plan.Clear();
    }

    void SetPathToTargetPosition()
    {
        Debug.Log("settingPath");
        StartNavigating();
        navAgent.SetDestination(m_targetPosition);
        // Start checking for path
        // Set state to moving
        m_stateMachine.SetState(1);
    }

    void SetTargetPosition(Vector3 targetPosition)
    {
        m_targetPosition = targetPosition;
        SetPathToTargetPosition();
    }

    void StartNavigating()
    {
        navAgent.isStopped = false;
    }

    void StopNavigating()
    {
        navAgent.isStopped = true;
    }

    void MoveTo()
    {
        if(navAgent.pathPending)
        {
            return;
        }
        // Check distance
        if(navAgent.remainingDistance < stoppingDistance)
        {
            // reached stopping point
            StopNavigating();

            // Start performing actions here
            m_stateMachine.SetState(2);
        }
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
            m_currentAction.EnterAction(this);
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
            Debug.Log("Getting Plan"); 
            FindPlan();
        }
    }

    void PerformAction()
    {
        Debug.Log("Performing Action");
        // Check result of performing action
        switch (m_currentAction.PerformAction(m_combinedWorldState))
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
