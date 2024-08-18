using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager instance
    {
        get;
        private set;
    }

    [SerializeField]
    private RawImage videoScreeRawImg;
    [SerializeField]
    private VideoPlayer videoPlayer;

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

    private IEnumerator Start()
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        videoScreeRawImg.texture = videoPlayer.texture;
    }

    public void PlayVideo()
    {
        videoScreeRawImg.gameObject.SetActive(true);
        videoPlayer.Play();
    }
}
