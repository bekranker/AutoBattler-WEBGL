using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Pack", menuName = "Create NPC Pack", order = 1)]
public class PackType : ScriptableObject
{
    [ShowInInspector] public List<NPCType> NPCPack = new();
}