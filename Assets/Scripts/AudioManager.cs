using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Toggle voToggle;
    private AudioSource audioSource;
    private bool voEnabled = true;
    public AudioClip applause, finalApplause, fireworks;

    // copied from https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    public static AudioManager Instance { get; private set; }
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
        voToggle.onValueChanged.AddListener(ToggleValueChanged);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ToggleValueChanged(bool value)
    {
        voEnabled = value;
    }

    public void Success(int which)
    {
        if (voEnabled)
        {
            audioSource.Stop();
            audioSource.clip = LangManager.Current.SuccessVO[which];
            audioSource.Play();
        }
        audioSource.PlayOneShot(applause);
    }

    public void Fail()
    {
        if (voEnabled)
        {
            audioSource.Stop();
            audioSource.clip = LangManager.Current.FailVO;
            audioSource.Play();
        }
    }

    public void TryAndCount()
    {
        if (voEnabled)
        {
            audioSource.Stop();
            audioSource.clip = LangManager.Current.TryAndCount;
            audioSource.Play();
        }
    }


    public void Results(bool playApplause = false)
    {
        audioSource.Stop();
        if (playApplause)
        {
            audioSource.clip = finalApplause;
            audioSource.Play();
            audioSource.PlayOneShot(fireworks);
        }
    }
}
