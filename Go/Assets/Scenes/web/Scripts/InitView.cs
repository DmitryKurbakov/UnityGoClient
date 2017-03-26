﻿using System;
using System.Collections.Generic;
using Assets.Essences;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scenes.web.Scripts
{
    public class InitView : MonoBehaviour {

        //public static int[,] type;

        public GameObject[] Reg { get; set; }

        public List<GameObject> ListReg { get; set; }

        public static Player player;

        public static int[,] type;
        //public static Game game;

        //public static int number;

        //public static bool isPass = false;

        //private Button passButton;
        // Use this for initialization
        void Start ()
        {

            type = new int[9,9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    type[i, j] = 0;
                }
            }
            //player = CreateConnection.player;

            ListReg = new List<GameObject>();
            Reg = GameObject.FindGameObjectsWithTag("reg");
            Array.Sort(Reg, CompareObNames);

            foreach (var t in Reg)
            {
                ListReg.Add(t);
                var mr = t.GetComponent<SpriteRenderer>();
                mr.enabled = false;
            }
            FinishMove.ListReg = ListReg;
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        private static int CompareObNames(GameObject x, GameObject y)
        {
            return int.Parse(x.name).CompareTo(int.Parse(y.name));
        }
    }
}
