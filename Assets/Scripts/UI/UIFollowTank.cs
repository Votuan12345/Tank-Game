using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTank : FollowTank
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    protected override void LateUpdate()
    {
        if(objectToFollow != null)
        {
            rectTransform.anchoredPosition = objectToFollow.localPosition;
        }
    }
}
