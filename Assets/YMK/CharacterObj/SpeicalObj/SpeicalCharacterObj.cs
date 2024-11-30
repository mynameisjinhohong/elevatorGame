using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

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
