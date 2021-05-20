using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllBehaviours
{
    static AIBehaviour m_basicBehaviour;
    public static AIBehaviour basicBehaviour
    {
        get
        {
            if (m_basicBehaviour == null)
            {
                m_basicBehaviour = new AIBehaviour();
                m_basicBehaviour.AddBehaviourComponent(AllBehaviourComponents.hungerComponent);
                m_basicBehaviour.AddBehaviourComponent(AllBehaviourComponents.basicComponent);
            }
            return m_basicBehaviour;
        }
    }

    static AIBehaviour m_woodCutterBehaviour;
    public static AIBehaviour woodCutterBehaviour
    {
        get
        {
            if (m_woodCutterBehaviour == null)
            {
                m_woodCutterBehaviour = new AIBehaviour();
                m_woodCutterBehaviour.AddBehaviourComponent(AllBehaviourComponents.hungerComponent);
                m_woodCutterBehaviour.AddBehaviourComponent(AllBehaviourComponents.woodCutterComponent);
            }
            return m_woodCutterBehaviour;
        }
    }

    static AIBehaviour m_gathererBehaviour;
    public static AIBehaviour gathererBehaviour
    {
        get
        {
            if (m_gathererBehaviour == null)
            {
                m_gathererBehaviour = new AIBehaviour();
                m_gathererBehaviour.AddBehaviourComponent(AllBehaviourComponents.hungerComponent);
                m_gathererBehaviour.AddBehaviourComponent(AllBehaviourComponents.gathererComponent);
            }
            return m_gathererBehaviour;
        }
    }

    static AIBehaviour m_collecterBehaviour;
    public static AIBehaviour collecterBehaviour
    {
        get
        {
            if (m_collecterBehaviour == null)
            {
                m_collecterBehaviour = new AIBehaviour();
                m_collecterBehaviour.AddBehaviourComponent(AllBehaviourComponents.hungerComponent);
                m_collecterBehaviour.AddBehaviourComponent(AllBehaviourComponents.collecterComponent);
            }
            return m_collecterBehaviour;
        }
    }
}
