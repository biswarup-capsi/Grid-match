using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource Bg;
    [SerializeField]
    private AudioSource Sfx;

    [Header("Audio Clips")]
    public AudioClip BgClip;
    public AudioClip DropClip;

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Bg.clip = BgClip;
        Bg.Play();
    }

    private void Update()
    {
        Bg.volume = PlayerPrefs.GetFloat("MusicVolume", 0.4f);
        Bg.loop = true;
        Sfx.volume = PlayerPrefs.GetFloat("SfxVolume", 1.5f);
    }

    public void PlayDropSound()
    {
        Sfx.PlayOneShot(DropClip);
    }
}
