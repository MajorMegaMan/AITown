using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    GOAPWorldState m_worldState = new GOAPWorldState();

    public List<GOAPAgent> m_allAgents;// = new List<GOAPAgent>();

    List<GOAPBehaviour> m_behvaiourList = new List<GOAPBehaviour>();

    public List<GameObject> axeObjects;

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

        m_worldState.CreateElement(WorldValues.worldAxe, null); // this class type should be a HoldableItem

        m_behvaiourList.Add(new AIHumanBehaviour());

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
        
    }
}
