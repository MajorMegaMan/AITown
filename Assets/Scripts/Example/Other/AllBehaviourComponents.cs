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
}
