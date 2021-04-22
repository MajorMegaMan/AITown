using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableItem : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    Transform followTarget = null;

    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(followTarget != null)
        {
            transform.position = followTarget.position + offset;
        }
    }

    public void AttachObject(Transform target)
    {
        followTarget = target;
        body.isKinematic = true;
    }

    public void DetachObject()
    {
        followTarget = null;
        body.isKinematic = false;
    }
}
