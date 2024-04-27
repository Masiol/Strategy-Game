using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Box Formation", menuName = "Formations/Box Formation")]

public class BoxFormation : FormationBase
{
    [SerializeField] private BoxFormationSettings settings = new BoxFormationSettings();

    public override void ApplySettings(object _settings)
    {
        settings = _settings as BoxFormationSettings;
        if (settings == null)
            throw new InvalidOperationException("Invalid settings applied to BoxFormation");
    }

    public override IEnumerable<Vector3> EvaluatePoints()
    {
        var middleOffset = new Vector3(settings.unitWidth * 0.5f, 0, settings.unitDepth * 0.5f);

        for (var x = 0; x < settings.unitWidth; x++)
        {
            for (var z = 0; z < settings.unitDepth; z++)
            {
                if (settings.hollow && x != 0 && x != settings.unitWidth - 1 && z != 0 && z != settings.unitDepth - 1)
                    continue;

                var pos = new Vector3(x + (z % 2 == 0 ? 0 : settings.nthOffset), 0, z) - middleOffset;
                pos += GetNoise(pos);
                pos *= settings.spread;

                yield return pos;
            }
        }
    }
}
