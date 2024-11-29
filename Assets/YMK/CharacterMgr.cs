using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterMgr : SerializedMonoBehaviour
{
    private static CharacterMgr Instance = null;

    private Dictionary<Character, CharacterObj> characterObj = new Dictionary<Character, CharacterObj>();
    private Dictionary<Character, Queue<CharacterObj>> poolDictionary = new Dictionary<Character, Queue<CharacterObj>>();
    private Dictionary<Character, HashSet<CharacterObj>> useList = new Dictionary<Character, HashSet<CharacterObj>>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            Instance.Init();
            DontDestroyOnLoad(Instance);
        }    
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        if (Instance != null)
            return;
        Instance = this;

        //ĳ���� ������Ʈ �ε�
        Array enumValues = Enum.GetValues(typeof(Character));
        for (int i = 0; i < enumValues.Length; i++)
        {
            Character day = (Character)enumValues.GetValue(i);  // enum �� ���
            poolDictionary.Add(day, new Queue<CharacterObj>());
            useList.Add(day, new HashSet<CharacterObj>());
        }

        string loadFormat = "Character/{0}";
        KeyValuePair<Character, string>[] kvpArr = new KeyValuePair<Character, string>[]
        {
            new KeyValuePair<Character, string>(Character.Random,"RandomCharacter")
        };

        foreach (KeyValuePair<Character, string> pair in kvpArr)
        {
            string route = string.Format(loadFormat, pair.Value);
            CharacterObj obj = Resources.Load<CharacterObj>(route);
            characterObj.Add(pair.Key, obj);
        }
    }

    public static CharacterObj CreateCharacterObj(CharacterData pCharacterData,float pSpawnTime)
    {
        //ĳ���� ����
        Character key = pCharacterData.character;
        if (Instance.poolDictionary.ContainsKey(key) == false)
            return null;

        CharacterObj obj = null;
        Queue<CharacterObj> queue = Instance.poolDictionary[key];
        if (queue.Count > 0)
            obj = Instance.poolDictionary[key].Dequeue();
        else
            obj = Instantiate(Instance.characterObj[key]);
        obj.Init(pSpawnTime);

        obj.gameObject.SetActive(true);
        Instance.useList[key].Add(obj);

        return obj;
    }

    public static void RemoveCharacterObj(CharacterObj pCharacterObj)
    {
        //ĳ���� ����
        Character key = pCharacterObj.characterData.character;
        Instance.useList[key].Remove(pCharacterObj);
        Instance.poolDictionary[key].Enqueue(pCharacterObj);

        pCharacterObj.gameObject.SetActive(false);
    }

    public static void ClearAll()
    {
        //Ȱ��ȭ �Ǿ��ִ� ������Ʈ ��� ��Ȱ��ȭ
        foreach (Character key in Instance.useList.Keys)
        {
            List<CharacterObj> units = Instance.useList[key].ToList();
            for (int i = 0; i < units.Count; i++)
                RemoveCharacterObj(units[i]);

            Instance.useList[key].Clear();
        }
    }
}
