using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text text_message;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        text_message = GetComponent<TMP_Text>();
        text_message.text = "0";
    }
    public void IncreaseScore(int amountToInc)
    {
        score = score + amountToInc;
        Debug.Log("Updated Score" + score);

        text_message.text = score.ToString();

        changecolortext(Color.yellow);

    }
    //done by me
    void changecolortext(Color clr)
    {
        if (score >= 200)
        {
            text_message.color = clr;

        }
    }

    void changetextsize()
    {
        text_message.fontSize = 200;
        Invoke("changetextback", 0.51f);

    }

    void changetextback()
    {
        text_message.fontSize = 98.8f;
    }

    //till here    
}
