using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PeopleIcon : MonoBehaviour
{
    public CharacterObj characterObj;
    public UIManager uIManager;
    public TMP_Text chatText;
    public GameObject peopleIcon;
    public Image patient;
    public Image body;
    public Image haed;
    public Image hair;
    public Image face;
    public Image backHair;
    public Image special;
    public Button chatButton;
    bool first = false;
    bool angry = false;
    bool veryAngry = false;
    public int lastChatIdx;

    public RectTransform characterRecttransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterRecttransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterObj != null)
        {
            if (!first)
            {
                first = true;
                switch (lastChatIdx)
                {
                    case 1:
                        chatText.text = characterObj.characterData.talkText1;
                        break;
                    case 2:
                        chatText.text = characterObj.characterData.talkText2;
                        break;
                    case 3:
                        chatText.text = characterObj.characterData.talkText3;
                        break;
                }
                chatButton.onClick.AddListener(() => uIManager.ChatCanvasOn(chatText.text));
                if (characterObj.characterData.character == Character.Random)
                {
                    RandomCharacterObj random = characterObj.gameObject.GetComponent<RandomCharacterObj>();
                    if (random.GetHairBack() != null)
                    {
                        backHair.sprite = random.GetHairBack();
                    }
                    body.sprite = random.GetBody();
                    haed.sprite = random.GetHead();
                    hair.sprite = random.GetHairFront();
                    face.sprite = random.GetFace();
                    special.gameObject.SetActive(false);
                    body.gameObject.SetActive(true);
                    haed.gameObject.SetActive(true);
                    hair.gameObject.SetActive(true);
                    face.gameObject.SetActive(true);
                    backHair.gameObject.SetActive(true);
                }
                else
                {
                    backHair.gameObject.SetActive(false);
                    body.gameObject.SetActive(false);
                    haed.gameObject.SetActive(false);
                    hair.gameObject.SetActive(false);
                    face.gameObject.SetActive(false);
                    special.gameObject.SetActive(true);
                    SpeicalCharacterObj speical = characterObj.gameObject.GetComponent<SpeicalCharacterObj>();
                    special.sprite = speical.GetImg();
                }
            }

            if (characterObj.completeCheck)
                return;

            patient.fillAmount = 1 - characterObj.GetPatienceTime() / characterObj.characterData.maxPatience;


            if(patient.fillAmount > 0.5f  && !angry)
            {
                angry = true;
                if (characterObj.characterData.character == Character.Random)
                {
                    RandomCharacterObj random = characterObj.gameObject.GetComponent<RandomCharacterObj>();
                    face.sprite = random.GetAngryFace();
                    AngryAni();
                }
                else
                {
                    SpeicalCharacterObj speicalObj = characterObj.gameObject.GetComponent<SpeicalCharacterObj>();
                    special.sprite = speicalObj.GetAngrySprite();
                    AngryAni();
                }
            }
            if(!veryAngry && patient.fillAmount >= 1)
            {
                veryAngry = true;
                VeryAngryAni();
            }
        }
    }


    public void AngryAni()
    {
        GameManager.instance.audioManager.StartAudio(SFX.Angry);
        if (characterRecttransform != null)
        {
            
            characterRecttransform
                .DOShakeAnchorPos(0.5f, 20, 10, 90, false, true) 
                .SetEase(Ease.Linear); 
        }
    }

    public void VeryAngryAni()
    {
        GameManager.instance.audioManager.StartAudio(SFX.VeryAngry);
        //ani to peopleIcon, when end need to call VeryAngryAniEnd
        if (characterRecttransform != null)
        {
            
            characterRecttransform
                .DOShakeAnchorPos(1f, 40, 10, 90, false, true) 
                .SetEase(Ease.Linear).OnComplete(()=>VeryAngryAniEnd());
        }
    }

    public void VeryAngryAniEnd()
    {
        GameManager.instance.hp -= GameManager.instance.damage;
        
    }
}
