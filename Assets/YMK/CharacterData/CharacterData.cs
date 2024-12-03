using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public Character    character;
    public float        spawnTime;
    public float        maxPatience;
    public int          targetFloor;

    public int stageNum;
    public int idx;

    public string talkKey1 => "Stage_" + stageNum + "_" + idx + "_1";
    public string talkKey2 => "Stage_" + stageNum + "_" + idx + "_2";
    public string talkKey3 => "Stage_" + stageNum + "_" + idx + "_3";

    public string angryKey => "Stage_" + stageNum + "_" + idx + "_A";
    public string thankKey => "Stage_" + stageNum + "_" + idx + "_T";

    public List<CharacterData> requireEvent;

    public int   pointValue;

}
