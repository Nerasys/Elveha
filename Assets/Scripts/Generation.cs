using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Generation : MonoBehaviour
{
    // Start is called before the first frame update
    private static Generation instance;

    public GameObject wallsL;
    public GameObject wallsR;

    public GameObject roomA;
    public GameObject roomB;
    public GameObject roomC;
    public GameObject roomD;
    //Pour rémi le con
    int wall1;
    int wall2;
    int numberRoomA;
    int numberRoomB;
    int numberRoomC;
    int numberRoomD;

    [SerializeField] GameObject player;
    DataDontDestroy dtn;

    int spawnPlayer;

    DataInfo data;

    int indexRoomA;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }


    }

    public static Generation GetInstance()
    {

        return instance;
    }
    // Update is called once per frame



    private void Start()
    {
        data = DataInfo.GetInstance();
        dtn = DataDontDestroy.GetInstance();
        GenerationRoom();
    }


    void GenerationRoom()
    {


        spawnPlayer = 0;

        wall1 = Random.Range(0, 3);
        wall2 = Random.Range(0, 3);
        wallsL.transform.GetChild(wall1).gameObject.SetActive(true);
        wallsR.transform.GetChild(wall2).gameObject.SetActive(true);
        //Generation Room A
        numberRoomA = Random.Range(0, 3);
        switch (wall1)
        {
            case 0:
                roomA.transform.GetChild(0 + numberRoomA).gameObject.SetActive(true);
                indexRoomA = 0 + numberRoomA;
                break;
            case 1:
                roomA.transform.GetChild(3 + numberRoomA).gameObject.SetActive(true);
                indexRoomA = 3 + numberRoomA;
                break;
            case 2:
                roomA.transform.GetChild(6 + numberRoomA).gameObject.SetActive(true);
                indexRoomA = 6 + numberRoomA;
                break;
        }


        switch (numberRoomA)
        {
            case 0:
                roomB.transform.GetChild(0 + wall2).gameObject.SetActive(true);
                break;
            case 1:
                roomB.transform.GetChild(3 + wall2).gameObject.SetActive(true);
                break;
            case 2:
                roomB.transform.GetChild(6 + wall2).gameObject.SetActive(true);
                break;
        }

        numberRoomC = Random.Range(0, 3);
        switch (wall2)
        {
            case 0:
                roomC.transform.GetChild(0 + numberRoomC).gameObject.SetActive(true);
                break;
            case 1:
                roomC.transform.GetChild(3 + numberRoomC).gameObject.SetActive(true);
                break;
            case 2:
                roomC.transform.GetChild(6 + numberRoomC).gameObject.SetActive(true);
                break;
        }

        switch (numberRoomC)
        {
            case 0:
                roomD.transform.GetChild(0 + wall1).gameObject.SetActive(true);
                break;
            case 1:
                roomD.transform.GetChild(3 + wall1).gameObject.SetActive(true);
                break;
            case 2:
                roomD.transform.GetChild(6 + wall1).gameObject.SetActive(true);
                break;
        }

        switch (spawnPlayer)
        {
            case 0:
                for (int i = 0; i < roomA.transform.childCount; i++)
                {
                    if (roomA.transform.GetChild(i).gameObject.activeSelf)
                    {

                        player.transform.position = roomA.transform.GetChild(i).Find("Spawn_Player").position;

                    }
                }
                break;
            case 1:
                for (int i = 0; i < roomB.transform.childCount; i++)
                {
                    if (roomB.transform.GetChild(i).gameObject.activeSelf)
                    {

                        player.transform.position = roomB.transform.GetChild(i).Find("Spawn_Player").position;

                    }
                }
                break;
            case 2:
                for (int i = 0; i < roomC.transform.childCount; i++)
                {
                    if (roomC.transform.GetChild(i).gameObject.activeSelf)
                    {

                        player.transform.position = roomC.transform.GetChild(i).Find("Spawn_Player").position;

                    }
                }
                break;
            case 3:
                for (int i = 0; i < roomD.transform.childCount; i++)
                {
                    if (roomD.transform.GetChild(i).gameObject.activeSelf)
                    {

                        player.transform.position = roomD.transform.GetChild(i).Find("Spawn_Player").position;

                    }
                }
                break;

        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            data.myListItems.Clear();
            data.cancer = 0.0f;
            data.boost = 0.0f;
            data.indexGenerateB = 0;
            data.indexGenerateO = 0;
            dtn.timeMinute = 0;
            dtn.timerCal = 0;
            dtn.timeSecond = 0;
            dtn.score = 0;
            data.canvasEndGame.transform.GetChild(0).gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            data.myListItems.Clear();
            data.cancer = 0.0f;
            data.boost = 0.0f;
            data.indexGenerateB = 0;
            data.indexGenerateO = 0;
            dtn.timeMinute = 0;
            dtn.timerCal = 0;
            dtn.timeSecond = 0;
            dtn.score = 0;
            data.canvasEndGame.transform.GetChild(0).gameObject.SetActive(false);
            SceneManager.LoadScene(0);

        }

    }
}

