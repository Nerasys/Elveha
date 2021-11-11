using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataInfo : MonoBehaviour
{
    // Start is called before the first frame update

    private static DataInfo instance;

    DataDontDestroy dataDontDestroy;

    public bool death = false;

    private AudioManager michel;


    [Header("Gestion Boost")]
    [SerializeField]
    public float boost;
    [SerializeField]
    public float boostMax;
    [SerializeField]
    public float regenBoost;


    [Header("Gestion Damage")]
    [SerializeField]
    public float cancer;
    [SerializeField]
    public float cancerMax;
    [SerializeField]
    public float cancerBoost;
    [SerializeField]
    public float multiplicatorWithLevel;


    [Header("Gestion PNJ")]
    public float acceptanceArretPNJDestination = 2.0f;
    public float cancerNPCDamanage = 0.5f;
    public float acceptancePlayerWithDamage = 2.0f;
    public int minPnj = 1;
    public int maxPnj = 5;

    [Header("Pas Touche")]
    public GameObject roomA;
    public GameObject roomB;
    public GameObject roomC;
    public GameObject roomD;
    public GameObject items;
    GameObject item;
    private GameObject pnj;
    public GameObject[] prefabsPnj;
    [SerializeField]
    public GameObject canvasEndGame;
    [SerializeField] public GameObject canvasList;
    public int indexGenerateO;
    public int indexGenerateB;
    [SerializeField]
    public List<GameObject> myListItems = new List<GameObject>();
    private bool gameOver = false;
    [SerializeField]
    public List<GameObject> prefabsCourse = new List<GameObject>();
    [SerializeField]
    public GameObject listPNJ;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }

        if (michel == null)
        {
            michel = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

        michel.Play("NewGame");
    }

    public static DataInfo GetInstance()
    {
        return instance;
    }


    void Start()
    {
        dataDontDestroy = DataDontDestroy.GetInstance();
        roomA = GameObject.Find("RNG_Room_A");
        roomB = GameObject.Find("RNG_Room_B");
        roomC = GameObject.Find("RNG_Room_C");
        roomD = GameObject.Find("RNG_Room_D");
        canvasEndGame = GameObject.Find("@CanvasEndGame");
        canvasList = GameObject.Find("Canvas").transform.GetChild(7).gameObject;
        GenerateItems();
        GeneratePNJ();
    }

    // Update is called once per frame
    void Update()
    {

        BoostRegen();
        CancerRegen();
        Death();


    }


    void BoostRegen()
    {
        if (boost < boostMax)
        {
            boost += Time.deltaTime * regenBoost;
        }
    }

    void CancerRegen()
    {
        if (cancer < cancerMax)
        {
            cancer += Time.deltaTime * cancerBoost * (dataDontDestroy.level * multiplicatorWithLevel);
        }
    }


    void Death()
    {
        if (cancerMax <= cancer)
        {
            death = true;
        }

        if (death)
        {
            if (!gameOver)
            {
                michel.Play("GameOver");
               gameOver = true;

            }
            canvasEndGame.transform.GetChild(0).gameObject.SetActive(true);
            if (PlayerPrefs.GetInt("scoreMax") < dataDontDestroy.score)
            {
                PlayerPrefs.SetInt("scoreMax", dataDontDestroy.score);
                canvasEndGame.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = dataDontDestroy.score.ToString();
                canvasEndGame.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = dataDontDestroy.score.ToString();
            }
            else
            {
                canvasEndGame.transform.GetChild(0).GetChild(4).GetComponent<Text>().text = PlayerPrefs.GetInt("scoreMax").ToString();
                canvasEndGame.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = dataDontDestroy.score.ToString();
            }


        }
    }

    public struct SpawnItem
    {
        public int room;
        public int spawn;

    }


  List<int> temp = new List<int>();

    void GenerateItems()
    {
        for (int j = 0; j < canvasList.transform.GetChild(3).childCount; j++)
        {
            canvasList.transform.GetChild(3).GetChild(j).gameObject.GetComponent<Text>().enabled = false;
        }

        for (int j = 0; j < canvasList.transform.GetChild(2).childCount; j++)
        {

            canvasList.transform.GetChild(2).GetChild(j).gameObject.GetComponent<Text>().enabled = false;
        }

        int number;
        bool isGood;
        for (int i = 0; i < dataDontDestroy.numberObjectInit; i++)
        {

            do
            {
                isGood = true;
                number = Random.Range(0, prefabsCourse.Count);
                for (int j = 0; j < temp.Count; j++)
                {
                    if (number == temp[j])
                    {
                        isGood = false;
                    }
                }
            } while (isGood == false);
            temp.Add(number);
            prefabsCourse[number].GetComponent<Items>().isObligatoire = true;

            canvasList.transform.GetChild(2).GetChild(indexGenerateO).gameObject.GetComponent<Text>().enabled = true;
            canvasList.transform.GetChild(2).GetChild(indexGenerateO).GetComponent<Text>().text = "-" + prefabsCourse[number].GetComponent<Items>().name;
            indexGenerateO++;
            myListItems.Add(prefabsCourse[number]);



        }
        for (int i = 0; i < dataDontDestroy.numberObjectFalcu; i++)
        {
            isGood = true;
            do
            {
                isGood = true;
                number = Random.Range(0, prefabsCourse.Count);
                for (int j = 0; j < temp.Count; j++)
                {
                    if (number == temp[j])
                    {
                        isGood = false;
                    }
                }
            } while (isGood == false);
            temp.Add(number);
            prefabsCourse[number].GetComponent<Items>().isObligatoire = false;
            canvasList.transform.GetChild(3).GetChild(indexGenerateB).gameObject.GetComponent<Text>().enabled = true;
            canvasList.transform.GetChild(3).GetChild(indexGenerateB).GetComponent<Text>().text = "-" + prefabsCourse[number].GetComponent<Items>().name;
            indexGenerateB++;
            myListItems.Add(prefabsCourse[number]);
        }

        for (int i = 0; i < myListItems.Count; i++)
        {
            List<SpawnItem> tempSpawn = new List<SpawnItem>();
            bool isGood2;
            SpawnItem spawn = new SpawnItem();
            do
            {
                isGood2 = true;
                int randomRoom = Random.Range(0, 4);
                int randomItemsSpawn = Random.Range(1, 11);
                spawn.room = randomRoom;
                spawn.spawn = randomItemsSpawn;

                for (int j = 0; j < tempSpawn.Count; j++)
                {
                    if (spawn.room == tempSpawn[j].room)
                    {
                        if (spawn.spawn == tempSpawn[j].spawn)
                        {
                            isGood2 = false;
                        }
                    }
                }


            } while (!isGood2);
            tempSpawn.Add(spawn);
            switch (spawn.room)
            {
                case 0:
                    for (int k = 0; k < roomA.transform.childCount; k++)
                    {
                        if (roomA.transform.GetChild(k).gameObject.activeSelf)
                        {
                            Vector3 positionItems = roomA.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                            item = Instantiate(myListItems[i], positionItems, myListItems[i].transform.rotation);
                            item.gameObject.name = myListItems[i].name;
                            item.transform.parent = items.transform;

                        }
                    }
                    break;
                case 1:
                    for (int k = 0; k < roomB.transform.childCount; k++)
                    {
                        if (roomB.transform.GetChild(k).gameObject.activeSelf)
                        {
                            Vector3 positionItems = roomB.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                            item = Instantiate(myListItems[i], positionItems, myListItems[i].transform.rotation);
                            item.gameObject.name = myListItems[i].name;
                            item.transform.parent = items.transform;

                        }
                    }
                    break;
                case 2:
                    for (int k = 0; k < roomC.transform.childCount; k++)
                    {
                        if (roomC.transform.GetChild(k).gameObject.activeSelf)
                        {
                            Vector3 positionItems = roomC.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                            //  positionItems.x = -5;
                            //  positionItems.z += 15;
                            item = Instantiate(myListItems[i], positionItems, myListItems[i].transform.rotation);
                            item.gameObject.name = myListItems[i].name;
                            item.transform.parent = items.transform;
                        }
                    }
                    break;
                case 3:
                    for (int k = 0; k < roomD.transform.childCount; k++)
                    {
                        if (roomD.transform.GetChild(k).gameObject.activeSelf)
                        {
                            Vector3 positionItems = roomD.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                            //   positionItems.x = -5;
                            //   positionItems.z += 15;


                            item = Instantiate(myListItems[i], positionItems, myListItems[i].transform.rotation);
                            item.gameObject.name = myListItems[i].name;
                            item.transform.parent = items.transform;

                        }
                    }
                    break;


            }

        }
        temp.Clear();

    }



    [SerializeField] GameObject PNJ;

    void GeneratePNJ()
    {


        for (int i = 0; i < Random.Range(minPnj, maxPnj+1); i++)
        {
            List<SpawnItem> tempSpawn = new List<SpawnItem>();
            bool isGood;
            SpawnItem spawn = new SpawnItem();
            do
            {
                isGood = true;
                int randomRoom = Random.Range(0, 4);
                int randomItemsSpawn = Random.Range(1, 11);
                spawn.room = randomRoom;
                spawn.spawn = randomItemsSpawn;

                for (int j = 0; j < tempSpawn.Count; j++)
                {
                    if (spawn.room == tempSpawn[j].room)
                    {
                        if (spawn.spawn == tempSpawn[j].spawn)
                        {
                            isGood = false;
                        }
                    }
                }
            } while (!isGood);
            tempSpawn.Add(spawn);

            switch (spawn.room)
            {
                case 0:
                    for (int k = 0; k < roomA.transform.childCount; k++)
                    {
                        if (roomA.transform.GetChild(k).gameObject.activeSelf)
                        {
                            Vector3 positionItems = roomA.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                            pnj = Instantiate(prefabsPnj[Random.Range(0, prefabsPnj.Length)], positionItems, Quaternion.identity);
                            pnj.transform.parent = PNJ.transform;

                        }
                    }
                    break;
                case 1:
                    for (int k = 0; k < roomB.transform.childCount; k++)
                    {
                        if (roomB.transform.GetChild(k).gameObject.activeSelf)
                        {
                            Vector3 positionItems = roomB.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                            pnj = Instantiate(prefabsPnj[Random.Range(0, prefabsPnj.Length)], positionItems, Quaternion.identity);
                            pnj.transform.parent = PNJ.transform;

                        }
                    }
                    break;
                case 2:
                    for (int k = 0; k < roomC.transform.childCount; k++)
                    {
                        if (roomC.transform.GetChild(k).gameObject.activeSelf)
                        {
                            Vector3 positionItems = roomC.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                            pnj = Instantiate(prefabsPnj[Random.Range(0, prefabsPnj.Length)], positionItems, Quaternion.identity);
                            pnj.transform.parent = PNJ.transform;
                        }
                    }
                    break;
                case 3:
                    for (int k = 0; k < roomD.transform.childCount; k++)
                    {
                        if (roomD.transform.GetChild(k).gameObject.activeSelf)
                        {
                            Vector3 positionItems = roomD.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                            pnj = Instantiate(prefabsPnj[Random.Range(0, prefabsPnj.Length)], positionItems, Quaternion.identity);
                            pnj.transform.parent = PNJ.transform;

                        }
                    }
                    break;


            }

        }

    }

}
