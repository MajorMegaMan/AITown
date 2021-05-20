using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

using U_GOAPAgentAction = GOAP.GOAPAgentAction<UnityEngine.GameObject>;

public static class ActionList
{
    static List<U_GOAPAgentAction> m_humanActions;
    public static List<U_GOAPAgentAction> humanActions
    {
        get 
        {
            InitHumanList();
            return m_humanActions;
        }
    }

    static List<U_GOAPAgentAction> m_humanWoodActions;
    public static List<U_GOAPAgentAction> humanWoodActions
    {
        get
        {
            InitHumanWoodList();
            return m_humanWoodActions;
        }
    }

    static List<U_GOAPAgentAction> m_humanFoodActions;
    public static List<U_GOAPAgentAction> humanFoodActions
    {
        get
        {
            InitHumanFoodList();
            return m_humanFoodActions;
        }
    }

    static U_GOAPAgentAction m_chopWood;
    public static U_GOAPAgentAction chopWood
    {
        get
        {
            if (m_chopWood == null)
            {
                m_chopWood = new ChopWood();
            }
            return m_chopWood;
        }
    }

    static U_GOAPAgentAction m_pickUpWood;
    public static U_GOAPAgentAction pickUpWood
    {
        get
        {
            if (m_pickUpWood == null)
            {
                m_pickUpWood = new PickUpWood();
            }
            return m_pickUpWood;
        }
    }

    static U_GOAPAgentAction m_storeWood;
    public static U_GOAPAgentAction storeWood
    {
        get
        {
            if (m_storeWood == null)
            {
                m_storeWood = new StoreWood();
            }
            return m_storeWood;
        }
    }

    static U_GOAPAgentAction m_gatherFood;
    public static U_GOAPAgentAction gatherFood
    {
        get
        {
            if (m_gatherFood == null)
            {
                m_gatherFood = new GatherFood();
            }
            return m_gatherFood;
        }
    }

    static U_GOAPAgentAction m_pickUpFood;
    public static U_GOAPAgentAction pickUpFood
    {
        get
        {
            if (m_pickUpFood == null)
            {
                m_pickUpFood = new PickUpFood();
            }
            return m_pickUpFood;
        }
    }

    static U_GOAPAgentAction m_storeFood;
    public static U_GOAPAgentAction storeFood
    {
        get
        {
            if (m_storeFood == null)
            {
                m_storeFood = new StoreFood();
            }
            return m_storeFood;
        }
    }

    static U_GOAPAgentAction m_eatFood;
    public static U_GOAPAgentAction eatFood
    {
        get
        {
            if (m_eatFood == null)
            {
                m_eatFood = new EatFood();
            }
            return m_eatFood;
        }
    }

    static U_GOAPAgentAction m_pickUpAxe;
    public static U_GOAPAgentAction pickUpAxe
    {
        get
        {
            if (m_pickUpAxe == null)
            {
                m_pickUpAxe = new PickUpAxe();
            }
            return m_pickUpAxe;
        }
    }

    static U_GOAPAgentAction m_dropAxe;
    public static U_GOAPAgentAction dropAxe
    {
        get
        {
            if (m_dropAxe == null)
            {
                m_dropAxe = new DropAxe();
            }
            return m_dropAxe;
        }
    }


    public static void Init(GameObject woodPrefab, GameObject foodPrefab)
    {
        ChopWood.SetWoodPrefab(woodPrefab);
        ChopWood.SetWoodObjectsList(WorldValues.woodObjects);

        PickUpWood.SetWoodObjectsList(WorldValues.woodObjects);

        GatherFood.SetFoodPrefab(foodPrefab);
        GatherFood.SetFoodObjectsList(WorldValues.foodObjects);

        PickUpFood.SetFoodObjectsList(WorldValues.foodObjects);

        PickUpAxe.SetAxeObjectsList(WorldValues.axeObjects);

        DropAxe.SetAxeObjectsList(WorldValues.axeObjects);
    }

    static void InitHumanList()
    {
        if(m_humanActions == null)
        {
            m_humanActions = new List<U_GOAPAgentAction>();

            foreach (var act in humanWoodActions)
            {
                if(!m_humanActions.Contains(act))
                {
                    m_humanActions.Add(act);
                }
            }

            foreach (var act in humanFoodActions)
            {
                if (!m_humanActions.Contains(act))
                {
                    m_humanActions.Add(act);
                }
            }
        }
    }

    static void InitHumanWoodList()
    {
        if (m_humanWoodActions == null)
        {
            m_humanWoodActions = new List<U_GOAPAgentAction>();

            m_humanWoodActions.Add(chopWood);
            m_humanWoodActions.Add(pickUpWood);
            m_humanWoodActions.Add(storeWood);

            m_humanWoodActions.Add(pickUpAxe);
            m_humanWoodActions.Add(dropAxe);
        }
    }

    static void InitHumanFoodList()
    {
        if (m_humanFoodActions == null)
        {
            m_humanFoodActions = new List<U_GOAPAgentAction>();

            m_humanFoodActions.Add(gatherFood);
            m_humanFoodActions.Add(pickUpFood);
            m_humanFoodActions.Add(storeFood);
            m_humanFoodActions.Add(eatFood);
        }
    }
}
