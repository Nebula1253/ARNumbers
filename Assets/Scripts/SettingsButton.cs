using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsMenu;
    private bool menuEnabled = false;

    // copied from https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    public static SettingsButton Instance { get; private set; }

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
        gameObject.GetComponent<Button>().onClick.AddListener(SettingsDisplay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SettingsDisplay()
    {
        if (menuEnabled)
        {
            menuEnabled = false;
        }
        else
        {
            menuEnabled = true;
        }
        settingsMenu.SetActive(menuEnabled);
    }

    public void DisableButton()
    {
        gameObject.SetActive(false);
    }
}
