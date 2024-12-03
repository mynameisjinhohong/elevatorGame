using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private string stringKey;
    private void Awake()
    {
        LanguageMgr.SetString(textUI, stringKey);
    }

    public void SetText(string pStrKey)
    {
        stringKey = pStrKey;
        LanguageMgr.SetString(textUI, stringKey);
    }
}
