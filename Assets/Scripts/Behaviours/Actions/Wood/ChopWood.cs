using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopWood : GOAPAction
{
    List<GameObject> instantiatedWoodObjects;
    GameObject woodPrefab;

    public ChopWood(object instantiatedWoodObjects, object woodPrefab)
    {
        preconditions.CreateElement(WorldValues.holdItemType, WorldValues.HoldItemType.axe);
        effects.CreateElement(WorldValues.woodAvailable, true);
        effects.CreateElement(WorldValues.worldWoodCount, +1);

        name = "Chop Wood";

        this.instantiatedWoodObjects = (List<GameObject>)instantiatedWoodObjects;
        this.woodPrefab = (GameObject)woodPrefab;
    }

    public override void AddEffects(GOAPWorldState state)
    {
        state.SetElementValue(WorldValues.woodAvailable, true);

        var woodCountData = state.GetData(WorldValues.worldWoodCount);
        int value = woodCountData.ConvertValue<int>();
        value++;
        woodCountData.value = value;
    }

    public override ActionState PerformAction(GOAPAgent agent, GOAPWorldState worldState)
    {
        // Instantiate wood object
        GameObject newWood = GameObject.Instantiate(woodPrefab);
        newWood.transform.position = agent.transform.position + (Vector3.up * 2.5f);
        instantiatedWoodObjects.Add(newWood);
        AddEffects(worldState);
        return ActionState.completed;
    }

    public override bool EnterAction(GOAPAgent agent)
    {
        // find tree to chop
        // debug values at the moment
        agent.actionObject = agent.treeTarget.gameObject;
        agent.m_actionTargetLocation = agent.treeTarget.position;
        return true;
    }

    public override bool IsInRange(GOAPAgent agent)
    {
        // is tree in range
        return (agent.transform.position - agent.actionObject.transform.position).magnitude < agent.stoppingDistance;
    }
}
