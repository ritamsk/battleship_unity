using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamepole : MonoBehaviour
{

    public GameObject eLitters, eNums, ePole;
  
   // 


    GameObject[] litters;
    GameObject[] nums;
    public static GameObject[,] pole;

    int lengPole = 10;

    GameObject Pole;


    void CreatePole()
    {
        Vector3 StartPoze = transform.position;
        float x = StartPoze.x + 1;
        float y = StartPoze.y - 1;

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
                string id = j.ToString() + i.ToString();

                pole[j, i].GetComponent<sets>().Index = PlayerPrefs.GetInt("pole" + id);
                //for (int k = 0; k < 4; ++k) ships[k] = 0;

                pole[j, i].transform.position = new Vector3(x, y, StartPoze.z);

                x++;
            }
            x = StartPoze.x + 1;
            y--;
        }
    }








    // Use this for initialization
    void Start()
    {
        Pole = GameObject.FindGameObjectWithTag("Player");
        CreatePole();
        //if (Application.loadedLevel == 3) OnLevelWasLoaded(2);
    }
  
    // Update is called once per frame
    void Update()
    {
      

    }
}