using UnityEngine;

[System.Serializable]
public class Sound
{

    public string m_sName = "nada";
    public AudioClip m_sClip;
    [Range(0f, 2f)]
    public float m_fVolumeMult = 1f;

    public Sound(AudioClip clip)
    {
        m_sClip = clip;
        m_fVolumeMult = 1f;
    }

    public Sound(AudioClip clip, float volumeMult)
    {
        m_sClip = clip;
        m_fVolumeMult = volumeMult;
    }
}
