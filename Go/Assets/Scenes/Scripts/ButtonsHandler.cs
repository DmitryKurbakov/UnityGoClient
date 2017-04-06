using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsHandler : MonoBehaviour
{

    public static Button passButton;

    public void Start()
    {
        passButton = GameObject.Find("PassButton").GetComponent<Button>();
    }

    public void OnPassButton()
    {
        if (Game.isPause)
        {
            return;
        }
        Debug.Log("Pass Button Pressed");
        StartCoroutine(PassButtonClicked());
    }

    private IEnumerator PassButtonClicked()
    {
        // yield return new WaitForSeconds(1);
       
        Game.socket.Emit(Game.player.isBlack ? "BLACK_PASSED" : "WHITE_PASSED");

        yield return new WaitForSeconds(0);

    }

    public void onExitButton()
    {
        Game.socket.Close();
        SceneManager.LoadScene("menu");
    }

    public void OnCancelButton()
    {
        Game.socket.Close();
        SceneManager.LoadScene("menu");
    }

}

