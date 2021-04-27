using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    GOAPWorldState m_worldState = new GOAPWorldState();

    List<GOAPAgent> m_allAgents;// = new List<GOAPAgent>();

    List<GOAPBehaviour> m_behvaiourList = new List<GOAPBehaviour>();

    public List<GameObject> axeObjects;
    public List<GameObject> woodObjects = new List<GameObject>();

    [Header("Prefabs")]
    public GameObject woodPrefab;

    [Header("Debug values")]
    public int storedWood = 0;
    public int storedFood = 0;
    public bool axeAvailable = true;
    public bool woodAvailable = false;
    public int worldWoodCount = 0;
    public GameObject worldAxe = null;

    private void Awake()
    {
        WorldValues.Init();

        //for(int i = 0; i < WorldValues.worldValueList.Count; i++)
        //{
        //    m_worldState.CreateElement(WorldValues.worldValueList[i], default);
        //}

        m_worldState.CreateElement(WorldValues.storedWood, 0);
        m_worldState.CreateElement(WorldValues.storedFood, 0);
        m_worldState.CreateElement(WorldValues.axeAvailable, true);
        m_worldState.CreateElement(WorldValues.woodAvailable, false);
        m_worldState.CreateElement(WorldValues.worldWoodCount, 0);

        m_worldState.CreateElement(WorldValues.worldAxe, axeObjects[0]); // this class type should be a HoldableItem

        m_behvaiourList.Add(new AIHumanBehaviour(woodObjects, woodPrefab));

        var agentArray = FindObjectsOfType<GOAPAgent>();


        m_allAgents = new List<GOAPAgent>();
        foreach(var agent in agentArray)
        {
            m_allAgents.Add(agent);
        }

        // debugging
        foreach(var agent in m_allAgents)
        {
            agent.SetBehaviour(m_behvaiourList[0]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var agent in m_allAgents)
        {
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
        worldWoodCount  = m_worldState.GetElementValue<int>(WorldValues.worldWoodCount);
        worldAxe        = m_worldState.GetElementValue<GameObject>(WorldValues.worldAxe);
    }
}
