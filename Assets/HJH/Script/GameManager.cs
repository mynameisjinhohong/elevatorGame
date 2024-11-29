using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Collections;

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
    public int hp = 100;
    public Queue<CharacterData> nowFloorCharacter = new Queue<CharacterData>(); //현재 층에서 타야할 사람들을 넣어놓은 큐
    public List<CharacterObj> nowElevatorCharacter= new List<CharacterObj>();
    public Transform characterParent;
    
    public ElevatorController elevator;


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
        nowFloorCharacter = new Queue<CharacterData>();
        hp = 100;
        floor = 0;
        nowStage= new List<FloorDataCopy>();
        for(int i = 0; i < stageDatas[stage].floorData.Length; i++)
        {
            nowStage.Add(new FloorDataCopy());
            for(int j = 0; j < stageDatas[stage].floorData[i].characterList.Length; j++)
            {
                nowStage[i].characterList = new Queue<CharacterData>();
                nowStage[i].characterList.Enqueue(stageDatas[stage].floorData[i].characterList[j]);
            }
        }
        time = 0;
        gameState = GameState.OpenElevator;
    }

    public void StartOpenElevator()
    {
        while (nowStage[floor].characterList.Count > 0)
        {
            if (nowStage[floor].characterList.Peek().spawnTime > time)
            {
                nowFloorCharacter.Enqueue(nowStage[floor].characterList.Dequeue());
            }
        }
        gameState = GameState.OutCharacter;
        uiManager.TurnOffElevatorButton();
        elevator.OpenElevator();

    }

    public void EndOpenElevator()
    {
        gameState = GameState.OutCharacter;
    }

    public void StartOutCharacter()
    {
        if(nowElevatorCharacter.Count > 0)
        {
            CharacterMgr.RemoveCharacterObj(nowElevatorCharacter[nowElevatorCharacter.Count - 1]);
            nowElevatorCharacter.RemoveAt(nowElevatorCharacter.Count-1);
        }
        else
        {
            gameState = GameState.ShowCharacter;
        }
        
    }


    public void StartInCharacter()
    {
        if(nowFloorCharacter.Count > 0)
        {
            CharacterObj obj = CharacterMgr.CreateCharacterObj(nowFloorCharacter.Dequeue());
            obj.transform.parent = characterParent;
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
        gameState = GameState.MoveFloor;
    }

    public void StartMoveFloor()
    {
        uiManager.TurnOnElevatorButton();
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
                if (nowStage[i].characterList.Peek().spawnTime > time)
                {
                    uiManager.OnFloorButton(i);
                }
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

