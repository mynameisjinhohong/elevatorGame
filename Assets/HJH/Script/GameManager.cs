using UnityEngine;
using Sirenix.OdinInspector;


public class GameManager : SerializedMonoBehaviour
{
    public static GameManager instance;
    public UIManager UIManager;
    public StageData[] stageDatas;
    public int stage = 0;
    public float time = 0;
    public float maxTime = 300;
    public StageData nowStage;
    GameState gs;
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

                    break;
                case GameState.OutCharacter: break;
                case GameState.ShowCharacter: break;
                case GameState.Conversation: break;
                case GameState.MoveFloor: break;
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
        time = 0;
        nowStage = stageDatas[stage];
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
        for(int i = 0; i< nowStage.floorData.Length; i++)
        {

        }
    }

    public void StageOver()
    {
        Time.timeScale = 0.0f;
        stage += 1;
    }
}
