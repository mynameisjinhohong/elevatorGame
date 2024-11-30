using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PeopleIcon : MonoBehaviour
{
    public CharacterObj characterObj;

    public TMP_Text chatText;
    public Image patient;
    public Image body;
    public Image haed;
    public Image hair;
    public Image face;
    public Image backHair;
    public Image special;
    bool first = false;
    public int lastChatIdx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            patient.fillAmount = 1 - characterObj.GetPatienceTime() / characterObj.characterData.maxPatience;
        }

    }
}
