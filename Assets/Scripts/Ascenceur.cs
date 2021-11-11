using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ascenceur : MonoBehaviour
{
    // Start is called before the first frame update
    DataInfo data;
    DataDontDestroy dtn;
    private AudioManager michel;

    bool victory = true;
    void Start()
    {
        data = DataInfo.GetInstance();
        dtn = DataDontDestroy.GetInstance();
        if (michel == null)
        {
            michel = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            victory = true;
            for (int i = 0; i < data.myListItems.Count; i++)
            {
                if (data.myListItems[i].GetComponent<Items>().isObligatoire == true)
                {
                    victory = false;
                    Debug.Log("pa sla victoire");
                }

            }

            if (victory)
            {

                michel.StopPlay("Drift");
                michel.StopPlay("BoucleRoue");
                dtn.level++;
                dtn.score +=500 ;
                data.myListItems.Clear();
                data.cancer = 0.0f;
                data.boost = 0.0f;
                data.indexGenerateB = 0;
                data.indexGenerateO = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

           

            }


        }
    }
}
