using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldValues
{
    // These variables are handy and used by actions, but they are not needed in the actual GOAPPlanner
    public static List<GameObject> axeObjects  = new List<GameObject>();
    public static List<GameObject> woodObjects = new List<GameObject>();
    public static List<GameObject> foodObjects = new List<GameObject>();

    // Debug values
    public static Transform treeTarget;
    public static Transform woodStoreTarget;
    public static Transform foodBushTarget;
    public static Transform foodStoreTarget;

    // GOAP WorldState value strings
    // World values
    public static string worldAxeCount { get; private set; } = "world_axe_count";// int
    public static string axeAvailable { get; private set; } = "axe_available"; // bool

    public static string storedWood { get; private set; } = "stored_wood";// int
    public static string worldWoodCount { get; private set; } = "world_wood_count";// int
    public static string woodAvailable { get; private set; } = "wood_available";// bool
    
    public static string storedFood { get; private set; } = "stored_food";// int
    public static string worldFoodCount { get; private set; } = "world_food_count";// int
    public static string foodAvailable { get; private set; } = "food_available";// bool

    // Agent Specific values
    public static string holdItemType { get; private set; } = "hold_item_type";// HoldItemType
    public static string holdItemObject { get; private set; } = "hold_item_object";// GameObject
    public static string hunger { get; private set; } = "hunger";// float
    public static string hasProcessedHunger { get; private set; } = "processed_hunger"; // bool

    public enum HoldItemType
    {
        nothing,
        wood,
        food,
        axe
    }
}
