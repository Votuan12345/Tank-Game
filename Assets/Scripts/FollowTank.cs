using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTank : MonoBehaviour
{
    public Transform objectToFollow;

    protected virtual void LateUpdate()
    {
        if (objectToFollow != null)
        {
            transform.position = objectToFollow.position;
        }
    }
}
