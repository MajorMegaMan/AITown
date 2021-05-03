using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GOAP;

using U_GOAPAgent = GOAP.GOAPAgent<UnityEngine.GameObject>;
using U_GOAPBehaviour = GOAP.GOAPBehaviour<UnityEngine.GameObject>;

public class AIAgent : MonoBehaviour
{
    public float stoppingDistance = 0.5f;

    [HideInInspector] public NavMeshAgent navAgent;

    Vector3 m_targetPosition = Vector3.zero;

    U_GOAPAgent m_goapAgent;

    GOAPWorldState selfishNeeds;

    // Action var
    public Vector3 m_actionTargetLocation = Vector3.zero;
    public GameObject actionObject = null;

    //Debugging
    [Header("Debugging")]
    public Transform treeTarget;
    public Transform woodStoreTarget;
    public Transform foodBushTarget;
    public Transform foodStoreTarget;

    [ReadOnly] public float hunger = 100.0f;
    [ReadOnly] public WorldValues.HoldItemType item = WorldValues.HoldItemType.nothing;
    [ReadOnly] public GameObject holdItemObject = null;

    private void Awake()
    {
        m_goapAgent = new U_GOAPAgent(gameObject);
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_goapAgent.SetEnterNavigationFunc(StartNavigating);
        m_goapAgent.SetExitNavigationFunc(StopNavigating);
        m_goapAgent.SetMoveToDelegate(Movefunc);
    }

    // Update is called once per frame
    void Update()
    {
        m_goapAgent.Update();

        // debugging
        hunger = selfishNeeds.GetElementValue<float>(WorldValues.hunger);
        item = selfishNeeds.GetElementValue<WorldValues.HoldItemType>(WorldValues.holdItemType);
        holdItemObject = selfishNeeds.GetElementValue<GameObject>(WorldValues.holdItemObject);
    }

    public void SetWorldState(GOAPWorldState worldState)
    {
        m_goapAgent.SetWorldState(worldState);
    }

    // returns a new worldstate from the cobined world and this agents selfish needs
    public GOAPWorldState GetWorldState()
    {
        return m_goapAgent.GetWorldState();
    }

    public void SetBehaviour(U_GOAPBehaviour behaviour)
    {
        m_goapAgent.SetBehaviour(behaviour);
        selfishNeeds = m_goapAgent.GetSelfishNeeds();
    }

    void SetTargetPosition(Vector3 targetPosition)
    {
        m_targetPosition = targetPosition;
        navAgent.SetDestination(m_targetPosition);
    }

    void StartNavigating()
    {
        navAgent.isStopped = false;
        SetTargetPosition(m_actionTargetLocation);
    }

    void StopNavigating()
    {
        navAgent.isStopped = true;
    }

    GOAPAgent<GameObject>.MovementFlag Movefunc()
    {
        // if agent is calculating a path need to wait until it has finished
        if (navAgent.pathPending)
        {
            return GOAPAgent<GameObject>.MovementFlag.PARTIAL;
        }
        else
        {
            // The action object may be moving while the agent is travelling
            SetTargetPosition(actionObject.transform.position);
        }

        return GOAPAgent<GameObject>.MovementFlag.COMPLETE;
    }
}
