
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public Character    character;
    public float        spawnTime;
    public float        maxPatience;
    public int          targetFloor;

    [MinMaxSlider("@dynamicRange.x", "@dynamicRange.y * 10f", true)]
    public Vector2Int   pointValue;
    private Vector2Int  dynamicRange = new Vector2Int(0, 50);

}
