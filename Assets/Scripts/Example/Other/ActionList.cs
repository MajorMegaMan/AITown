using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgentAction = GOAP.GOAPAgentAction<UnityEngine.GameObject>;

public static class ActionList
{
    public static bool isInitialised = false;

    public static List<U_GOAPAgentAction> humanActions;
    public static List<U_GOAPAgentAction> humanWoodActions;
    public static List<U_GOAPAgentAction> humanFoodActions;

    public static U_GOAPAgentAction chopWood;
    public static U_GOAPAgentAction pickUpWood;
    public static U_GOAPAgentAction storeWood;


    public static U_GOAPAgentAction gatherFood;
    public static U_GOAPAgentAction pickUpFood;
    public static U_GOAPAgentAction storeFood;
    public static U_GOAPAgentAction eatFood;


    public static U_GOAPAgentAction pickUpAxe;
    public static U_GOAPAgentAction dropAxe;

    public static void Init(GameObject woodPrefab, GameObject foodPrefab)
    {
        chopWood      = new ChopWood(WorldValues.woodObjects, woodPrefab);
        pickUpWood    = new PickUpWood(WorldValues.woodObjects);
        storeWood     = new StoreWood();

        gatherFood    = new GatherFood(WorldValues.foodObjects, foodPrefab);
        pickUpFood    = new PickUpFood(WorldValues.foodObjects);
        storeFood     = new StoreFood();
        eatFood       = new EatFood();

        pickUpAxe     = new PickUpAxe();
        dropAxe       = new DropAxe();

        InitHumanList();
        isInitialised = true;
    }

    static void InitHumanList()
    {
        humanWoodActions = new List<U_GOAPAgentAction>();

        humanWoodActions.Add(chopWood);
        humanWoodActions.Add(pickUpWood);
        humanWoodActions.Add(storeWood);

        humanWoodActions.Add(pickUpAxe);
        humanWoodActions.Add(dropAxe);

        humanFoodActions = new List<U_GOAPAgentAction>();

        humanFoodActions.Add(gatherFood);
        humanFoodActions.Add(pickUpFood);
        humanFoodActions.Add(storeFood);
        humanFoodActions.Add(eatFood);

        humanActions = new List<U_GOAPAgentAction>();

        foreach(var act in humanWoodActions)
        {
            humanActions.Add(act);
        }

        foreach (var act in humanFoodActions)
        {
            humanActions.Add(act);
        }
    }

    public static void EditorClear()
    {
        if(!Application.isPlaying)
        {
            chopWood    = null;
            pickUpWood  = null;
            storeWood   = null;

            gatherFood  = null;
            pickUpFood  = null;
            storeFood   = null;
            eatFood     = null;

            pickUpAxe   = null;
            dropAxe     = null;

            humanActions = null;
            humanFoodActions = null;
            humanWoodActions = null;

            isInitialised = false;
        }
    }
}
