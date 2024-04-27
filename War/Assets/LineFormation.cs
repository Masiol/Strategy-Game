using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Line Formation", menuName = "Formations/Line Formation")]
public class LineFormation : FormationBase
{
    [SerializeField] private LineFormationSettings settings = new LineFormationSettings();

    public override void ApplySettings(object _settings)
    {
       settings = _settings as LineFormationSettings;
        if (settings == null)
            throw new InvalidOperationException("Invalid settings applied to LineFormation");
    }


    public override IEnumerable<Vector3> EvaluatePoints()
    {
        var middleOffset = new Vector3(settings.unitWidth * 0.5f, 0, 0);

        for (var x = 0; x < settings.unitWidth; x++)
        {
            
            var pos = new Vector3(x + (x % 2 == 0 ? 0 : settings.nthOffset), 0, 0);

            pos -= middleOffset;

            pos += GetNoise(pos);

            pos *= Spread; 

            yield return pos;
        }
    }
}
