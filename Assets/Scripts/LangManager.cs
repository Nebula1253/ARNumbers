using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LangManager : MonoBehaviour
{
    public class LanguageData
    {
        public string[] Success;
        public string Fail;
        public string ResultsTitle;
        public string[] ResultsSentence;
        public string[] ResultsComments;
        public string Restart;
        public string Quit;
        public string LangText;
        public string VOText;
        public string FinishGame;
        public string SettingsTitle;

        // VO lines
        public AudioClip TryAndCount;
        public AudioClip[] SuccessVO;
        public AudioClip FailVO;
    }

    private LanguageData ENG, GER, FRE;
    public static LanguageData Current { get; private set; }
    public TMP_Dropdown langDropdown;

    [Header("ENGLISH VOICE CLIPS")]
    public AudioClip engTryAndCount;
    public AudioClip[] engSuccess;
    public AudioClip engFail;

    [Header("GERMAN VOICE CLIPS")]
    public AudioClip gerTryAndCount;
    public AudioClip[] gerSuccess;
    public AudioClip gerFail;

    [Header("FRENCH VOICE CLIPS")]
    public AudioClip freTryAndCount;
    public AudioClip[] freSuccess;
    public AudioClip freFail;

    private void Awake()
    {
        ENG = new()
        {
            Success = new[] { "Well done!", "Excellent!", "Good job!", "Very good!" },
            Fail = "Try again...",
            ResultsTitle = "RESULTS",
            ResultsSentence = new[] { "You got", "out of 10 numbers right!" },
            ResultsComments = new[]
                {
                    "Try again, see if you can get a better score!",
                    "Good attempt! See if you can get more next time!",
                    "Well done! See if you can get all 10 next time!",
                    "Fantastic work! Amazing!"
                },
            Restart = "PLAY AGAIN",
            Quit = "QUIT",
            LangText = "Language",
            VOText = "Voiceover",
            FinishGame = "FINISH GAME",
            SettingsTitle = "SETTINGS",

            TryAndCount = engTryAndCount,
            SuccessVO = engSuccess,
            FailVO = engFail
        };

        GER = new()
        {
            Success = new[] { "Gut gemacht!", "Exzellent!", "Gute arbeit!", "Sehr gut!" },
            Fail = "Erneut versuchen...",
            ResultsTitle = "ERGEBNISSE",
            ResultsSentence = new[] { "Sie haben", "von 10 Zahlen richtig!" },
            ResultsComments = new[]
                {
                    "Versuchen Sie es erneut und sehen Sie, ob Sie ein besseres Ergebnis erzielen können!",
                    "Guter Versuch! Mal sehen, ob du beim nächsten Mal mehr erreichen kannst!",
                    "Gut gemacht! Mal sehen, ob du das nächste Mal alle 10 schaffst!",
                    "Fantastische Arbeit! Erstaunlich!"
                },
            Restart = "SPIELEN SIE NOCHMAL",
            Quit = "AUFHÖREN",
            LangText = "Sprache",
            VOText = "Voiceover",
            FinishGame = "SPIEL BEENDEN",
            SettingsTitle = "EINSTELLUNGEN",

            TryAndCount = gerTryAndCount,
            SuccessVO = gerSuccess,
            FailVO = gerFail
        };

        FRE = new()
        {
            Success = new[] { "Bien joué!", "Excellent!", "Bon travail!", "Très bien!" },
            Fail = "Essayer à nouveau...",
            ResultsTitle = "RÉSULTATS",
            ResultsSentence = new[] { "Vous avez eu", "numéros sur 10 correctement!" },
            ResultsComments = new[]
                {
                "Réessayez, voyez si vous pouvez obtenir un meilleur score!",
                "Bon essai! Essayez d'obtenir plus la prochaine fois!",
                "Bravo! Essayez d'obtenir les 10 la prochaine fois!",
                "Excellent travail! Incroyable!"
            },
            Restart = "JOUER À NOUVEAU",
            Quit = "QUITTER",
            LangText = "Langue",
            VOText = "Voix off",
            FinishGame = "TERMINER LE JEU",
            SettingsTitle = "PARAMÈTRES",

            TryAndCount = freTryAndCount,
            SuccessVO = freSuccess,
            FailVO = freFail
        };

        Current = ENG;
    }

    // Start is called before the first frame update
    void Start()
    {
        langDropdown.onValueChanged.AddListener(ChangeLanguage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeLanguage(int index)
    {
        switch (index)
        {
            case 0:
                Current = ENG;
                break;
            case 1:
                Current = GER;
                break;
            case 2:
                Current = FRE;
                break;
            default:
                Current = ENG;
                break;
        }
        SettingsMenu.Instance.UpdateText();
    }
}
