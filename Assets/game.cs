using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class game : MonoBehaviour
{
    public GameObject scoreTxt;
    public int score = 0;

    public void AddScore()
    {
        score++;
        scoreTxt.GetComponent<TMP_Text>().text = "Score: " + score.ToString();
    }
}
