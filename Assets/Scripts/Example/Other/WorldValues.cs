using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldValues
{
    public static List<GameObject> axeObjects  = new List<GameObject>();
    public static List<GameObject> woodObjects = new List<GameObject>();
    public static List<GameObject> foodObjects = new List<GameObject>();

    // World values
    public static string storedWood { get; private set; } = "stored_wood";// int
    public static string storedFood { get; private set; } = "stored_food";// int
    public static string worldAxe { get; private set; } = "world_axe";// GameObject
    public static string axeAvailable { get; private set; } = "axe_available"; // bool
    public static string woodAvailable { get; private set; } = "wood_available";// bool
    public static string worldWoodCount { get; private set; } = "world_wood_count";// int
    public static string foodAvailable { get; private set; } = "food_available";// bool
    public static string worldFoodCount { get; private set; } = "world_food_count";// int


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

    public static GameObject CreateItem(GameObject itemPrefab, GameObject entity)
    {
        return CreateItem(itemPrefab, entity.transform.position + (Vector3.up * 2.5f));
    }

    public static GameObject CreateItem(GameObject itemPrefab, Vector3 location)
    {
        GameObject newItem = GameObject.Instantiate(itemPrefab);
        newItem.transform.position = location;
        return newItem;
    }

    public static GameObject Createfood(GameObject foodPrefab, GameObject entity)
    {
        GameObject newFood = CreateItem(foodPrefab, entity);
        foodObjects.Add(newFood);
        return newFood;
    }
}
