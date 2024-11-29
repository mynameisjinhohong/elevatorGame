using UnityEngine;

public class YmkTest : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Transform createTrans;
    public void Start()
    {
        CharacterObj characterObj = CharacterMgr.CreateCharacterObj(characterData);
        characterObj.transform.parent = createTrans;
        characterObj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        characterObj.RunCharacterAction(CharacterAction.Show, ()=>
        {
            RunTalkEvent(characterObj, 1);
        });
    }

    private void RunTalkEvent(CharacterObj characterObj ,int idx)
    {
        characterObj.RunTalkAction(idx, (res) =>
        {
            if (res)
            {
                RunTalkEvent(characterObj, idx + 1);
            }
            else
            {
                characterObj.RunCharacterAction(CharacterAction.Hide, null);
            }
        }
);
    }
}
