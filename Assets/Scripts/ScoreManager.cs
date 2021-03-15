using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int blueScore;
    public static int yellowScore;
    private GameObject blueText;
    private GameObject yellowText;

    void Start()
    {
        blueText = GameObject.Find("Blue Text");
        yellowText = GameObject.Find("Yellow Text");
    }

    void Update()
    {
        blueText.GetComponent<TextMeshProUGUI>().text = "Blue Team: " + blueScore;
        yellowText.GetComponent<TextMeshProUGUI>().text = "Yellow Team: " + yellowScore;
    }
}
