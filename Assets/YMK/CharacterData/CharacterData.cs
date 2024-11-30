using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public Character    character;
    public float        spawnTime;
    public float        maxPatience;
    public int          targetFloor;

    public string       talkText1;
    public string       talkText2;
    public string       talkText3;

    public string       angryText;
    public string       thankText;

    public List<CharacterData> requireEvent;

    public int   pointValue;

}
