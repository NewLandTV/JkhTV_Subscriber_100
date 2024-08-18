using UnityEngine;

public enum SoundType
{
    BGM,
    SFX
}

[System.Serializable]
public struct Sound
{
    [SerializeField]
    private string name;
    public string Name => name;

    [SerializeField]
    private AudioClip clip;
    public AudioClip Clip => clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance
    {
        get;
        private set;
    }

    [SerializeField]
    private AudioSource bgmPlayer;
    [SerializeField]
    private AudioSource[] sfxPlayer;

    [SerializeField]
    private Sound[] bgm;
    [SerializeField]
    private Sound[] sfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(string name)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgm[i].Name.Equals(name))
            {
                bgmPlayer.clip = bgm[i].Clip;

                bgmPlayer.Play();

                return;
            }
        }
    }

    public void PlaySFX(string name)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfx[i].Name.Equals(name))
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].Clip;

                        sfxPlayer[j].Play();

                        return;
                    }
                }
            }
        }
    }
}
