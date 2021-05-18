using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPBehaviour = GOAP.GOAPBehaviour<UnityEngine.GameObject>;

public class WoodCutterBehaviour : AIBehaviour
{
    public WoodCutterBehaviour()
    {
        // Initialise Action List
        AddBehaviourComponent(AllBehaviourComponents.hungerComponent);
        AddBehaviourComponent(AllBehaviourComponents.woodCutterComponent);
    }
}
