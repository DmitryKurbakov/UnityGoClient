using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Essences;
using Assets.Scripts;
using SocketIO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public static SocketIOComponent socket;
    public static GameObject mainCanvas;
    public static GameObject pauseImage;
    public static GameObject waitImage;
    public static GameObject rockColorImage;
    public static GameObject timeText;
    public static GameObject turntext;
    public static GameObject moveText;

    public static Player player;

    public static bool isPause = false;
    public static int move;

    public void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        mainCanvas = GameObject.Find("MainCanvas");

        pauseImage = GameObject.Find("PauseImage");
        pauseImage.SetActive(false);

        waitImage = GameObject.Find("WaitImage");
        waitImage.SetActive(false);

        rockColorImage = GameObject.Find("RockColorImage");

        timeText = GameObject.Find("TimeText");

        turntext = GameObject.Find("TurnText");

        moveText = GameObject.Find("MoveText");

        move = 0;

        socket.Connect();

        socket.On("WAIT", Wait);
        socket.On("START_GAME", StartGame);
        socket.On("START_ATTRIBUTES", InitializePlayer);
        socket.On("FINISH_MOVE", PlayerFinishMove);
        socket.On("PAUSE", Pause);
        socket.On("PLAY", Play);
        socket.On("END_OF_GAME", EndOfGame);

        socket.On("error", TestError);
        socket.On("close", TestClose);

        //StartCoroutine(BeepBoop());
        //socket.autoConnect = false;
        //socket.On("close");

          
    }

    public void Wait(SocketIOEvent e)
    {
        mainCanvas.SetActive(false);
        waitImage.transform.localPosition = new Vector3(0, 0, 0);
        waitImage.SetActive(true);
        //new WaitForSeconds(20);
    }

    public void StartGame(SocketIOEvent e)
    {
        mainCanvas.SetActive(true);
        waitImage.SetActive(false);
    }

    public void InitializePlayer(SocketIOEvent e)
    {
        Debug.Log(e.data);

        //var black = e.data.list;

        var isBlack = String.Compare(e.data.list[1].ToString(), "1", StringComparison.Ordinal) == 0;
        var scores = Int32.Parse(e.data.list[2].ToString());
        player = new Player(isBlack, scores);

        var mr = rockColorImage.GetComponent<Image>();
        mr.sprite = Resources.Load<Sprite>(isBlack ? "br" : "wr");

        InitView.player = player;
        FinishMove.player = player;
    }

    public void TestError(SocketIOEvent e)
    {
        // Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
    }

    public void TestClose(SocketIOEvent e)
    {
        Debug.Log("You're disconnect");
    }

    public void PlayerFinishMove(SocketIOEvent e)
    {
        Debug.Log("1314");
        Debug.Log(e.data);

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                InitView.type[i, j] = int.Parse(e.data.list[i * 9 + j].ToString());
                var mr = GameObject.Find((i * 9 + j).ToString()).GetComponent<SpriteRenderer>();

                if (InitView.type[i, j] == 0)
                {
                    mr.enabled = false;
                }
                else
                {
                    mr.sprite = Resources.Load<Sprite>(InitView.type[i, j] == 1 ? "br" : "wr");
                    mr.enabled = true;
                }
                mr.transform.localScale = new Vector3(0.025f, 0.025f);
                    
            }
        }

        OnTouch.PrintMatrix(InitView.type);
            
        //var black = e.data.list;

    }
        
    public void Pause(SocketIOEvent e)
    {
        var time = timeText.GetComponent<Text>();
        time.text = String.Empty;

        var turn = turntext.GetComponent<Text>();
        turn.text = "Opponent Turn";

        var moveNumber = moveText.GetComponent<Text>();
        moveNumber.text = (move++).ToString();

        pauseImage.SetActive(true);
        isPause = true;
        ButtonsHandler.passButton.interactable = false;
    }

    public void Play(SocketIOEvent e)
    {
        var turn = turntext.GetComponent<Text>();
        turn.text = "Your Turn";

        var moveNumber = moveText.GetComponent<Text>();
        moveNumber.text = (move++).ToString();

        isPause = false;
        ButtonsHandler.passButton.interactable = true;
        pauseImage.SetActive(false);
        //new WaitForSeconds(20);
        StartCoroutine(PauseEnd());
    }

    public void EndOfGame(SocketIOEvent e)
    {
        //TODO: e to win scene
        SceneManager.LoadScene("win");

        Debug.Log("Winner: " + e.data.list[0]);
    }

    private IEnumerator PauseEnd()
    {
        var time = 0;
        while (time < 20 && !isPause)
        {
            Debug.Log("TimerCount: " + (time++));
            var text = timeText.GetComponent<Text>();
            text.text = (20 - time + 1).ToString();
            yield return new WaitForSeconds(1);
        }

        if (!isPause)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["row"] = "0";
            data["col"] = "0";
            data["color"] = "0";
            //KOSTYL
            data["pass"] = 0.ToString();

            socket.Emit("POSITION", new JSONObject(data));
            isPause = true;
            //break;
            yield return null;
        }
           
    }

}

