using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static GameManager instance;
    public System.Action OnGameOver;



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

    public static GameManager GetInstance()
    {
        return instance;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    [ContextMenu("GameOver")]
    void GameOver()
    {
        OnGameOver();
    }


}
