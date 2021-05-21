using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPBehaviour = GOAP.GOAPBehaviour<UnityEngine.GameObject>;

public class AIManager : MonoBehaviour
{
    GOAPWorldState m_worldState = new GOAPWorldState();

    List<AIAgent> m_allAgents;// = new List<GOAPAgent>();

    List<U_GOAPBehaviour> m_behvaiourList = new List<U_GOAPBehaviour>();

    [Header("Test Action targets")]
    public GameObject debugAxe;
    public GameObject debugAxe2;
    public Transform treeTarget;
    public Transform woodStoreTarget;
    public Transform foodBushTarget;
    public Transform foodStoreTarget;

    [Header("Agent Materials")]
    public Material woodCutterMaterial;
    public Material gathererMaterial;
    public Material collecterMaterial;

    [Header("Camera")]
    public CameraFocus cameraFocus;

    [Header("Prefabs")]
    public GameObject axePrefab;
    public GameObject woodPrefab;
    public GameObject foodPrefab;

    private void Awake()
    {
        GOAP.GOAPPlanner.SetMaxDepth(50);
        InitWorldVariables();

        m_behvaiourList.Add(AllBehaviours.basicBehaviour);
        m_behvaiourList.Add(AllBehaviours.woodCutterBehaviour);
        m_behvaiourList.Add(AllBehaviours.gathererBehaviour);
        m_behvaiourList.Add(AllBehaviours.collecterBehaviour);

        var agentArray = FindObjectsOfType<AIAgent>();


        m_allAgents = new List<AIAgent>();
        foreach(var agent in agentArray)
        {
            m_allAgents.Add(agent);
        }

        cameraFocus.SetWorldState(m_worldState);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_allAgents[0].SetBehaviour(m_behvaiourList[1]);
        m_allAgents[0].SetWorldState(m_worldState);
        m_allAgents[0].animRenderer.material = woodCutterMaterial;

        m_allAgents[1].SetBehaviour(m_behvaiourList[2]);
        m_allAgents[1].SetWorldState(m_worldState);
        m_allAgents[1].animRenderer.material = gathererMaterial;

        m_allAgents[2].SetBehaviour(m_behvaiourList[3]);
        m_allAgents[2].SetWorldState(m_worldState);
        m_allAgents[2].animRenderer.material = collecterMaterial;

        for (int i = 3; i < m_allAgents.Count; i++)
        {
            m_allAgents[i].SetBehaviour(m_behvaiourList[0]);
            m_allAgents[i].SetWorldState(m_worldState);
        }

        if (m_allAgents.Count > 0)
        {
            cameraFocus.SetAIAgents(m_allAgents);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitWorldVariables()
    {
        // wood values
        m_worldState.CreateElement(WorldValues.storedWood, 0);
        m_worldState.CreateElement(WorldValues.woodAvailable, false);
        m_worldState.CreateElement(WorldValues.worldWoodCount, 0);

        // food values
        m_worldState.CreateElement(WorldValues.storedFood, 0);
        m_worldState.CreateElement(WorldValues.foodAvailable, false);
        m_worldState.CreateElement(WorldValues.worldFoodCount, 0);

        // axe values
        m_worldState.CreateElement(WorldValues.axeAvailable, true);
        m_worldState.CreateElement(WorldValues.worldAxeCount, 2);

        WorldValues.axeObjects.Add(debugAxe);
        WorldValues.axeObjects.Add(debugAxe2);

        // Init Actions
        ActionList.Init(woodPrefab, foodPrefab);

        // These are debug transforms, they should be more dynaimc values but for the purpose of this demonstration, this is fine.
        WorldValues.treeTarget = treeTarget;
        WorldValues.woodStoreTarget = woodStoreTarget;
        WorldValues.foodBushTarget = foodBushTarget;
        WorldValues.foodStoreTarget = foodStoreTarget;
    }
}
