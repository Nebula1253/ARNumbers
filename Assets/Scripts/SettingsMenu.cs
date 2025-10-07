using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Text LanguageText, VoiceoverText, FinishGameText, SettingsTitleText;

    // copied from https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    public static SettingsMenu Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateText()
    {
        LanguageText.text = LangManager.Current.LangText;
        VoiceoverText.text = LangManager.Current.VOText;
        FinishGameText.text = LangManager.Current.FinishGame;
        SettingsTitleText.text = LangManager.Current.SettingsTitle;
    }
}
