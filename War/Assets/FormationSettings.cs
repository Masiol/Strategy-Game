using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BoxFormationSettings
{
    public int unitWidth = 5;
    public int unitDepth = 5;
    public bool hollow = false;
    public  float nthOffset = 0;
    public float noise = 0;
    public float spread = 1;
}

[System.Serializable]
public class CircleFormationSettings
{
    public int amount = 10;
    public float radius = 1;
    public float radiusGrowthMultiplier = 0;
    public float rotations = 1;
    public int rings = 1;
    public float ringOffset = 1;
    public float nthOffset = 0;
    public float noise = 0;
    public float spread = 1;
}
[System.Serializable]
public class LineFormationSettings
{
    public int unitWidth = 5;
    public float nthOffset = 0;
    public float noise = 0;
    public float spread = 1;
}
[System.Serializable]
public class TriangleFormationSettings
{
    public int unitDepth = 5;
    public bool hollow = false;
    public float nthOffset = 0;
    public float noise = 0;
    public float spread = 1;
}
[System.Serializable]
public class FormationsSettings
{
    public BoxFormationSettings boxFormationSettings;
    public LineFormationSettings lineFormationSettings;
    public CircleFormationSettings circleFormationSettings;
    public TriangleFormationSettings triangleFormationSettings;
}
public class FormationSettings : MonoBehaviour
{
    public FormationsSettings formationsSettings;
}
