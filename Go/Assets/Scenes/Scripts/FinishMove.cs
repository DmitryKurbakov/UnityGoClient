

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Essences;
using Assets.Scripts;
using SocketIO;
using UnityEngine;


public class FinishMove : MonoBehaviour
{

    public static List<GameObject> ListReg;
    public static SocketIOComponent socket;

    public static Player player;
    // Use this for initialization
    void Start ()
    {
        player = Game.player;
        socket = Game.socket;
    }
	
    // Update is called once per frame
    void Update () {
		
    }

    public void OnMouseDown()
    {

        if (InitView.type[int.Parse(name) / 9, int.Parse(name) % 9] != 0 || Game.isPause) return;
        var mr = GetComponent<SpriteRenderer>();
        mr.sprite = Resources.Load<Sprite>(player.isBlack ? "br" : "wr");
        mr.transform.localScale = new Vector3(0.025f, 0.025f);
        mr.enabled = true;

        StartCoroutine(SentPosition());

        //socket.Close();
        //new WaitForSeconds(10);
    }

    private IEnumerator SentPosition()
    {
        // yield return new WaitForSeconds(1);

        Dictionary<string, string> data = new Dictionary<string, string>();
        data["row"] = (int.Parse(name) / 9).ToString();
        data["col"] = (int.Parse(name) % 9).ToString();
        data["color"] = (player.isBlack ? 1 : 2).ToString();
        //KOSTYL
        data["pass"] = 0.ToString();

        socket.Emit("POSITION", new JSONObject(data));

        yield return new WaitForSeconds(0);
            
    }

        
}

