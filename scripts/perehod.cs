using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class perehod : MonoBehaviour {

    //public GameObject[] button;
    public int e;
    public GameObject[] button = new GameObject[2];
    

    // Use this for initialization

    public void pressButton()
    {
        GameObject c = GameObject.FindGameObjectWithTag("Player");


        DontDestroyOnLoad(c);
       // DontDestroyOnLoad();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    public void pressRestart() {

        Destroy(GameObject.Find("pole"));

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void pressEnd()
    {

        Application.Quit();
    }

    void Start () {

     //   GameObject.Find("endGame").SetActive(false);



    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
