using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    public FloorData[] floorData;
}

[CreateAssetMenu(fileName = "FloorData", menuName = "Scriptable Objects/FloorData")]
public class FloorData : ScriptableObject
{
    public CharacterData[] characterList;
}


