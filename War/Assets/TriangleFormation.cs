using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Triangle Formation", menuName = "Formations/Triangle Formation")]

public class TriangleFormation : FormationBase
{
    [SerializeField] private TriangleFormationSettings settings = new TriangleFormationSettings();

    public override void ApplySettings(object _settings)
    {
        settings = _settings as TriangleFormationSettings;
        if (settings == null)
            throw new InvalidOperationException("Invalid settings applied to TriangleFormation");
    }

    public override IEnumerable<Vector3> EvaluatePoints()
    {
        var middleOffset = new Vector3(0, 0, settings.unitDepth * 0.5f);

        for (int z = 0; z < settings.unitDepth; z++)
        {
            for (var x = z * -1; x <= z; x++)
            {
                if (settings.hollow && z < settings.unitDepth - 1 && x > z * -1 && x < z)
                    continue;

                var pos = new Vector3(x + (z % 2 == 0 ? 0 : settings.nthOffset), 0, z);

                pos -= middleOffset;

                pos += GetNoise(pos);  // Assuming GetNoise method also adapted for settings

                pos *= settings.spread;

                yield return pos;
            }
        }
    }
}
