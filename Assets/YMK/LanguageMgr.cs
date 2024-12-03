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
            //�̹� �ε尡 �Ϸ�Ǿ��ִ�.
            yield break;
        }

        //,�� �������� ����� csv������ �д´�.
        TextAsset textAsset = Resources.Load<TextAsset>(filePath);
        if (textAsset == null)
            yield break;

        //���� ������.
        string[] rows = textAsset.text.Split('\n');
        List<string> rowList = new List<string>();
        for (int i = 0; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]))
            {
                //�ƹ��͵� ���� ��ü
                continue;
            }
            string row = rows[i].Replace("\r", string.Empty);
            row = row.Trim();
            rowList.Add(rows[i]);
        }

        //������
        string[] subjects = rowList[0].Split("\t");

        for (int r = 1; r < rowList.Count; r++)
        {
            //�ش� �ٺ��� �����ʹ�.
            string[] values = rowList[r].Split("\t");

            //Ű���� ��´�.
            string keyValue = values[0].Replace('\r', ' ').Trim();

            for (int c = 1; c < values.Length; c++)
            {
                //�ش�ĭ�� �� �����´�.
                subjects[c] = subjects[c].Replace('\r', ' ').Trim();
                Language language = (Language)Enum.Parse(typeof(Language), subjects[c]);
                if (languageData.ContainsKey(language) == false)
                    languageData.Add(language, new Dictionary<string, string>());

                //������ȯ�Ѵ�.
                values[c] = values[c].Replace('\r', ' ').Trim();

                //�����͸� �߰��Ѵ�.
                languageData[language].Add(keyValue, values[c]);
            }
        }

        isLoad = true;
    }

    public static string GetText(string pKey)
    {
        //Key�� �ش��ϴ� ��� �ؽ�Ʈ�� �ҷ��´�.

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
        //textUI�� �ش��ϴ� TextMeshUI�� ���ڸ� �����ش�.
        //pKey�� �ش��ϴ� ���ڸ� �־���
        string str = GetText(pKey);
        SetText(textUI, str, pLanguagee);
    }

    public static void SetText(TextMeshProUGUI textUI, string pStr)
    {
        SetText(textUI, pStr, Instance.nowLanguage);
    }

    public static void SetText(TextMeshProUGUI textUI, string pStr, Language pLanguagee)
    {
        //textUI�� �ش��ϴ� TextMeshUI�� ���ڸ� �����ش�.
        //pStr�� �״�� �־���
        textUI.text = pStr;

        //�� �´� ��Ʈ�� �������ش�.
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
