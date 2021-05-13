using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public abstract class AIAgentAction : GOAPAgentAction<GameObject>
{
    protected string animTrigger = "idle";

    public string GetAnimTrigger()
    {
        return animTrigger;
    }
}
