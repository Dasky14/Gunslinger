using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private Sound[] m_sSounds = null;
    private AudioSource m_gcAudioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        if (m_sSounds == null || m_sSounds.Length == 0)
            gameObject.SetActive(false);
    }

    private void Start()
    {
        m_gcAudioSource = GetComponent<AudioSource>();
    }

    public static void PlayClip(string name, float volume)
    {
        foreach (Sound sound in instance.m_sSounds)
        {
            if (sound.m_sName == name)
            {
                instance.m_gcAudioSource.PlayOneShot(sound.m_sClip, Mathf.Clamp01(volume));
            }
        }
    }

    public static void PlayClip(string name)
    {
        PlayClip(name, 1f);
    }
}
