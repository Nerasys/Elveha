using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlerIA : MonoBehaviour
{
    [SerializeField] GameObject player;
    NavMeshAgent nMA;
    DataInfo dI;
    private AudioManager michel;
    private bool damage = false;

    // Start is called before the first frame update
    void Start()
    {
        nMA = GetComponent<NavMeshAgent>();
        dI = DataInfo.GetInstance();
        player = GameObject.FindGameObjectWithTag("Player");
        if (michel == null)
        {
            michel = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
        SetDestination();
    }



    void SetDestination()
    {
        DataInfo.SpawnItem spawn = new DataInfo.SpawnItem();
        int randomRoom = Random.Range(0, 4);
        int randomItemsSpawn = Random.Range(1, 11);
        spawn.room = randomRoom;
        spawn.spawn = randomItemsSpawn;

        switch (spawn.room)
        {
            case 0:
                for (int k = 0; k < dI.roomA.transform.childCount; k++)
                {
                    if (dI.roomA.transform.GetChild(k).gameObject.activeSelf)
                    {
                        Vector3 positionItems = dI.roomA.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                        nMA.SetDestination(positionItems);
                    }
                }
                break;
            case 1:
                for (int k = 0; k < dI.roomB.transform.childCount; k++)
                {
                    if (dI.roomB.transform.GetChild(k).gameObject.activeSelf)
                    {
                        Vector3 positionItems = dI.roomB.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                        nMA.SetDestination(positionItems);
                    }
                }
                break;
            case 2:
                for (int k = 0; k < dI.roomC.transform.childCount; k++)
                {
                    if (dI.roomC.transform.GetChild(k).gameObject.activeSelf)
                    {
                        Vector3 positionItems = dI.roomC.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                        nMA.SetDestination(positionItems);
                    }
                }
                break;
            case 3:
                for (int k = 0; k < dI.roomD.transform.childCount; k++)
                {
                    if (dI.roomD.transform.GetChild(k).gameObject.activeSelf)
                    {
                        Vector3 positionItems = dI.roomD.transform.GetChild(k).Find("Spawn_Item_" + spawn.spawn.ToString()).transform.position;
                        nMA.SetDestination(positionItems);
                    }
                }
                break;

        }

    }


    void NewDestination()
    {

        if (nMA.remainingDistance < dI.acceptanceArretPNJDestination)
        {
            SetDestination();
        }

    }



    // Update is called once per frame
    void Update()
    {
        NewDestination();

    }




}
