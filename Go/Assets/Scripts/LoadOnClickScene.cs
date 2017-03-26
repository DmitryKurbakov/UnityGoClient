using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClickScene : MonoBehaviour {

	// Use this for initialization
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
