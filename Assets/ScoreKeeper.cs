using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{

    public static int score = 0;

    private Text myText;
    

    private void Start()
    {
        myText = GetComponent<Text>(); //will return our text component
        Reset();
    }
    
	public void Score(int points)
    {
        Debug.Log("Scored points");
        score += points;
        myText.text = score.ToString();

    }

    public static void Reset() //static method can be called without having a mytext variable created.
    {
        score = 0;
    }
}
