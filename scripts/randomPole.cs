using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class randomPole : MonoBehaviour
{
    public GameObject eLitters, eNums, ePole;

    public int[] ships;
    GameObject[] litters;
    GameObject[] nums;
    GameObject[,] pole;
    int k, nr, nl;
    int lengPole = 10;
    GameObject Pole;
    void randomCreatePole()
    {
        Vector3 StartPoze = transform.position;
        float x = StartPoze.x + 1;
        float y = StartPoze.y - 1;
        System.Random random = new System.Random();
        litters = new GameObject[lengPole];
        nums = new GameObject[lengPole];

        for (int i = 0; i < lengPole; i++)
        {
            litters[i] = Instantiate(eLitters);
            litters[i].transform.parent = Pole.transform;
            litters[i].transform.position = new Vector3(x, StartPoze.y, StartPoze.z);
            litters[i].GetComponent<sets>().Index = i;
            x++;

            nums[i] = Instantiate(eNums);
            nums[i].transform.parent = Pole.transform;
            nums[i].transform.position = new Vector3(StartPoze.x, y, StartPoze.z);
            nums[i].GetComponent<sets>().Index = i;
            y--;
        }
        x = StartPoze.x + 1;
        y = StartPoze.y - 1;

        pole = new GameObject[lengPole, lengPole];

        for (int i = 0; i < lengPole; i++)
        {
            for (int j = 0; j < lengPole; j++)
            {
                pole[j, i] = Instantiate(ePole);
                pole[j, i].transform.parent = Pole.transform;
                pole[j, i].GetComponent<sets>().Index = 0;
                pole[j, i].transform.position = new Vector3(x, y, StartPoze.z);

                x++;
            }
            x = StartPoze.x + 1;
            y--;
        }

        for (int i = 0; i < ships.Length; ++i)
        {
            for (int j = 0; j <= i; ++j)
            {
                int size = ships[i];
                Console.WriteLine(size.ToString());
                x = (int) random.Next(0, 10);
                y = (int) random.Next(0, 10);
                int direction = random.Next(0, 2);

                while (!test_placement(size, (int)x, (int)y, direction, pole))
                {
                    x = (int) random.Next(0, 10);
                    y = (int) random.Next(0, 10);
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
                        pole[l, (int)y].GetComponent<sets>().Index = 4;
                        //  Console.WriteLine(pole[k, l].ToString());
                    }
                }
                else
                {
                    for (int l = (int)y; l < y + size; l++)
                    {
                        pole[(int)x, l].GetComponent<sets>().Index = 4;
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

    void Start()
    {
        Pole = GameObject.FindGameObjectWithTag("bot");
        print("Pole_bot " + Pole);
        randomCreatePole();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
