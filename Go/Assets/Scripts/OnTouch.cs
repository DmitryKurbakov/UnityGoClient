using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Essences;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class OnTouch : MonoBehaviour {

        public static List<GameObject> ListReg;
        
        // Use this for initialization
        public void Start ()
        {

           
            //Debug.Log(reg);
        }

        
        // Update is called once per frame

        public void Update()
        {
            
        }

        public void OnMouseDown()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (var item in ListReg)
            {
                
                var regPos = item.transform.position;
                const double k = 0.32;
                if (((mousePos.x >= regPos.x - k) && (mousePos.x <= regPos.x + k))
                    && ((mousePos.y >= regPos.y - k) && (mousePos.y <= regPos.y + k)))
                {
                    var mr = item.GetComponent<SpriteRenderer>();
                    mr.sprite = Resources.Load<Sprite>(Game1.Turn++ % 2 == 0 ? "br" : "wr");
                    GameBoard.game.Rocks.ElementAt(int.Parse(name)).type = Game1.Turn % 2 != 0 ? 1 : 2;
                    //GameBoard.number = int.Parse(name);
                    //CaptureTest
                    mr.enabled = true;
                    CaptureTest(1, 2);
                    CaptureTest(2, 1);
                    //Debug.Log(name);
                    // Debug.Log(GameBoard.game.Rocks.ElementAt(int.Parse(name)).type);

                    mr.transform.localScale = new Vector3(0.025f, 0.025f);
                      
                    
                    break;
                }
            }
            
        }

        void CaptureTest(int a, int b)
        {
            var type = new int[9, 9];
            var nStr = -1;
            for (var i = 0; i < 81; i++)
            {
                if (i % 9 == 0) nStr++;
                type[nStr, i % 9] = GameBoard.game.Rocks[i].type;
            }


            var eatenList = new List<int[]>();
            //List<int> whiteRocks = new List<int>();

            for (var i = 0; i < 9; i++)
            {
                var isEatRows = false;
                var eachCol = false;
                var eachRow = false;
                for (var j = 0; j < 9; j++)
                {
                    var k = 0;
                    
                    var isEatCols = new List<Col>();

                    //rows
                    if (type[i, j] == b)
                    {
                        //type[i, j] += 3;

                        k = j;

                        if (k - 1 == -1 || type[i, k - 1] == a)
                        {
                            while (k < 8 && type[i, k + 1] == b)
                            {
                                k++;
                            }

                            if (k == 8 || type[i, k + 1] == a)
                            {
                                isEatRows = true;
                                eachRow = true;
                                eachCol = true;
                                //cols
                                for (var l = j; l < k + 1; l++)
                                {
                                    var m = i;
                                    
                                    if (m - 1 == -1 || type[m - 1, l] == a)
                                    {
                                        
                                        while (m < 9 &&  type[m, l] == b)
                                        {
                                            var posHor = l;
                                            while (posHor > -1)
                                            {
                                                if (type[m, posHor] == b)
                                                {
                                                    eatenList.Add(new []{ m, posHor});
                                                    var posVer = m;

                                                    while (posVer > -1)
                                                    {
                                                        if (type[posVer, posHor] == b)
                                                        {
                                                            eatenList.Add(new[]{ posVer, posHor});
                                                            posVer--;
                                                            continue;
                                                        }
                                                        if (type[posVer, posHor] == a)
                                                        {
                                                            break;
                                                        }
                                                        eatenList.Clear();
                                                        eachCol = false;
                                                        break;
                                                    }

                                                    posVer = m;


                                                    while (posVer < 9)
                                                    {
                                                        if (type[posVer, posHor] == b)
                                                        {
                                                            eatenList.Add(new [] { posVer, posHor});
                                                            posVer++;
                                                            continue;
                                                        }
                                                        if (type[posVer, posHor] == a)
                                                        {
                                                            break;
                                                        }

                                                        eatenList.Clear();
                                                        eachCol = false;
                                                        break;
                                                    }
                                                    
                                                    posHor--;
                                                    continue;
                                                }

                                                if (type[m, posHor] == a)
                                                {
                                                    break;
                                                }
                                                eatenList.Clear();
                                                eachRow = false;
                                                break;
                                            }

                                            posHor = l;

                                            while (posHor < 9)
                                            {
                                                if (type[m, posHor] == b)
                                                {
                                                    eatenList.Add(new [] { m, posHor });
                                                    var posVer = m;
                                                    while (posVer > -1)
                                                    {
                                                        if (type[posVer, posHor] == b)
                                                        {
                                                            eatenList.Add(new[] {posVer, posHor});
                                                            posVer--;
                                                            continue;
                                                        }
                                                        if (type[posVer, posHor] == a)
                                                        {
                                                            break;
                                                        }

                                                        eatenList.Clear();
                                                        eachCol = false;
                                                        break;
                                                    }

                                                    posVer = m;

                                                    while (posVer < 9)
                                                    {
                                                        if (type[posVer, posHor] == b)
                                                        {
                                                            eatenList.Add(new[] {posVer, posHor});
                                                            posVer++;
                                                            continue;
                                                        }
                                                        if (type[posVer, posHor] == a)
                                                        {
                                                            break;
                                                        }

                                                        eatenList.Clear();
                                                        eachCol = false;
                                                        break;
                                                    }
                                                    posHor++;
                                                    continue;
                                                }

                                                if (type[m, posHor] == a)
                                                {
                                                    break;
                                                }

                                                eachRow = false;
                                                break;
                                            }
                                           

                                            m++;
                                        }

                                        if (!eachRow || !eachCol)
                                        {
                                            break;
                                        }

                                        m = m >= 8 ? 8 : m;

                                        if (m == 8 || type[m, l] == a)
                                        {

                                            var col = new Col(m == 8 ? 8 : m - 1, true);
                                            isEatCols.Add(col);
                                        }
                                        else
                                        {
                                            isEatCols = new List<Col>();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        isEatCols = new List<Col>();
                                        break;
                                    }
                                }


                                var eat = false;
                                if (isEatCols.Count > 0)
                                {
                                    eat = true;
                                    foreach (var col in isEatCols)
                                    {
                                        if (!col.isEat)
                                        {
                                            eat = false;
                                            break;
                                        }
                                    }

                                    if (isEatRows && eat && eachRow && eachCol)
                                    {
                                        Debug.Log("EEEEEEEEEEEEEEEEEEEEEE");

                                        for (var l = 0; l < eatenList.Count; l++)
                                        {
                                            for (var m = l + 1; m < eatenList.Count; m++)
                                            {
                                                if ((eatenList[l][0] == eatenList[m][0]) &&
                                                    (eatenList[l][1] == eatenList[m][1]))
                                                {
                                                    eatenList.Remove(eatenList[m]);
                                                }
                                            }
                                        }

                                        for (var l = 0; l < 9; l++)
                                        {
                                            for (var m = 0; m < 9; m++)
                                            {
                                                for (var n = 0; n < eatenList.Count; n++)
                                                {
                                                    if (l == eatenList[n][0] && m == eatenList[n][1])
                                                    {
                                                        //PrintMatrix(type);
                                                        type[l, m] = 0;
                                                       // var mr =
                                                        //    GameObject.Find((l * 9 + m).ToString()).GetComponent<SpriteRenderer>();
                                                        GameBoard.game.Rocks[l * 9 + m].type = 0;
                                                        if (b == 1)
                                                        {
                                                            GameBoard.game.WhitePlayer.Score++;
                                                        }
                                                        else
                                                        {
                                                            GameBoard.game.BlackPlayer.Score++;
                                                        }
                                                       // mr.enabled = false;
                                                        
                                                        //TODO:Scores for players
                                                    }
                                                }
                                            }
                                            

                                        }
                                        eatenList.Clear();
                                        //for (int l = 0; l < isEatCols.Count; l++)
                                        //{
                                        //    for (int m = i; m < isEatCols[l].col + 1; m++)
                                        //    {

                                        //        type[m, j + l] = 0;
                                        //        var mr = GameObject.Find((m * 9 + j + l).ToString()).GetComponent<SpriteRenderer>();
                                        //        mr.enabled = false;
                                        //        //TODO:Scores for players

                                        //    }
                                        //}
                                    }

                                    for (var l = 0; l < 9; l++)
                                    {
                                        for (var m = 0; m < 9; m++)
                                        {
                                            if (type[l, m] == 0)
                                            {
                                                var mr =
                                                            GameObject.Find((l * 9 + m).ToString()).GetComponent<SpriteRenderer>();
                                                mr.enabled = false;
                                            }
                                        }
                                    }
                                   

                                    PrintMatrix(type);
                                }

                                else continue;

                            }
                            else continue;
                           
                            }

                        
                        else continue;

                    }
                  else continue;
                

                }
                //if (isEatRows && eachRow && eachCol)
                //{
                //    for (int l = 0; l < 9; l++)
                //    {
                //        for (int m = 0; m < 9; m++)
                //        {
                //            for (int n = 0; n < eatenList.Count; n++)
                //            {
                //                if (l == eatenList[n][0] && m == eatenList[n][1])
                //                {
                //                    type[l, m] = 0;
                //                    var mr =
                //                        GameObject.Find((l * 9 + m).ToString()).GetComponent<SpriteRenderer>();
                //                    mr.enabled = false;
                //                    //TODO:Scores for players
                //                }
                //            }
                //        }
                //    }
                //}


            }
        }


        public static void PrintMatrix(int[,] type)
        {
            //Debug.Log("-------------------------------------------------------");
            for (var i = 0; i < 9; i++)
            {
                   Debug.Log(String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", type[i, 0], type[i, 1], type[i, 2], type[i, 3], type[i, 4], type[i, 5], type[i, 6], type[i, 7], type[i, 8]));
               
            }
        }

        public void PrintCol(List<Col> cols)
        {
            foreach (var col in cols)
            {
                Debug.Log(col.col);
            }
        }



    }//END of class OnTouch

    public class Col
    {
        public int col = -1;
        public bool isEat = false;

        public Col(int col, bool isEat)
        {
            this.col = col;
            this.isEat = isEat;
        }
    }

    
       

}

