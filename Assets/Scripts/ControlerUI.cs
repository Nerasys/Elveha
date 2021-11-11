using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlerUI : MonoBehaviour
{
    // Start is called before the first frame update
    DataInfo dI;
    DataDontDestroy dtn;
    [SerializeField] Image cancerImage;
    [SerializeField] Image boostImage;

    [SerializeField] Text score;
    [SerializeField] Text timer;

 [SerializeField] Text niveau;
   
    void Start()
    {
        dI = DataInfo.GetInstance();
        dtn = DataDontDestroy.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateBoost();
        UpdateTimer();
        UpdateScore();
    }



    private void UpdateHealth()
    {
        dI.cancer = Mathf.Clamp(dI.cancer, 0, dI.cancerMax);
        float amount = (float)dI.cancer / dI.cancerMax;
        cancerImage.fillAmount = amount;
    }

    private void UpdateBoost()
    {
        dI.boost = Mathf.Clamp(dI.boost, 0, dI.boostMax);
        float amount = (float)dI.boost / dI.boostMax;
        boostImage.fillAmount = amount;
    }



    private void UpdateTimer()
    {
        if (!dI.death)
        {
            dtn.timerCal += Time.deltaTime;
            dtn.timeSecond = (int)dtn.timerCal;

            if (dtn.timeSecond > 59)
            {
                dtn.timeSecond -= 60;
                dtn.timerCal -= 60;
                dtn.timeMinute += 1;
            }
            if (dtn.timeMinute < 10)
            {
                if (dtn.timeSecond < 10)
                {
                    timer.text = " 0" + dtn.timeMinute.ToString() + " : 0" + dtn.timeSecond.ToString();
                }
                else
                {
                    timer.text = " 0" + dtn.timeMinute.ToString() + " : " + dtn.timeSecond.ToString();
                }

            }
            else
            {
                if (dtn.timeSecond < 10)
                {
                    timer.text = dtn.timeMinute.ToString() + " : 0" + dtn.timeSecond.ToString();
                }
                else
                {
                    timer.text = dtn.timeMinute.ToString() + ": " + dtn.timeSecond.ToString();
                }

            }
        }
    }

    private void UpdateScore()
    {
        score.text = dtn.score.ToString();
        niveau.text = dtn.level.ToString();
    }
}
