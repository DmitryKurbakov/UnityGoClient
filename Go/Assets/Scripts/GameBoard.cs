using System;
using System.Collections.Generic;
using Assets.Essences;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameBoard : MonoBehaviour
    {

        public static int[,] type;
   
        public GameObject[] Reg { get; set; }

        public List<GameObject> ListReg { get; set; }

        public static Game game;

        public static int number;

        public static bool isPass = false;

        private Button passButton;
        //public  static int turn = 0;

        // Use this for initialization
        void Start ()
        {

            passButton = GameObject.Find("PassButton").GetComponent<Button>();
            passButton.onClick.AddListener(OnPassButtonClick);

            game = new Game();
            //turn = 0;
            Game.Turn = 0;

            ListReg = new List<GameObject>();
            Reg = GameObject.FindGameObjectsWithTag("reg");
            Array.Sort(Reg, CompareObNames);

            foreach (var t in Reg)
            {
                ListReg.Add(t);
                var mr = t.GetComponent<SpriteRenderer>();
                mr.enabled = false;
            }
            OnTouch.ListReg = ListReg;
        }

        private static int CompareObNames(GameObject x, GameObject y)
        {
            return int.Parse(x.name).CompareTo(int.Parse(y.name));
        }
        // Update is called once per frame
        void Update () {
		
        }

        private void OnPassButtonClick()
        {
            if (isPass)
            {
                EndGame();
            } 
            isPass = true;
        }

        public void EndGame()
        {
            GameObject.Find("baduk_9x9").GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
