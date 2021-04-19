﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldValues
{
    public static List<string> worldValueList { get; private set; } = new List<string>();
    public static List<string> agentValueList { get; private set; } = new List<string>();

    // World values
    public static string storedWood { get; private set; } = "stored_wood";
    public static string storedFood { get; private set; } = "stored_food";

    // Agent Specific values
    public static string holdingWood { get; private set; } = "holding_wood";
    public static string holdingFood { get; private set; } = "holding_food";
    public static string hunger { get; private set; } = "hunger";
    public static string hasProcessedHunger { get; private set; } = "processed_hunger";

    public static void Init()
    {
        worldValueList.Add(storedWood);

        worldValueList.Add(storedFood);
    }
}
