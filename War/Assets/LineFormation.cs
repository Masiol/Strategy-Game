using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Line Formation", menuName = "Formations/Line Formation")]
public class LineFormation : FormationBase
{
    [SerializeField] private int _unitWidth = 5; // Liczba jednostek w linii
    [SerializeField] private float _nthOffset = 0; // Przesuniêcie dla nieparzystych jednostek w linii

    public override IEnumerable<Vector3> EvaluatePoints()
    {
        var middleOffset = new Vector3(_unitWidth * 0.5f, 0, 0); // Aby wyœrodkowaæ formacjê wzglêdem pozycji rodzica

        for (var x = 0; x < _unitWidth; x++)
        {
            // Oblicz pozycjê dla ka¿dej jednostki w linii
            var pos = new Vector3(x + (x % 2 == 0 ? 0 : _nthOffset), 0, 0); // Dodaj przesuniêcie dla parzystych jednostek

            pos -= middleOffset; // Centruj formacjê

            pos += GetNoise(pos); // Dodaj ewentualny szum do pozycji

            pos *= Spread; // Zastosuj parametr rozproszenia, jeœli jest u¿ywany

            yield return pos;
        }
    }
}
