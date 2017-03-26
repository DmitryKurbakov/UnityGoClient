using System;
using System.Collections;
using Assets.Essences;
using Assets.Scripts;
using SocketIO;
using UnityEngine;

namespace Assets.Scenes.web.Scripts
{
    public class CreateConnection : MonoBehaviour
    {
        public static SocketIOComponent socket;
        public static Player player;

        public void Start()
        {
            GameObject go = GameObject.Find("SocketIO");
            socket = go.GetComponent<SocketIOComponent>();
            
            socket.On("START_ATTRIBUTES", InitializePlayer);
            socket.Connect();
            socket.On("FINISH_MOVE", PlayerFinishMove);
            //socket.On("WAIT", Wait);
            //socket.On("open", TestOpen);
            //socket.On("boop", TestBoop);
            socket.On("error", TestError);
            socket.On("close", TestClose);
            
            //StartCoroutine(BeepBoop());
            //socket.autoConnect = false;
            //socket.On("close");
        }


        public void InitializePlayer(SocketIOEvent e)
        {
            Debug.Log(e.data);

            //var black = e.data.list;

            var isBlack = string.Compare(e.data.list[1].ToString(), "1", StringComparison.Ordinal) == 0;
            var scores = int.Parse(e.data.list[2].ToString());
            player = new Player(isBlack, scores);

            InitView.player = player;
            FinishMove.player = player;
            FinishMove.socket = socket;

            //StartCoroutine()

            //player = new Player();
        }
        //private IEnumerator BeepBoop()
        //{
        //    // wait 1 seconds and continue
        //    yield return new WaitForSeconds(1);

        //    socket.Emit("beep", new JSONObject("314314"));

        //    // wait 3 seconds and continue
        //    yield return new WaitForSeconds(3);

        //    socket.Emit("beep");

        //    // wait 2 seconds and continue
        //    yield return new WaitForSeconds(2);

        //    socket.Emit("beep");

        //    // wait ONE FRAME and continue
        //    yield return null;

        //    socket.Emit("beep");
        //    socket.Emit("beep");
        //}

        //public void TestOpen(SocketIOEvent e)
        //{
        //    Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
        //}

        //public void TestBoop(SocketIOEvent e)
        //{
        //    Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);

        //    if (e.data == null) { return; }

        //    Debug.Log(
        //        "#####################################################" +
        //        "THIS: " + e.data.GetField("this").str +
        //        "#####################################################"
        //    );
        //}

        public void TestError(SocketIOEvent e)
        {
           // Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
        }

        public void TestClose(SocketIOEvent e)
        {
           // Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
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

        public void Wait(SocketIOEvent e)
        {
            //Debug.Log("start");
            new WaitForSeconds(20);
            Debug.Log("end");
        }

    }
}
