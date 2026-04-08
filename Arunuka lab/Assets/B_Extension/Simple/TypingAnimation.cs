using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class TypingAnimation
{
    float timeWrite = 0.05f;
    public TypingAnimation(float timeWrite)
    {
        this.timeWrite = timeWrite;
    }

    public IEnumerator DoAnimateText(TMP_Text textComponent, string text)
    {
        bool completed = false;
        int indexChar = 0;
        string bufferText = text;
        // TODO Improve This
        bufferText = bufferText.Replace("<b>", "");
        bufferText = bufferText.Replace("</b>", "");
        StringBuilder builder = new StringBuilder();
        while (!completed)
        {
            float fullTime = (char.IsWhiteSpace(bufferText[indexChar])) ? timeWrite * 0.5f : timeWrite;
            builder.Append(bufferText[indexChar]);
            textComponent.text = builder.ToString();
            indexChar++;
            if (indexChar >= bufferText.Length)
                completed = true;
            yield return new WaitForSecondsRealtime(fullTime);
        }
        textComponent.text = text;
    }
}
