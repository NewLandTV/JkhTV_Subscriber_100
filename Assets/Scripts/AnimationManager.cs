using System.Collections;
using UnityEngine;

public enum AnimationController
{
    None = -1,
    Camera,
    Fade,
    Fade_Black
}

public enum AnimationScript
{
    None = -1,
    Text
}

public enum CameraType
{
    DayTime,
    SunSet
}

[System.Serializable]
public class AnimationElement
{
    [Header("Animation Controller")]
    [SerializeField]
    private AnimationController animationController;
    public AnimationController AnimationController => animationController;

    [SerializeField]
    private string triggerName;
    public string TriggerName => triggerName;

    [SerializeField]
    private float animationActiveTime;
    public float AnimationActiveTime => animationActiveTime;

    [SerializeField]
    private float speed;
    public float Speed => speed;

    [Header("Animation Script")]
    [SerializeField]
    private AnimationScript animationScript;
    public AnimationScript AnimationScript => animationScript;

    [SerializeField]
    private string text;
    public string Text => text;

    [SerializeField]
    private int textSize;
    public int TextSize => textSize;

    [SerializeField]
    private float waitTime;
    public float WaitTime => waitTime;

    [SerializeField]
    private float lifeTime;
    public float LifeTime => lifeTime;

    [Header("Sound")]
    [SerializeField]
    private string soundName;
    public string SoundName => soundName;

    [SerializeField]
    private SoundType soundType;
    public SoundType SoundType => soundType;

    [Header("Camera")]
    [SerializeField]
    private CameraType cameraType;
    public CameraType CameraType => cameraType;

    [Header("Background")]
    [SerializeField]
    private bool backgrounActive;
    public bool BackgrounActive => backgrounActive;

    [Header("Video")]
    [SerializeField]
    private bool useVideo;
    public bool UseVideo => useVideo;
}

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private Animator[] animators;
    [SerializeField]
    private TextAnimation textAnimation;
    [SerializeField]
    private Background background;
    [SerializeField]
    private AnimationElement[] animationElements;
    [SerializeField]
    private GameObject sunSetCamera;
    private GameObject dayTimeCamera;

    private void Awake()
    {
        dayTimeCamera = Camera.main.gameObject;
    }

    private IEnumerator Start()
    {
        int currentAnimationIndex = 0;
        float timer = 0f;

        while (animationElements.Length > currentAnimationIndex)
        {
            if (timer >= animationElements[currentAnimationIndex].AnimationActiveTime)
            {
                if (animationElements[currentAnimationIndex].AnimationController != AnimationController.None)
                {
                    animators[(int)animationElements[currentAnimationIndex].AnimationController].speed = animationElements[currentAnimationIndex].Speed;

                    animators[(int)animationElements[currentAnimationIndex].AnimationController].SetTrigger(animationElements[currentAnimationIndex].TriggerName);
                }

                switch (animationElements[currentAnimationIndex].AnimationScript)
                {
                    case AnimationScript.Text:
                        StartCoroutine(textAnimation.SetText(animationElements[currentAnimationIndex].Text, animationElements[currentAnimationIndex].WaitTime, animationElements[currentAnimationIndex].TextSize, animationElements[currentAnimationIndex].LifeTime));

                        break;
                }

                if (!animationElements[currentAnimationIndex].SoundName.Equals(string.Empty))
                {
                    if (animationElements[currentAnimationIndex].SoundType == SoundType.BGM)
                    {
                        SoundManager.instance.PlayBGM(animationElements[currentAnimationIndex].SoundName);
                    }
                    if (animationElements[currentAnimationIndex].SoundType == SoundType.SFX)
                    {
                        SoundManager.instance.PlaySFX(animationElements[currentAnimationIndex].SoundName);
                    }
                }

                dayTimeCamera.SetActive(animationElements[currentAnimationIndex].CameraType == CameraType.DayTime);
                sunSetCamera.SetActive(animationElements[currentAnimationIndex].CameraType == CameraType.SunSet);

                background.SetBackgroundActive(animationElements[currentAnimationIndex].BackgrounActive);

                if (animationElements[currentAnimationIndex].UseVideo)
                {
                    VideoManager.instance.PlayVideo();
                }

                currentAnimationIndex++;
            }

            timer += Time.deltaTime;

            yield return null;
        }
    }
}
