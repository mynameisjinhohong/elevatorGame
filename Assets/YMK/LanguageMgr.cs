using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageMgr : MonoBehaviour
{
    private static LanguageMgr Instance = null;
    [SerializeField] private TMP_FontAsset KoreaFont;
    [SerializeField] private TMP_FontAsset EnglishFont;

    private bool isLoad = false;

    [SerializeField] private string   filePath;
    [SerializeField] private Language nowLanguage;

    private Dictionary<Language, Dictionary<string, string>> languageData
        = new Dictionary<Language, Dictionary<string, string>>();

    private const string LANGUAGE_KEY = "LANGUAGE";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        Language loadLan = (Language)PlayerPrefs.GetInt(LANGUAGE_KEY, 0);
        if (loadLan == Language.NONE)
            loadLan = Language.Korea;
        SetLanguage(loadLan);
        StartCoroutine(runLoadData());
    }

    private IEnumerator runLoadData()
    {
        if (isLoad)
        {
            //이미 로드가 완료되어있다.
            yield break;
        }

        //,자 형식으로 저장된 csv파일을 읽는다.
        TextAsset textAsset = Resources.Load<TextAsset>(filePath);
        if (textAsset == null)
            yield break;

        //줄을 나눈다.
        string[] rows = textAsset.text.Split('\n');
        List<string> rowList = new List<string>();
        for (int i = 0; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]))
            {
                //아무것도 없는 객체
                continue;
            }
            string row = rows[i].Replace("\r", string.Empty);
            row = row.Trim();
            rowList.Add(rows[i]);
        }

        //제목줄
        string[] subjects = rowList[0].Split("\t");

        for (int r = 1; r < rowList.Count; r++)
        {
            //해당 줄부터 데이터다.
            string[] values = rowList[r].Split("\t");

            //키값을 얻는다.
            string keyValue = values[0].Replace('\r', ' ').Trim();

            for (int c = 1; c < values.Length; c++)
            {
                //해당칸의 언어를 가져온다.
                subjects[c] = subjects[c].Replace('\r', ' ').Trim();
                Language language = (Language)Enum.Parse(typeof(Language), subjects[c]);
                if (languageData.ContainsKey(language) == false)
                    languageData.Add(language, new Dictionary<string, string>());

                //값을반환한다.
                values[c] = values[c].Replace('\r', ' ').Trim();

                //언어데이터를 추가한다.
                languageData[language].Add(keyValue, values[c]);
            }
        }

        isLoad = true;
    }

    public static string GetText(string pKey)
    {
        //Key에 해당하는 언어 텍스트를 불러온다.

        LanguageMgr languageMgr         = Instance;
        var         languageData        = languageMgr.languageData;
        Language    nowLanguage         = languageMgr.nowLanguage;

        if (languageData.ContainsKey(nowLanguage))
        {
            if (languageData[nowLanguage].ContainsKey(pKey))
            {
                string str = languageData[nowLanguage][pKey];
                str = str.Replace("\\n", "\n");
                return str;
            }
        }
        return string.Empty;
    }

    public static void SetLanguage(Language pLanguage)
    {
        PlayerPrefs.SetInt(LANGUAGE_KEY, (int)pLanguage);
        Instance.nowLanguage = pLanguage;
    }

    public static Language GetLanguage()
    {
        return Instance.nowLanguage;
    }

    public static void SetString(TextMeshProUGUI textUI, string pKey)
    {
        SetString(textUI, pKey, Instance.nowLanguage);
    }

    public static void SetString(TextMeshProUGUI textUI,string pKey, Language pLanguagee)
    {
        //textUI에 해당하는 TextMeshUI의 글자를 정해준다.
        //pKey에 해당하는 문자를 넣어줌
        string str = GetText(pKey);
        SetText(textUI, str, pLanguagee);
    }

    public static void SetText(TextMeshProUGUI textUI, string pStr)
    {
        SetText(textUI, pStr, Instance.nowLanguage);
    }

    public static void SetText(TextMeshProUGUI textUI, string pStr, Language pLanguagee)
    {
        //textUI에 해당하는 TextMeshUI의 글자를 정해준다.
        //pStr를 그대로 넣어줌
        textUI.text = pStr;

        //언어에 맞는 폰트를 설정해준다.
        LanguageMgr languageMgr = Instance;
        TMP_FontAsset fontAsset = null;

        switch (pLanguagee)
        {
            case Language.Korea:
                fontAsset = languageMgr.KoreaFont;
                break;
            case Language.English:
                fontAsset = languageMgr.EnglishFont;
                break;
        }

        textUI.font = fontAsset;
    }
}
