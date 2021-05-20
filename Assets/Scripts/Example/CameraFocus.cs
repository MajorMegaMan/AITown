using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GOAP;

public class CameraFocus : MonoBehaviour
{
    public Vector3 offset = new Vector3(0.0f, 5.0f, 10.0f);
    
    Camera mainCam;

    List<AIAgent> allAgents = null;
    GOAPWorldState worldstate = null;
    GOAPWorldState agentSelfish = null;

    int currentIndex = -1;

    [HideInInspector]public Transform followTarget;
    [HideInInspector]public AIAgent agentTarget;

    public Text worldstateText;
    public Text selfishText;
    public Text actionPlanText;
    
    private void Awake()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = followTarget.position;
        position += offset;
        transform.position = position;

        mainCam.transform.LookAt(followTarget);

        DumbUpdateText();
    }

    void DumbUpdateText()
    {
        UpdateWorldStateText();
        UpdateSelfish();
        UpdatePlannerText();
    }

    void UpdateWorldStateText()
    {
        worldstateText.text = GetWorldStateValues(worldstate);
    }

    void UpdateSelfish()
    {
        selfishText.text = GetWorldStateValues(agentSelfish);
    }

    string GetWorldStateValues(GOAPWorldState worldState)
    {
        string worldValueString = "";

        foreach (string name in worldState.GetNames())
        {
            object obj = worldState.GetElementValue(name);

            string objString = "null";
            if(obj != null)
            {
                objString = obj.ToString();
            }
            string worldValueLine = name + " : " + objString;
            worldValueLine += "\n";

            worldValueString += worldValueLine.Replace('_', ' ');
        }

        return worldValueString;
    }

    void UpdatePlannerText()
    {
        var plan = agentTarget.GetPlan();
        string planText = "Goal";
        planText += "\n";

        var goal = agentTarget.GetGoal();

        if(goal != null)
        {
            planText += GetWorldStateValues(goal);
        }
        else
        {
            planText += "No Goal";
            planText += "\n";
        }

        planText += "------------\n";
        planText += "Plan";
        planText += "\n";

        var currentAct = agentTarget.GetCurrentAction();
        if (currentAct != null)
        {
            planText += agentTarget.GetCurrentAction().GetName();
            planText += "\n";

            foreach (var act in plan)
            {
                planText += act.GetName();
                planText += "\n";
            }
        }
        else
        {
            planText += "No Plan";
        }

        actionPlanText.text = planText;
    }

    void SetFollowTarget(AIAgent aiAgent)
    {
        agentTarget = aiAgent;
        followTarget = aiAgent.gameObject.transform;
        agentSelfish = agentTarget.GetSelfishNeeds();
    }

    public void SetWorldState(GOAP.GOAPWorldState worldstate)
    {
        this.worldstate = worldstate;
    }

    public void SetAIAgents(List<AIAgent> agentList)
    {
        allAgents = agentList;
        if(agentList.Count > 0)
        {
            SetFollowTarget(agentList[0]);
            currentIndex = 0;
        }
    }

    public void FocusNext()
    {
        currentIndex++;
        if(currentIndex >= allAgents.Count)
        {
            currentIndex = 0;
        }

        SetFollowTarget(allAgents[currentIndex]);
    }

    public void FocusPrevious()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = allAgents.Count - 1;
        }

        SetFollowTarget(allAgents[currentIndex]);
    }
}
