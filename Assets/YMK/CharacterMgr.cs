using UnityEngine;

public class CharacterMgr : MonoBehaviour
{
    private static CharacterMgr Instance;

    public static CharacterMgr instance
    {
        get
        {
            if(Instance == null)
            {
                GameObject gameObject = new GameObject();
                DontDestroyOnLoad(gameObject);

                gameObject.AddComponent<CharacterMgr>();
                Instance = gameObject.GetComponent<CharacterMgr>();
                gameObject.name = instance.GetType().Name;
                Instance.Init();
            }
            return instance;
        }
    }

    private void Init()
    {
        
    }
}
