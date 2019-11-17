using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class clickpole : MonoBehaviour {
    int k, nr, nl;
    public int[] ships;
    int ii = -1;
    int jj = -1;
    int count_chooting =0;
    int waitTime = 5;
    public GameObject[] button = new GameObject[4];
    public GameObject clickedGmObj;
    public Text turn;
    string endGame = "";
    int countShips_bot = -1;
    int countShips_PLayer = -1;
    int nbrOfShips = 0;
    Text endText;
    bool isEndGame= false;
    public AudioClip shootSound;
    public AudioClip missSound;
    public AudioSource efxSource;
    bool whatturn = true; //player`s turn


    public GameObject GetClickedGameObject()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
       
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            return hit.transform.gameObject;
        else
            return null;
    }
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (efxSource == null && SceneManager.GetActiveScene().buildIndex == 1) {
        efxSource = GameObject.Find("audioSource").GetComponent<AudioSource>();
      //  print(efxSource);
        }

        if (Input.GetMouseButtonDown(0) || count_chooting > 0)
        {
          

            Vector3 c = Input.mousePosition;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            clickedGmObj = GetClickedGameObject();
            if (clickedGmObj != null || count_chooting > 0) {
             

                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    //checking players placement                                      
                    for (int i = 0; i < 10; ++i) {
                        for (int j = 0; j < 10; ++j) {

                            if (clickedGmObj.Equals(gamepole.pole[j, i]))
                                if (shipcheck(j, i))
                                {
                                    gamepole.pole[j, i].GetComponent<sets>().Index = 1;
                                    ifplaced(ships);
                                }
                        }
                    }
                    
                }
                else {
                    if(SceneManager.GetActiveScene().buildIndex == 1) {


                        if (whatturn) {
                            //player`s turn

                            if ((clickedGmObj.transform.parent.name == "pole_bot"))
                            {
                                if (clickedGmObj.GetComponent<sets>().Index == 4)
                                {
                                    clickedGmObj.GetComponent<sets>().Index = 2; //kill
                                    clickedGmObj.GetComponent<sets>().ChangePlayers();
                                    countShips_bot -= 1; 
                                    PlaySingle(shootSound);

                                }

                                else
                                {
                                    if (clickedGmObj.GetComponent<sets>().Index == 0)
                                    {
                                        clickedGmObj.GetComponent<sets>().Index = 3;//miss
                                        clickedGmObj.GetComponent<sets>().ChangePlayers();
                                        PlaySingle(missSound);
                                        whatturn = false; //switch to bot`s turn
                                        count_chooting = 1; //at least 1 bot`s shoot need to be done

                                    }
                                }
                            }

                        }
                        else {

                            int isshoot = 1;

                            isshoot = shoot(ii, jj, out ii, out jj);
                            count_chooting += isshoot;
                            if (isshoot == 1)
                            {
                                //kill
                                PlaySingle(shootSound);
                                countShips_PLayer -= 1;
                                count_chooting = 1;
                            }
                            else
                            {
                                //miss
                                count_chooting = 0;
                                PlaySingle(missSound);
                                whatturn = true;
                                count_chooting = 0;
                            }

                        }

                    }
                }

            }
        }

        //print(count_chooting);
        if (isEndGame)
        {


            Destroy(GameObject.Find("pole"));
            GameObject.Find("pole_bot").SetActive(false);
            GameObject.Find("turn").SetActive(false);
            GameObject restart_button = GameObject.Find("restart");
            restart_button.GetComponent<Image>().color = Color.white;
            restart_button.GetComponentInChildren<Text>().color = Color.black;
        }

        if (SceneManager.GetActiveScene().buildIndex == 1 && !isEndGame)
        {

            turn = FindObjectOfType<Text>();
            if (count_chooting == 0)
            {
                turn.text = "your turn";

            }
            else
            {
                turn.text = "computer turn";
               
            }

        }
        if (countShips_bot == 0)
        {

            GameObject.Find("endGame").GetComponent<Text>().color = Color.black;
          
            endGame = "win";
            endText = GameObject.Find("endGame").GetComponent<Text>();
            endText.text = "You " + endGame;

            print(endText.text);
            isEndGame = true;


        }
        if (countShips_PLayer == 0)
        {

            GameObject.Find("endGame").GetComponent<Text>().color = Color.black;
          
            endGame = "lose";
            endText = GameObject.Find("endGame").GetComponent<Text>();
            endText.text = "You " + endGame;
            print(endText.text);
            isEndGame = true;


        }

        if (SceneManager.GetActiveScene().buildIndex == 0 && nbrOfShips == 0)
        {
            for (int i = 0; i < 4; ++i) { nbrOfShips += ships[i] * (i + 1); }
            countShips_bot = nbrOfShips;
            countShips_PLayer = nbrOfShips;

        }
        
    }

    public bool shipcheck(int x, int y)
    {
        int t = 0;
        // int n = 0;
        if ((x > -1) && (x < 10) && (y > -1) && (y < 10))
        {
            int[] xx = new int[9], yy = new int[9];
            xx[0] = x + 1; yy[0] = y + 1;
            xx[1] = x; yy[1] = y + 1;
            xx[2] = x - 1; yy[2] = y + 1;
            xx[3] = x + 1; yy[3] = y;
            xx[4] = x; yy[4] = y;
            xx[5] = x - 1; yy[5] = y;
            xx[6] = x + 1; yy[6] = y - 1;
            xx[7] = x; yy[7] = y - 1;
            xx[8] = x - 1; yy[8] = y - 1;

            for (int i = 0; i < 9; i += 2)
            {

                if ((xx[i] > -1) && (xx[i] < 10) && (yy[i] > -1) && (yy[i] < 10))
                {
                    if (gamepole.pole[xx[i], yy[i]].GetComponent<sets>().Index != 0)
                    {
                        k = 0; nr = nl = 0;
                        return false;
                    }
                }
            }

            for (int i = 1; i < 8; i += 2)
            {
                if ((xx[i] > -1) && (xx[i] < 10) && (yy[i] > -1) && (yy[i] < 10))
                {
                    t += 1;
                }
            }
            k = 0;
            nr = 0;
            nl = 0;

            //if (t == 4)
            // {
            if ((xx[3] > -1) && (xx[3] < 10) && (yy[3] > -1) && (yy[3] < 10))
            {
                if ((gamepole.pole[xx[3], yy[3]].GetComponent<sets>().Index != 0))
                {
                    for (int j = 1; j < 4; ++j) { if ((xx[4] + j > -1) && (xx[4] + j < 10)) { if (gamepole.pole[xx[4] + j, yy[4]].GetComponent<sets>().Index != 0) { ++k; ++nr; print("1++"); } else break; } }
                    //if ((ships[k] > 0) && (k > 0)) { ships[k] -= 1; ships[k - 1] += 1; return true; }
                }
            }
            if ((xx[5] > -1) && (xx[5] < 10) && (yy[5] > -1) && (yy[5] < 10))
            {
                if ((gamepole.pole[xx[5], yy[5]].GetComponent<sets>().Index != 0))
                {
                    for (int j = 1; j < 4; ++j) { if ((xx[4] - j > -1) && (xx[4] - j < 10)) { if (gamepole.pole[xx[4] - j, yy[4]].GetComponent<sets>().Index != 0) { ++k; ++nl; print("2++"); } else break; } }
                    //if ((ships[k] > 0) && (k > 0)) { ships[k] -= 1; ships[k - 1] += 1; return true; }
                }
            }

            if (k == 0)
            {
                if ((xx[1] > -1) && (xx[1] < 10) && (yy[1] > -1) && (yy[1] < 10))
                {
                    if ((gamepole.pole[xx[1], yy[1]].GetComponent<sets>().Index != 0))
                    {
                        for (int j = 1; j < 4; ++j) { if ((yy[4] + j > -1) && (yy[4] + j < 10)) { if (gamepole.pole[xx[4], yy[4] + j].GetComponent<sets>().Index != 0) { ++k; ++nr; print("3++"); } else break; } }
                        //if ((ships[k] > 0) && (k > 0)) { ships[k] -= 1; ships[k - 1] += 1; return true; }
                    }
                }
                if ((xx[7] > -1) && (xx[7] < 10) && (yy[7] > -1) && (yy[7] < 10))
                {
                    if ((gamepole.pole[xx[7], yy[7]].GetComponent<sets>().Index != 0))
                    {
                        for (int j = 1; j < 4; ++j) { if ((yy[4] - j > -1) && (yy[4] - j < 10)) { if (gamepole.pole[xx[4], yy[4] - j].GetComponent<sets>().Index != 0) { ++k; ++nl; print("4++"); } else break; } }
                        //if ((ships[k] > 0) && (k > 0)) { ships[k]-=1; ships[k-1]+=1; return true; }
                    }
                }
            }
            // }

            print("nr"); print(nr); print("nl"); print(nl);
            print("k"); print(k);
            if ((k > 0) && (k < 4))
            {
                if (ships[k] > 0)
                {
                    ships[k] -= 1;
                    if (((nl == 0) && (nr == k))) { ships[k - 1] += 1; k = 0; nr = 0; print(".1"); }
                    else
                    {
                        if (((nr == 0) && (nl == k))) { ships[k - 1] += 1; k = 0; nl = 0; print(".2"); }
                        else { if (((nl > 0) && (nr > 0))) { ships[k - nr - 1] += 1; ships[k - nl - 1] += 1; nl = 0; k = 0; nr = 0; print(".3"); } }
                    }
                    print("nl"); print(nl); print("nr"); print(nr); print("k"); print(k); //ifplaced(ships); 
                    return true;
                }
            }
            else
            {


                if ((ships[0] != 0) && (k == 0))
                {
                    ships[0]--;
                    k = 0;
                    nr = nl = 0;
                    //ifplaced(ships);
                    return true;
                }
            }
        }
        k = 0;
        nr = nl = 0;
        //ifplaced(ships);

        return false;
    }

    public void ifplaced(int[] ships)
    {
        ///print("ifplaced0");
        int n = 0;
        for (int i = 0; i < 4; ++i) { n += ships[i]; }
        if (n == 0)
        {
            // print("ifplaced 2");
            button[0].GetComponent<Image>().color = Color.white;

            button[1].GetComponent<Text>().color = Color.black;
            //return true;


        }
    }


    int shoot(int i, int j, out int k, out int l) {
       
        System.Random random = new System.Random();
        k = i;
        l = j;
      
        if (i == -1 && j == -1)
        {
            //if we dont have ship we kill before

            do
            {
                i = (int)random.Next(0, 10);
                j = (int)random.Next(0, 10);
            } while (gamepole.pole[j, i].GetComponent<sets>().Index == 3 || gamepole.pole[j, i].GetComponent<sets>().Index == 2);

            if (gamepole.pole[j, i].GetComponent<sets>().Index == 1)
            {

                gamepole.pole[j, i].GetComponent<sets>().Index = 2;

                gamepole.pole[j, i].GetComponent<sets>().Change();
                k = i;
                l = j;
                return 1;

            }
            else
            {
                k = ii;
                l = jj;
                gamepole.pole[j, i].GetComponent<sets>().Index = 3;

                gamepole.pole[j, i].GetComponent<sets>().Change();
                return 0;
            }
        }
        else {
            //trying to get place for shoot nearby
            bool isget = false;
            for (int count = 0; count < 4; count++) {

                    if (i-1>=0 ) {
                        if (gamepole.pole[j, i-1].GetComponent<sets>().Index != 3 && gamepole.pole[j, i-1].GetComponent<sets>().Index != 2)
                        {
                            i -= 1;
                            isget = true;
                            break;
                        }
                    }
                    if (j-1>=0)
                    {
                        if (gamepole.pole[j - 1, i].GetComponent<sets>().Index != 3 && gamepole.pole[j-1, i].GetComponent<sets>().Index != 2)
                        {
                            j -= 1;
                            isget = true;
                            break;
                        }
                    }
                    if (i+1<10)
                    {
                        if (gamepole.pole[j, i + 1].GetComponent<sets>().Index != 3 && gamepole.pole[j, i + 1].GetComponent<sets>().Index != 2)
                        {
                            i +=1;
                            isget = true;
                             break;
                        }
                    }
                    if (j+1<10)
                    {
                        if (gamepole.pole[j + 1, i].GetComponent<sets>().Index != 3 && gamepole.pole[j + 1, i].GetComponent<sets>().Index != 2)
                        {
                            j += 1;
                            isget = true;
                            break;
                        }
                    }

                    count += 1;
            }
       
          //  print("fin ij " + i.ToString() + " " + j.ToString());
            if (isget)
            {

                if (gamepole.pole[j, i].GetComponent<sets>().Index == 1)
                {
                    //kill
                    gamepole.pole[j, i].GetComponent<sets>().Index = 2;
                    k = i;
                    l = j;

                    gamepole.pole[j, i].GetComponent<sets>().Change();
                    return 1;

                }
                else
                {
                    //miss 
                    k = ii; //remembering last kill
                    l = jj;
                    gamepole.pole[j, i].GetComponent<sets>().Index = 3;

                    gamepole.pole[j, i].GetComponent<sets>().Change();
                    return 0;
                }
            }
            else {
                //if all possible places nearby were shooted 
                do
                {
                    i = (int)random.Next(0, 10);
                    j = (int)random.Next(0, 10);
                } while (gamepole.pole[j, i].GetComponent<sets>().Index == 3 || gamepole.pole[j, i].GetComponent<sets>().Index == 2);

                if (gamepole.pole[j, i].GetComponent<sets>().Index == 1)
                {

                    gamepole.pole[j, i].GetComponent<sets>().Index = 2;
                    k = i;
                    l = j;

                    gamepole.pole[j, i].GetComponent<sets>().Change();
                    return 1;

                }
                else
                {
                    k = ii;
                    l = jj;

                    gamepole.pole[j, i].GetComponent<sets>().Index = 3;
                    gamepole.pole[j, i].GetComponent<sets>().Change();
                    return 0;
                }

            }

        }

    }


    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }


}
