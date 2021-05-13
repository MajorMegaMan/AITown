using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public abstract class BehaviourUpdater
{
    static List<BehaviourUpdater> allUpdaters = new List<BehaviourUpdater>();

    public BehaviourUpdater()
    {
        allUpdaters.Add(this);
    }

    public abstract void FindGoal(GOAPWorldState agentWorldState, ref GOAPWorldState targetGoal);
    public abstract void Update(GOAPAgent<GameObject> agent, GOAPWorldState agentSelfishNeeds);
}
