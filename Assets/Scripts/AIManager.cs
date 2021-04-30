using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using U_GOAPAgent = GOAPAgent<UnityEngine.GameObject>;
using U_GOAPBehaviour = GOAPBehaviour<UnityEngine.GameObject>;


public class AIManager : MonoBehaviour
{
    
    GOAPWorldState m_worldState = new GOAPWorldState();

    List<AIAgent> m_allAgents;// = new List<GOAPAgent>();

    List<U_GOAPBehaviour> m_behvaiourList = new List<U_GOAPBehaviour>();

    public List<GameObject> axeObjects;
    public List<GameObject> woodObjects = new List<GameObject>();
    public List<GameObject> foodObjects = new List<GameObject>();

    [Header("Prefabs")]
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
        WorldValues.Init();

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
        m_worldState.CreateElement(WorldValues.worldAxe, axeObjects[0]); // this class type should be a HoldableItem

        m_behvaiourList.Add(new AIHumanBehaviour(woodObjects, woodPrefab, foodObjects, foodPrefab));

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
        foreach (var agent in m_allAgents)
        {
            agent.SetBehaviour(m_behvaiourList[0]);
            agent.SetWorldState(m_worldState);
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
