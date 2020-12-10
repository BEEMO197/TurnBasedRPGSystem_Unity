using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxAnimator : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI text = null;

    [SerializeField]
    [Range(10, 100)]
    float textSpeedCharactersPerSecond = 10;

    // This reference is valid while running the coroutine and null when not
    IEnumerator animateTextRoutine = null;

    public void AnimateTextCharacterTurn(string whoseTurn)
    {
        AnimateText("It is " + whoseTurn+ "'s turn.");
    }
    public void AnimateText(string message)
    {
        //if coroutine is running
        if(animateTextRoutine != null)
        {
            StopCoroutine(animateTextRoutine);
        }

        // set the reference to the running coroutine
        animateTextRoutine = AnimateTextRoutine(message);
        //start it!
        StartCoroutine(animateTextRoutine);
    }

    IEnumerator AnimateTextRoutine(string message)
    {

        string currentMessage = "";

        for(int currentChar = 0; currentChar < message.Length; currentChar++)
        {
            currentMessage += message[currentChar];
            text.text = currentMessage;
            yield return new WaitForSeconds(1/textSpeedCharactersPerSecond);
        }

        //when finished, set reference to null
        animateTextRoutine = null;
    }
}
