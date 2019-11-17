using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Timers;
using Object = System.Object;

public class sets : MonoBehaviour {
    int waitTime = 1;
    public Sprite[] imgs;
    public int Index = 0;
    private static System.Timers.Timer aTimer;
    static GameObject Obj;
    public static bool iscoroutine = false;

    public void ChangeImgs() {
     
        if (imgs.Length > Index) {
          
            GetComponent<SpriteRenderer>().sprite = imgs[Index];
        }
       

    }

    void ChangeImg()
    {
     

        print("coroutine");
        if (imgs.Length > Index)
        {

            GetComponent<SpriteRenderer>().sprite = imgs[Index];
        }
     
    }


    // Use this for initialization
    void Start () {
        ChangeImgs();
        

	}
	
	// Update is called once per frame
	public void Update () {
       
          
        ChangeImgs();
        
    }


    public void Change()
    {

        ChangeImg();
        waiting(1000);
       
    }

    public void ChangePlayers()
    {

        ChangeImg();
      


    }


    private static void waiting(int i=3000)
    {
        System.DateTime timer = System.DateTime.Now;
      //  print("Start waiting: " + timer);
        System.DateTime endTime = timer.AddMilliseconds(i);
      //  print(endTime);
        while (timer <= endTime)
        {
            timer = System.DateTime.Now;
        }

      //  print("End waiting: " + timer);
       
    }
}
