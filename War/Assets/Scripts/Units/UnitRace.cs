using UnityEngine;

public enum RaceType
{
    Human,
    Orc,
    Elf,
    Undead
}


[CreateAssetMenu(fileName = "New Race", menuName = "Unit/Race")]
public class UnitRace : ScriptableObject
{
    public RaceType raceType;
    public int additionalHealth;
    public int additionalDamage;
    public float additionalSpeed; 

}
