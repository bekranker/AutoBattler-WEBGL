using UnityEngine;

[CreateAssetMenu(fileName = "NPC Type", menuName = "NPC Section/NPC")]
public class NPCType : ScriptableObject
{
    public float Health;
    public Sprite NPCSprite;
    public NPCAttackType NPCAttackType;
    public int Level;
}