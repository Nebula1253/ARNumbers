using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    public Button quitButton, restartButton;
    public TMP_Text tallyText, commentText, titleText, sentenceText1, sentenceText2;
    public GameObject fireworks;

    // Start is called before the first frame update
    void Start()
    {
        quitButton.onClick.AddListener(() => Application.Quit());
        restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowResult(int howManyCorrect)
    {
        titleText.text = LangManager.Current.ResultsTitle;
        tallyText.text = howManyCorrect.ToString();
        sentenceText1.text = LangManager.Current.ResultsSentence[0];
        sentenceText2.text = LangManager.Current.ResultsSentence[1];

        quitButton.gameObject.GetComponentInChildren<TMP_Text>().text = LangManager.Current.Quit;
        restartButton.gameObject.GetComponentInChildren<TMP_Text>().text = LangManager.Current.Restart;

        AudioManager.Instance.Results(howManyCorrect > 6);

        if (howManyCorrect <= 3)
        {
            commentText.text = LangManager.Current.ResultsComments[0];
        }
        else if (howManyCorrect <= 6)
        {
            commentText.text = LangManager.Current.ResultsComments[1];
        }
        else if (howManyCorrect <= 9)
        {
            fireworks.SetActive(true);
            commentText.text = LangManager.Current.ResultsComments[2];
        }
        else
        {
            fireworks.SetActive(true);
            commentText.text = LangManager.Current.ResultsComments[3];
        }
    }
    
}
