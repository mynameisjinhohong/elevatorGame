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
    private FaceData faceData;
    private HeadData headData;
    private HairData hairData;
    private BodyData bodyData;

    public override void Init(float pSpawnTime, CharacterData pCharacterData)
    {
        base.Init(pSpawnTime, pCharacterData);
        SetRandom();
    }

    private void SetRandom()
    {
        int rIdx = UnityEngine.Random.Range(0, faceDatas.Count);
        faceData = faceDatas[rIdx];
        SetSprite(face, faceData.face);

        rIdx = UnityEngine.Random.Range(0, headDatas.Count);
        headData = headDatas[rIdx];
        SetSprite(head, headData.head);

        rIdx = UnityEngine.Random.Range(0, hairDatas.Count);
        hairData = hairDatas[rIdx];
        SetSprite(hairFront, hairData.hairFront);
        SetSprite(hairBack, hairData.hairBack);

        rIdx = UnityEngine.Random.Range(0, bodyDatas.Count);
        bodyData = bodyDatas[rIdx];
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

    protected override void SetAngry(bool state)
    {
        //state : ȭ�� ����
        if(state) 
            SetSprite(face, faceData.angryFace);
        else
            SetSprite(face, faceData.face);
    }

    public Sprite GetHairFront() => hairData.hairFront;
    public Sprite GetHairBack() => hairData.hairBack;
    public Sprite GetBody() => bodyData.body;
    public Sprite GetHead() => headData.head;
    public Sprite GetFace() => faceData.face;
    public Sprite GetAngryFace() => faceData.angryFace;

}



