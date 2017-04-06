using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinController : MonoBehaviour {

	
	void Start ()
	{
	    GameObject.Find("WinStatusText").GetComponent<Text>().text = Variables.winStatus;
	    GameObject.Find("WhiteText").GetComponent<Text>().text = "White: " + Variables.whiteScores;
	    GameObject.Find("BlackText").GetComponent<Text>().text = "Black: " + Variables.blackScores;
	}

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("menu");
    }

}
