using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private GameObject background;

    public void SetBackgroundActive(bool value)
    {
        background.SetActive(value);
    }
}
