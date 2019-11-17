using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class randomPlayerPole : MonoBehaviour
{
    public int[] ships;
    public  GameObject[,] pole = new GameObject [10, 10];
    public GameObject[] button = new GameObject[4];
    int x;
    int y;
    // Start is called before the first frame update
    public void PlayerPole_random()
    {
        System.Random random = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                pole[i, j] = gamepole.pole[i, j];
                print(pole[i, j]);

            }
        }
                for (int i = 0; i < ships.Length; ++i)
        {
            for (int j = 0; j <= i; ++j)
            {
                int size = ships[i];
                //Console.WriteLine(size.ToString());
                x = (int)random.Next(0, 10);
                y = (int)random.Next(0, 10);
                int direction = random.Next(0, 2);

                while (!test_placement(size, (int)x, (int)y, direction, pole))
                {
                    x = (int)random.Next(0, 10);
                    y = (int)random.Next(0, 10);
                    direction = random.Next(0, 2);
                    // Console.WriteLine(x.ToString() + ", " + y.ToString() + ", " + direction.ToString());
                }
                //place_ship(size, 1, coordinate, direction);
                int dirx = 0;
                int diry = 0;
                if (direction == 0)
                {
                    for (int l = (int)x; l < (int)x + size; l++)
                    {
                        pole[l, (int)y].GetComponent<sets>().Index = 1;
                        //  Console.WriteLine(pole[k, l].ToString());
                    }
                }
                else
                {
                    for (int l = (int)y; l < y + size; l++)
                    {
                        pole[(int)x, l].GetComponent<sets>().Index = 1;
                        //  Console.WriteLine(pole[k, l].ToString());
                    }
                }


                for (int k = 0; k < 10; ++k)
                {
                    string s = "";
                    for (int l = 0; l < 10; ++l)
                    {
                        s += pole[k, l].GetComponent<sets>().Index.ToString() + " ";
                        // pole[i, j] = 0;
                    }

                }

            }
        }

        for (int i = 0; i < 10; ++i)
        {
            string s = "";
            for (int j = 0; j < 10; ++j)
            {
                s += pole[i, j].GetComponent<sets>().Index.ToString() + " ";
                // pole[i, j] = 0;
            }
            // Console.WriteLine(s);
        }
        ifplaced(ships);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ifplaced(int[] ships)
    {
        ///print("ifplaced0");

        // print("ifplaced 2");
        button[0].GetComponent<Image>().color = Color.clear;
        button[1].GetComponent<Text>().color = Color.clear;

        button[2].GetComponent<Image>().color = Color.white;
        button[3].GetComponent<Text>().color = Color.black;
            //return true;


       
    }
        public static bool test_placement(int size, int x, int y, int direction, GameObject[,] pole)
    {
        int dirx = 0;
        int diry = 0;
        if (direction == 0)
        {
            dirx = 1;
            diry = 0;
        }
        else
        {
            dirx = 0;
            diry = 1;
        }


        if ((x + size >= 10) && (direction == 0))
        {
            return false;
        }
        else
        {
            if ((y + size >= 10) && (direction == 1))
            {
                return false;
            }
            else
            {

                for (int i = x; i <= x + size * dirx; i++)
                {
                    for (int j = y; j <= y + size * diry; j++)
                    {
                        if (i < 10 && j < 10)
                        {
                            if (pole[i, j].GetComponent<sets>().Index >= 1)
                                return false;
                            else
                            {
                                if (direction == 0)
                                {
                                    if (x - 1 >= 0)
                                    {
                                        if (pole[x - 1, y].GetComponent<sets>().Index >= 1) return false;
                                    }
                                    if (i + 1 < 10)
                                    {

                                        if (pole[i + 1, j].GetComponent<sets>().Index >= 1)
                                            return false;
                                        if (j - 1 >= 0)
                                        {
                                            if (pole[i + 1, j - 1].GetComponent<sets>().Index >= 1 || pole[i, j - 1].GetComponent<sets>().Index >= 1)
                                                return false;
                                        }
                                        if (j + 1 < 10)
                                        {
                                            if (pole[i + 1, j + 1].GetComponent<sets>().Index >= 1 || pole[i, j + 1].GetComponent<sets>().Index >= 1)
                                                return false;
                                        }
                                    }
                                    if (i - 1 >= 0)
                                    {


                                        if (j - 1 >= 0)
                                        {
                                            if (pole[i - 1, j - 1].GetComponent<sets>().Index >= 1 || pole[i, j - 1].GetComponent<sets>().Index >= 1)
                                                return false;
                                        }
                                        if (j + 1 < 10)
                                        {
                                            if (pole[i - 1, j + 1].GetComponent<sets>().Index >= 1 || pole[i, j + 1].GetComponent<sets>().Index >= 1)
                                                return false;
                                        }

                                    }

                                }
                                else
                                {
                                    if (y - 1 >= 0)
                                    {
                                        if (pole[x, y - 1].GetComponent<sets>().Index >= 1) return false;
                                    }
                                    if (i - 1 >= 0)
                                    {

                                        if (j - 1 >= 0)
                                        {
                                            if (pole[i - 1, j - 1].GetComponent<sets>().Index >= 1 || pole[i - 1, j].GetComponent<sets>().Index >= 1)
                                                return false;
                                        }
                                        if (j + 1 < 10)
                                        {
                                            if (pole[i - 1, j + 1].GetComponent<sets>().Index >= 1 || pole[i - 1, j].GetComponent<sets>().Index >= 1 || pole[i, j + 1].GetComponent<sets>().Index >= 1)
                                                return false;
                                        }

                                    }

                                    if (i + 1 < 10)
                                    {

                                        if (pole[i + 1, j].GetComponent<sets>().Index >= 1)
                                            return false;
                                        if (j - 1 >= 0)
                                        {
                                            if (pole[i + 1, j - 1].GetComponent<sets>().Index >= 1)
                                                return false;
                                        }
                                        if (j + 1 < 10)
                                        {
                                            if (pole[i + 1, j + 1].GetComponent<sets>().Index >= 1 || pole[i, j + 1].GetComponent<sets>().Index >= 1)
                                                return false;
                                        }
                                    }
                                }

                            }
                        }
                        else { return false; }
                    }
                }

            }
        }
        return true;
    }

}
