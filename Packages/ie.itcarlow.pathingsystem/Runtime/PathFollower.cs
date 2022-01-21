﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public BezierSpline spline;
    public float duration;

    private float progress;

    void Update()
    {
        progress += Time.deltaTime / duration;
        if (progress > 1f)
            progress -= 1f;

        transform.localPosition = spline.GetPoint(progress);
    }
}
