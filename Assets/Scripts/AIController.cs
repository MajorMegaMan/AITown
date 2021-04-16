using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float stoppingDistance = 0.5f;

    NavMeshAgent navAgent;

    Vector3 m_targetPosition = Vector3.zero;

    StateMachine m_stateMachine = new StateMachine();

    // GOAP
    GOAPWorldState m_worldState = new GOAPWorldState();
    List<GOAPAction> m_actions = new List<GOAPAction>();
    Queue<GOAPAction> m_plan = new Queue<GOAPAction>();


    GOAPAction m_currentAction;
    // Action var
    public Vector3 m_actionTargetLocation = Vector3.zero;
    public GameObject actionObject = null;

    //Debugging
    [Header("Debugging")]
    public Transform treeTarget;
    public Transform woodStoreTarget;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();

        // Debugging
        //m_stateMachine.SetPlanFunc(DecideState);
        //m_stateMachine.SetMoveFunc(MoveTo);
        //m_stateMachine.SetPerformFunc(PerformAction);

        m_stateMachine.AddStates(3);
        m_stateMachine.SetStateFunc(0, DecideState);
        m_stateMachine.SetStateFunc(1, MoveTo);
        m_stateMachine.SetStateFunc(2, PerformAction);

        m_stateMachine.SetState(0);

        m_worldState.CreateElement(WorldValues.holdingWood, false);
        m_worldState.CreateElement(WorldValues.storedWood, 0);

        m_actions.Add(new PickUpWood());
        m_actions.Add(new StoreWood());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_stateMachine.CallState();
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

    public void SetTargetPosition(Vector3 targetPosition)
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
        // Check distance
        if(navAgent.remainingDistance < stoppingDistance)
        {
            // reached stopping point
            StopNavigating();

            // Start performing actions here
            m_stateMachine.SetState(2);
        }
    }

    void GetPlan()
    {
        // Get GOAPplan
        // need to find goal
        GOAPWorldState goal = new GOAPWorldState(m_worldState);
        var data = goal.GetData(WorldValues.storedWood);
        int woodVal = data.ConvertValue<int>();
        woodVal++;
        data.value = woodVal;
        m_plan = GOAPPlanner.CalcPlan(m_worldState, goal, m_actions);
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
                Debug.Log("Entering Action");
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
            GetPlan();
        }
    }

    void PerformAction()
    {
        Debug.Log("Performing Action");
        // Check result of performing action
        switch (m_currentAction.PerformAction(m_worldState))
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
                    break;
                }
        }
    }
}
