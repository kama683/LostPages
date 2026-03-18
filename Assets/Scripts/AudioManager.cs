using UnityEngine;

// ============================================================
//  LOST PAGES — AudioManager.cs
//  Прикрепи на пустой объект "AudioManager".
//  Заполни массив sounds в инспекторе.
// ============================================================
public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundEntry
    {
        public string name;
        public AudioClip clip;
        [Range(0, 1)] public float volume = 0.6f;
    }

    public SoundEntry[] sounds;
    static AudioManager instance;
    AudioSource src;

    void Awake()
    {
        instance = this;
        src = GetComponent<AudioSource>();
    }

    public static void Play(string name)
    {
        if (!instance) return;

        foreach (var s in instance.sounds)
        {
            if (s.name == name && s.clip != null)
            {
                instance.src.PlayOneShot(s.clip, s.volume);
                return;
            }
        }
    }
}