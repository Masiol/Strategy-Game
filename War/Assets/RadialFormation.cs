using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Radial Formation", menuName = "Formations/Radial Formation")]
public class RadialFormation : FormationBase
{
    [SerializeField] private CircleFormationSettings settings = new CircleFormationSettings();

    public override void ApplySettings(object _settings)
    {
        settings = _settings as CircleFormationSettings;
        if (settings == null)
            throw new InvalidOperationException("Invalid settings applied to CircleFormation");
    }

    public override IEnumerable<Vector3> EvaluatePoints()
    {
        var amountPerRing = settings.amount / settings.rings;
        var ringOffset = 0f;

        for (var i = 0; i < settings.rings; i++)
        {
            for (var j = 0; j < amountPerRing; j++)
            {
                var angle = j * Mathf.PI * (2 * settings.rotations) / amountPerRing + (i % 2 != 0 ? settings.nthOffset : 0);
                var radius = settings.radius + ringOffset + j * settings.radiusGrowthMultiplier;
                var x = Mathf.Cos(angle) * radius;
                var z = Mathf.Sin(angle) * radius;

                var pos = new Vector3(x, 0, z);

                pos += GetNoise(pos);
                pos *= settings.spread;

                yield return pos;
            }

            ringOffset += settings.ringOffset;
        }
    }
}