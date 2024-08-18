using System.Collections;
using System.Text;
using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;
    private StringBuilder stringBuilder = new StringBuilder();

    public IEnumerator SetText(string text, float waitTimes, int textSize, float lifeTime)
    {
        if (textSize != 0)
        {
            textMeshPro.enableAutoSizing = false;
            textMeshPro.fontSize = textSize;
        }
        else
        {
            textMeshPro.enableAutoSizing = true;
        }

        for (int i = 0; i < text.Length; i++)
        {
            stringBuilder.Append(text[i]);

            textMeshPro.text = stringBuilder.ToString();

            yield return new WaitForSeconds(waitTimes);
        }

        yield return new WaitForSeconds(lifeTime);

        stringBuilder.Clear();

        textMeshPro.text = string.Empty;
    }
}
