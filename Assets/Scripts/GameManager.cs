using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool[] complete = new bool[10];
    public GameObject numbersRoot;
    public GameObject resultsScreen;
    private GameObject[] numbers = new GameObject[10];
    private int currNumber = 1;
    public TMP_Text statusText;
    public ParticleSystem firework;

    // copied from https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    public static GameManager Instance { get; private set; }

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
        firework.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            numbers[i] = numbersRoot.transform.GetChild(i).gameObject;
        }
        FinishOrProgress();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CorrectPicked()
    {
        int successPrompt = Random.Range(0, LangManager.Current.Success.Length);

        complete[currNumber - 1] = true;

        StopAllCoroutines();
        StartCoroutine(StatusDisplay(LangManager.Current.Success[successPrompt], true));

        AudioManager.Instance.Success(successPrompt);

        FinishOrProgress();
    }

    public void WrongPicked()
    {
        AudioManager.Instance.Fail();

        StopAllCoroutines();
        StartCoroutine(StatusDisplay(LangManager.Current.Fail));
    }

    IEnumerator StatusDisplay(string textToDisplay, bool showFirework = false)
    {
        if (showFirework)
        {
            firework.Play();
        }
        statusText.text = textToDisplay;

        yield return new WaitForSeconds(0.9f);
        statusText.text = "";

        if (showFirework)
        {
            firework.Stop();
        }
    }

    private void FinishOrProgress()
    {
        bool allComplete = true;

        // check if all numbers have been identified
        for (int i = 0; i < 10; i++)
        {
            if (!complete[i])
            {
                allComplete = false;
                break;
            }
        }

        if (allComplete)
        {
            EndGame();
        }
        else
        {
            // pick a new number from the ones that haven't been selected, and enable corresponding game object
            int newNumber = Random.Range(1, 11);

            while (complete[newNumber - 1])
            {
                newNumber = Random.Range(1, 11);
            }

            numbers[currNumber - 1].SetActive(false);
            numbers[newNumber - 1].SetActive(true);
            currNumber = newNumber;

            // adjust on-screen options accordingly
            NumOptions.Instance.AssignButtons(newNumber);

            StartCoroutine(TryAndCount());
        }
    }

    IEnumerator TryAndCount()
    {
        yield return new WaitForSeconds(2.0f);
        AudioManager.Instance.TryAndCount();
    }

    public void EndGame(bool calledFromSettings = false)
    {
        StopAllCoroutines();

        firework.Stop();
        statusText.text = "";

        // called when all 10 numbers have been identified, also called when player hits 'end game' from settings
        NumOptions.Instance.DisableOptions();
        SettingsButton.Instance.DisableButton();
        if (calledFromSettings)
        {
           SettingsMenu.Instance.gameObject.SetActive(false); 
        }

        resultsScreen.SetActive(true);

        int howManyCorrect = 0;
        foreach (bool b in complete)
        {
            if (b) howManyCorrect++;
        }
        resultsScreen.GetComponent<Results>().ShowResult(howManyCorrect);
    }
}
