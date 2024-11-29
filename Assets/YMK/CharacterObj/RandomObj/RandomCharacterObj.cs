using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class RandomCharacterObj : CharacterObj
{
    [FoldoutGroup("Part Data"), SerializeField] private List<FaceData> faceDatas = new List<FaceData>();
    [FoldoutGroup("Part Data"), SerializeField] private List<HeadData> headDatas = new List<HeadData>();
    [FoldoutGroup("Part Data"), SerializeField] private List<HairData> hairDatas = new List<HairData>();
    [FoldoutGroup("Part Data"), SerializeField] private List<BodyData> bodyDatas = new List<BodyData>();

    [SerializeField] private Image face;
    [SerializeField] private Image head;
    [SerializeField] private Image body;
    [SerializeField] private Image hairFront;
    [SerializeField] private Image hairBack;

    public override void Init(float pSpawnTime)
    {
        base.Init(pSpawnTime);
        SetRandom();
    }

    private void SetRandom()
    {
        int rIdx = UnityEngine.Random.Range(0, faceDatas.Count);
        FaceData faceData = faceDatas[rIdx];
        SetSprite(face, faceData.face);

        rIdx = UnityEngine.Random.Range(0, headDatas.Count);
        HeadData headData = headDatas[rIdx];
        SetSprite(head, headData.head);

        rIdx = UnityEngine.Random.Range(0, hairDatas.Count);
        HairData hairData = hairDatas[rIdx];
        SetSprite(hairFront, hairData.hairFront);
        SetSprite(hairBack, hairData.hairBack);

        rIdx = UnityEngine.Random.Range(0, bodyDatas.Count);
        BodyData bodyData = bodyDatas[rIdx];
        SetSprite(body, bodyData.body);
    }

    private void SetSprite(Image pImg, Sprite pSprite)
    {
        if (pSprite == null)
        {
            pImg.enabled = false;
            return;
        }

        pImg.enabled = true;
        pImg.sprite = pSprite;
    }

    public Sprite GetHairFront() => hairFront.sprite;
    public Sprite GetHairBack() => hairBack.sprite;
    public Sprite GetBody() => body.sprite;
    public Sprite GetHead() => head.sprite;
    public Sprite GetFace() => face.sprite;

}



