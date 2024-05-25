using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit/Base")]
public class UnitBase : ScriptableObject
{
    public bool isPlayerUnit;
    public string unitName;
    public int commandPoints;
    public int baseHealth;
    public int baseDamage;    
    public float baseSpeed;
    public float baseAttackSpeed;
    public float baseArmor;
    public float attackRange;

    public GameObject unitFormation;
    
    public UnitRace race;
    public TypeUnit type;

    public int TotalHealth => baseHealth + race.additionalHealth;
    public int TotalDamage => baseDamage + race.additionalDamage;
    public float TotalSpeed => baseSpeed + race.additionalSpeed;

    public bool isUnlocked;
    public Sprite icon;
}
public enum TypeUnit
{
    Soldier,
    Knight,
    Spearman,
    Shield_Spearman,
    Mage,
    Archer,
    Axe_Man,
    TwoHanded,
    Calvary,
    Balista,
    Catapult,
    Trebuchet
}
