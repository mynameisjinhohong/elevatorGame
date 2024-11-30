using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.TextCore.Text;

public class GameManager : SerializedMonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;
    #region 스테이지 관련
    public StageData[] stageDatas;
    public int stage = 0;
    public List<FloorDataCopy> nowStage;
    #endregion
    #region 시간관련
    public float time = 0;
    public float maxTime = 300;
    #endregion
    public int tip = 0;
    public int hp = 100;
    public List<CharacterObj> nowElevatorCharacter= new List<CharacterObj>();
    public Transform characterParent;

    public float moveTime;

    public ElevatorController elevator;

    public int damage;
    GameState gs;
    public int floor = 0;
    public GameState gameState
    {
        get
        {
            return gs;
        }
        set
        {
            gs = value;
            switch (gs)
            {
                case GameState.OpenElevator:
                    StartOpenElevator();
                    break;
                case GameState.OutCharacter:
                    StartOutCharacter();
                    break;
                case GameState.ShowCharacter:
                    StartInCharacter();
                    break;
                case GameState.CloseElevator:
                    StartCloseElevator();
                    break;
                case GameState.MoveFloor:
                    StartMoveFloor();
                    break;
            }
        }
    } 
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        StageStart();
    }

    public void StageStart()
    {
        Time.timeScale = 1.0f;
        hp = 100;
        floor = 0;
        uiManager.LampOn(floor);
        uiManager.TurnOffElevatorButton();
        nowStage= new List<FloorDataCopy>();
        for(int i = 0; i < stageDatas[stage].floorData.Length; i++)
        {
            nowStage.Add(new FloorDataCopy());
            nowStage[i].characterList = new Queue<CharacterData>();
            for (int j = 0; j < stageDatas[stage].floorData[i].characterList.Length; j++)
            {
                nowStage[i].characterList.Enqueue(stageDatas[stage].floorData[i].characterList[j]);
            }
        }
        
        time = 0;
        gameState = GameState.OpenElevator;
    }

    public void StartOpenElevator()
    {
        uiManager.TurnOffElevatorButton();
        uiManager.OffFloorArrowButton(floor);
        elevator.OpenElevator();

    }

    public void EndOpenElevator()
    {
        gameState = GameState.OutCharacter;
    }

    public void OutCharacter(CharacterObj character)
    {
        character.RunCharacterAction(CharacterAction.Show, () => ChatBye(character));
    }

    public void ChatBye(CharacterObj character)
    {
        if (character.GetPatienceTime() > 0)
        {
            character.RunThankTalkAction(() =>character.RunCharacterAction(CharacterAction.Hide,()=> CharacterRemove(character)));
        }
        else
        {
            character.RunAngryTalkAction(() => character.RunCharacterAction(CharacterAction.Hide, () => CharacterRemove(character)));
        }
        
    }
    public void CharacterRemove(CharacterObj obj)
    {
        uiManager.RemovePeopleIcon(obj);
        StartOutCharacter();
    }

    public void StartOutCharacter()
    {
        if (nowElevatorCharacter.Count > 0)
        {
            for(int i =0; i< nowElevatorCharacter.Count; i++)
            {
                if (nowElevatorCharacter[nowElevatorCharacter.Count - 1].characterData.targetFloor == floor)
                {
                    CharacterObj obj = nowElevatorCharacter[i];
                    nowElevatorCharacter.RemoveAt(i);
                    OutCharacter(obj);
                    break;
                }
                if(i == nowElevatorCharacter.Count - 1)
                {
                    gameState = GameState.ShowCharacter;
                }
            }
        }
        else
        {
            gameState = GameState.ShowCharacter;
        }
        
    }
    private void RunTalkEvent(CharacterObj characterObj, int idx)
    {
        characterObj.RunTalkAction(idx, (res) =>
        {
            if (res)
            {
                RunTalkEvent(characterObj, idx + 1);
            }
            else
            {
                nowElevatorCharacter.Add(characterObj);
                uiManager.CreateNewPeopleIcon(characterObj,idx);
                characterObj.RunCharacterAction(CharacterAction.Hide, () => StartInCharacter());
            }
        }
);
    }


    public void StartInCharacter()
    {
        if (nowStage.Count > floor)
        {
            if (nowStage[floor].characterList.Count > 0)
            {
                if (nowStage[floor].characterList.Peek().spawnTime <= time)
                {
                    CharacterObj obj = CharacterMgr.CreateCharacterObj(nowStage[floor].characterList.Dequeue());
                    obj.transform.parent = characterParent;
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(100, obj.GetComponent<RectTransform>().anchoredPosition.y, 0);
                    obj.RunCharacterAction(CharacterAction.Show, () =>
                    {
                        RunTalkEvent(obj, 1);
                    });
                }
                else
                {
                    gameState = GameState.CloseElevator;
                }
            }
            else
            {
                gameState = GameState.CloseElevator;
            }
        }
        else
        {
            gameState = GameState.CloseElevator;
        }

    }

    public void StartCloseElevator()
    {
        elevator.CloseElevator();
    }

    public void EndCloseElevator()
    {
        uiManager.OffFloorArrowButton(floor);
        gameState = GameState.MoveFloor;
    }

    public void StartMoveFloor()
    {
        uiManager.TurnOnElevatorButton();
    }

    public void MoveFloor(int idx)
    {
        uiManager.TurnOffElevatorButton();
        StartCoroutine(MoveFloorCo(idx));
    }
    IEnumerator MoveFloorCo(int idx)
    {
        while(idx != floor)
        {
            if (idx > floor)
            {
                yield return new WaitForSeconds(moveTime);
                floor += 1;
                uiManager.LampOn(floor);
            }
            else
            {
                yield return new WaitForSeconds(moveTime);
                floor -= 1;
                uiManager.LampOn(floor);
            }
        }
        gameState = GameState.OpenElevator;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= maxTime)
        {
            StageOver();
        }
        for(int i =0; i<nowStage.Count; i++)
        {
            if (nowStage[i].characterList.Count > 0)
            {
                if (nowStage[i].characterList.Peek().spawnTime < time)
                {
                    uiManager.OnFloorArrowButton(i);
                }
            }
        }
        for(int i =0; i<nowElevatorCharacter.Count; i++)
        {
            if (nowElevatorCharacter[i].GetPatienceTime() <= 0 && !nowElevatorCharacter[i].angryCheck)
            {
                hp -= damage;
                nowElevatorCharacter[i].angryCheck = true;
                //need to put character Icon angry effect
            }
        }
    }

    public void StageOver()
    {
        Time.timeScale = 0.0f;
        stage += 1;
    }
}



[System.Serializable]
public class FloorDataCopy
{
    public Queue<CharacterData> characterList;
}

