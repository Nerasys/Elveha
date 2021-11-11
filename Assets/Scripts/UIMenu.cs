using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject buttons;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject htp;
    GameObject audioManager;



    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager");
    }

    public void Play()
    {    
        SceneManager.LoadScene(1);
    }

    public void Credit()
    {
        audioManager.GetComponent<AudioManager>().Play("Select");
        buttons.SetActive(false);
        credits.SetActive(true);
    }

 public void HowToPlay()
    {
        audioManager.GetComponent<AudioManager>().Play("Select");
        buttons.SetActive(false);
        htp.SetActive(true);
    }


    public void Quit()
    {
        audioManager.GetComponent<AudioManager>().Play("Select");
        Application.Quit();
    }

    public void Back()
    {
        audioManager.GetComponent<AudioManager>().Play("Select");
        buttons.SetActive(true);
        credits.SetActive(false);
        htp.SetActive(false);
    }


    private void Update()
    {
    }

}
