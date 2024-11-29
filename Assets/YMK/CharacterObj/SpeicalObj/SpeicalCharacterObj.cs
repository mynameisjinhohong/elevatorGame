using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class SpeicalCharacterObj : CharacterObj
{
    [SerializeField] private Image character;

    public Sprite GetImg() => character.sprite;
}
