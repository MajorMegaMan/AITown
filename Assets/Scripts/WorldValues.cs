using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldValues
{
    public static List<string> worldValueList { get; private set; } = new List<string>();
    public static List<string> agentValueList { get; private set; } = new List<string>();

    // World values
    public static string storedWood { get; private set; } = "stored_wood";
    public static string storedFood { get; private set; } = "stored_food";
    public static string worldAxe { get; private set; } = "world_axe";
    public static string axeAvailable { get; private set; } = "axe_available";
    public static string woodAvailable { get; private set; } = "wood_available";

    // Agent Specific values
    public static string holdItemType { get; private set; } = "hold_item_type";
    public static string holdItemObject { get; private set; } = "hold_item_object";
    public static string hunger { get; private set; } = "hunger";
    public static string hasProcessedHunger { get; private set; } = "processed_hunger";

    public enum HoldItem
    {
        nothing,
        wood,
        food,
        axe
    }

    public static void Init()
    {
        worldValueList.Add(storedWood);
        worldValueList.Add(storedFood);
        worldValueList.Add(worldAxe);
        worldValueList.Add(axeAvailable);
    }
}
