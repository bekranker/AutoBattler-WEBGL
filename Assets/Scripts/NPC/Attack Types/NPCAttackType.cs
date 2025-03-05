using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "NPC Attack Type", menuName = "NPC Section/NPC Attack")]
public class NPCAttackType : ScriptableObject
{
    public float AttackDelay;
    public float Damage;
    public Vector2 InitialFightPosition;
    [ListDrawerSettings]
    [SerializeReference, PolymorphicDrawerSettings]
    public IAttackAnimation AttackAnimationType;

}