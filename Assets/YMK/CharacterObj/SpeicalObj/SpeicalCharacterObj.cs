
using UnityEngine;
using UnityEngine.UI;

public class SpeicalCharacterObj : CharacterObj
{
    [SerializeField] private Image character;
    [SerializeField] private Sprite normalFace;
    [SerializeField] private Sprite angryFace;

    public Sprite GetImg() => normalFace;

    public Sprite GetAngrySprite() => angryFace;

    protected override void SetAngry(bool state)
    {
        //state : ȭ�� ����
        if (state)
            character.sprite = angryFace;
        else
            character.sprite = normalFace;
    }
}
