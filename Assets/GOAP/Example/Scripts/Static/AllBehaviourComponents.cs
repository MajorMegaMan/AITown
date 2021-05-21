using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public static class AllBehaviourComponents
{
    static BehaviourComponent m_hungerComponent;
    public static BehaviourComponent hungerComponent
    {
        get
        {
            if (m_hungerComponent == null)
            {
                m_hungerComponent = new HungerComponent();
            }
            return m_hungerComponent;
        }
    }

    static BehaviourComponent m_woodCutterComponent;
    public static BehaviourComponent woodCutterComponent
    {
        get
        {
            if (m_woodCutterComponent == null)
            {
                m_woodCutterComponent = new WoodCutterComponent();
            }
            return m_woodCutterComponent;
        }
    }

    static BehaviourComponent m_gathererComponent;
    public static BehaviourComponent gathererComponent
    {
        get
        {
            if (m_gathererComponent == null)
            {
                m_gathererComponent = new GathererComponent();
            }
            return m_gathererComponent;
        }
    }

    static BehaviourComponent m_collecterComponent;
    public static BehaviourComponent collecterComponent
    {
        get
        {
            if (m_collecterComponent == null)
            {
                m_collecterComponent = new CollecterComponent();
            }
            return m_collecterComponent;
        }
    }

    static BehaviourComponent m_basicComponent;
    public static BehaviourComponent basicComponent
    {
        get
        {
            if (m_basicComponent == null)
            {
                m_basicComponent = new BasicComponent();
            }
            return m_basicComponent;
        }
    }
}
