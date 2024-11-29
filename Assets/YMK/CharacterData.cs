
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public float MaxPatience;
    public int TargetFloor;
    [MinMaxSlider("@DynamicRange.x", "@DynamicRange.y * 10f", true)]
    public Vector2Int   MinMaxValue;
    private Vector2Int  DynamicRange = new Vector2Int(0, 50);

}
