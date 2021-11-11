using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;
    void Start()
    {
        gm = GameManager.GetInstance();
        gm.OnGameOver += DialogGameOver;
        gm.OnGameOver += Dialog2GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DialogGameOver()
    {
        Debug.Log("azertyuiop");

    }

    void Dialog2GameOver()
    {
        Debug.Log("Dylan le mec torp fort");
    }
}
