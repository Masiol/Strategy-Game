using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    public UnitRace race;
    public TypeUnit type;

    public int TotalHealth => baseHealth + race.additionalHealth;
    public int TotalDamage => baseDamage + race.additionalDamage;
    public float TotalSpeed => baseSpeed + race.additionalSpeed;

    public bool isUnlocked;
    public Sprite icon;

    public AnimSettings animSettings;


    public virtual void Attack()
    {
        Debug.Log($"{race.raceType} {unitName} attacks!");
    }
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

[Serializable]
public class AnimSettings
{
    public AnimSettingsIdle animSettingsIdle;
    public AnimSettingsRun animSettingsRun;
    public AnimSettingsAttack animSettingsAttack;
}

[Serializable]
public class AnimSettingsIdle
{
    public float speed;
    public int animationInt;
    public int weaponType;
    public int meleeTypeInt;
    public List<AnimLayers> animLayers = new List<AnimLayers>();
}
[Serializable]
public class AnimSettingsRun
{
    public float speed;
    public int animationInt;
    public int weaponType;
    public int meleeTypeInt;
    public List<AnimLayers> animLayers = new List<AnimLayers>();
}
[Serializable]
public class AnimSettingsAttack
{
    public float speed;
    public int animationInt;
    public int weaponType;
    public int meleeTypeInt;
    public List<AnimLayers> animLayers = new List<AnimLayers>();
}

[Serializable]
public class AnimLayers
{
    public int layersIndex;
    public LayerWeight layerWeight;
}
[Serializable]
public class LayerWeight
{
    public float layerWeight;
    public bool changeSmooth;
    public float speed;
}


