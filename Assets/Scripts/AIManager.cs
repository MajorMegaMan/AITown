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

    public GameObject debugAxe;

    [Header("Prefabs")]
    public GameObject axePrefab;
    public GameObject woodPrefab;
    public GameObject foodPrefab;

    [Header("Debug values")]
    [ReadOnly] public int storedWood = 0;
    [ReadOnly] public int storedFood = 0;
    [ReadOnly] public bool axeAvailable = true;
    [ReadOnly] public bool woodAvailable = false;
    [ReadOnly] public bool foodAvailable = false;
    [ReadOnly] public int worldWoodCount = 0;
    [ReadOnly] public GameObject worldAxe = null;

    private void Awake()
    {
        //for(int i = 0; i < WorldValues.worldValueList.Count; i++)
        //{
        //    m_worldState.CreateElement(WorldValues.worldValueList[i], default);
        //}


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
        m_worldState.CreateElement(WorldValues.worldAxe, debugAxe); // this class type should be a HoldableItem

        ActionList.Init(woodPrefab, foodPrefab);
        BehaviourComponenets.Init();

        m_behvaiourList.Add(new AIHumanBehaviour());
        m_behvaiourList.Add(new WoodCutterBehaviour());

        var agentArray = FindObjectsOfType<AIAgent>();


        m_allAgents = new List<AIAgent>();
        foreach(var agent in agentArray)
        {
            m_allAgents.Add(agent);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_allAgents[0].SetBehaviour(m_behvaiourList[1]);
        m_allAgents[0].SetWorldState(m_worldState);

        for (int i = 1; i < m_allAgents.Count; i++)
        {
            m_allAgents[i].SetBehaviour(m_behvaiourList[0]);
            m_allAgents[i].SetWorldState(m_worldState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        storedWood      = m_worldState.GetElementValue<int>(WorldValues.storedWood);
        storedFood      = m_worldState.GetElementValue<int>(WorldValues.storedFood);
        axeAvailable    = m_worldState.GetElementValue<bool>(WorldValues.axeAvailable);
        woodAvailable   = m_worldState.GetElementValue<bool>(WorldValues.woodAvailable);
        foodAvailable   = m_worldState.GetElementValue<bool>(WorldValues.foodAvailable);
        worldWoodCount  = m_worldState.GetElementValue<int>(WorldValues.worldWoodCount);
        worldAxe        = m_worldState.GetElementValue<GameObject>(WorldValues.worldAxe);
    }
}
