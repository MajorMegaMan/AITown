using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GOAP;

public class NodeViewer : MonoBehaviour
{
    static List<NodeViewer> allNodeViewers = new List<NodeViewer>();

    public GOAPPlanner.Node targetNode = null;
    public NodeViewer parent = null;
    

    //Text textComponent;

    bool followMouse = false;

    private void Awake()
    {
        //textComponent = GetComponentInChildren<Text>();
        allNodeViewers.Add(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(followMouse)
        {
            
            transform.position = Input.mousePosition;
        }
    }

    public void SetNode(GOAPPlanner.Node node)
    {
        targetNode = node;
        if(targetNode.action != null)
        gameObject.name = targetNode.action.GetName();
    }

    public void UpdateText()
    {
        if(targetNode.action == null)
        {
            //textComponent.text = "start";
        }
        else
        {
            //textComponent.text = targetNode.action.GetName();
        }
    }

    public static void SetParents()
    {
        for(int i = 0; i < allNodeViewers.Count; i++)
        {
            for (int j = 0; j < allNodeViewers.Count; j++)
            {
                var firstNodeView = allNodeViewers[i];
                var secondNodeView = allNodeViewers[j];

                if(firstNodeView.targetNode.parent == secondNodeView.targetNode)
                {
                    firstNodeView.parent = secondNodeView;
                }
            }
        }

        foreach(var nodeViewer in allNodeViewers)
        {
            if(nodeViewer.parent != null)
            {
                nodeViewer.transform.SetParent(nodeViewer.parent.transform);
            }
        }
    }

    public void ToggleFollowMouse()
    {
        followMouse = !followMouse;
    }

    private void OnDrawGizmos()
    {
        if(targetNode != null && parent != null)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(transform.position);
            Vector3 parentWorldPos = Camera.main.ScreenToWorldPoint(parent.transform.position);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(worldPos, parentWorldPos);
        }
    }
}
