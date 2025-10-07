using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumOptions : MonoBehaviour
{
    private Button[] buttons;

    // copied from https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    public static NumOptions Instance { get; private set; }

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

        buttons = GetComponentsInChildren<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AssignButtons(int correctNum)
    {
        // pick one button to be the correct one
        int correctButton = Random.Range(0, buttons.Length);

        // assign labels to all 4 buttons
        HashSet<int> assignedNumbers = new();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;

            if (i == correctButton)
            {
                buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = correctNum.ToString();
                buttons[i].onClick.AddListener(RightButton);
            }
            else
            {
                int buttonLabel = correctNum;
                while (buttonLabel == correctNum || assignedNumbers.Contains(buttonLabel))
                {
                    buttonLabel = Random.Range(1, 11);
                }
                assignedNumbers.Add(buttonLabel);
                buttons[i].gameObject.GetComponentInChildren<TMP_Text>().text = buttonLabel.ToString();

                int temp = i;
                buttons[i].onClick.AddListener(() => WrongButton(temp));
            }
        }
    }

    private void WrongButton(int i)
    {
        buttons[i].interactable = false;
        GameManager.Instance.WrongPicked();
    }

    private void RightButton()
    {
        foreach (Button btn in buttons)
        {
            btn.onClick.RemoveAllListeners();
        }

        GameManager.Instance.CorrectPicked();
    }

    public void DisableOptions()
    {
        gameObject.SetActive(false);
    }
}
