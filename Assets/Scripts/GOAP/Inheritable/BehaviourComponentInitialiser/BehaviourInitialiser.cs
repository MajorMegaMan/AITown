using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class BehaviourInitialiser
{
    public List<GOAPAgentAction<GameObject>> actionList = new List<GOAPAgentAction<GameObject>>();
    public GOAPWorldState requiredWorldStates = null;
    public BehaviourUpdater updater = null;
}
