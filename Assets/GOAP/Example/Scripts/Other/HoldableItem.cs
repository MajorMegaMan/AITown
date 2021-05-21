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

        transform.rotation = target.transform.rotation;
    }

    public void DetachObject()
    {
        followTarget = null;
        body.isKinematic = false;

        AddRandomForce();
    }

    public void AddRandomForce()
    {
        float rand = Random.Range(0.0f, 360.0f);
        rand -= 180.0f;

        Vector3 direction = Quaternion.AngleAxis(rand, Vector3.up) * Vector3.forward;

        body.AddForce(direction * 5, ForceMode.Impulse);
    }

    public void SetRigidBody()
    {
        body = GetComponent<Rigidbody>();
    }
}
