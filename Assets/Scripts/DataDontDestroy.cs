using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDontDestroy : MonoBehaviour
{

    public float level = 1;
    public int numberObjectInit = 3;
    public int numberObjectFalcu = 3;
    public int score;
    private static DataDontDestroy instance;
    public float timerCal = 0.0f;
    public int timeSecond = 0;
    public int timeMinute = 0;

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
        DontDestroyOnLoad(this.gameObject);
    }
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static DataDontDestroy GetInstance()
    {

        return instance;
    }

}
