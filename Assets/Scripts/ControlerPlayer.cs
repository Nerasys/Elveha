using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlerPlayer : MonoBehaviour
{

    private AudioManager michel;
    // Start is called before the first frame update
    private static ControlerPlayer instance;

    DataInfo data;

    [Header("Physics")]

    public float moveSpeed = 10.0f;
    public float moveBoost = 10.0f;
    public float rotateSpeed = 0.5f;
    public bool useRelativeForce = true;

    [Header("Touche")]

    public KeyCode Accelerate = KeyCode.Z;
    public KeyCode Decelerate = KeyCode.S;
    public KeyCode SteerLeft = KeyCode.Q;
    public KeyCode SteerRight = KeyCode.D;
    public KeyCode Accelerate2 = KeyCode.UpArrow;
    public KeyCode Decelerate2 = KeyCode.DownArrow;
    public KeyCode SteerLeft2 = KeyCode.LeftArrow;
    public KeyCode SteerRight2 = KeyCode.RightArrow;
    public KeyCode Boost = KeyCode.Space;



    //Movement Limit
    [Header("Limits et Timer")]

    public float limSpeedMin = -20.0f;
    public float limSpeedMax = 20.0f;
    public float timerFXboost = 2.0f;
    private float timerBoost = 0.0f;
    bool isBoosted = false;
    //Movemement 

    [Header ("FX")]

    [SerializeField]
    public GameObject fx1Z;
    [SerializeField]
    public GameObject fx2Z;
    [SerializeField]
    public GameObject fxVitessBoost;

    [Header("PAS TOUCH")]

    Rigidbody rb;
    DataDontDestroy dtn;


    bool isDrift = false;
    bool isMoveSound = false;
    bool takeDamage = false;
    bool damageSound = false;

    // Use this for initialization


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

        if(michel == null)
        {
            michel = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
      //  michel.Play("LevelBegin");
    }

    public static ControlerPlayer GetInstance()
    {
        return instance;
    }

    void Start()
    {
        dtn = DataDontDestroy.GetInstance();
        rb = gameObject.GetComponent<Rigidbody>();
        data = DataInfo.GetInstance();

    }

    void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!data.death)
        {
            OnDrive();
            OnLimit();
            TakeDamage();
        }
    }


    void TakeDamage()
    {
        takeDamage = false;
        for (int i = 0; i < data.listPNJ.transform.childCount; i++)
        {
            Debug.Log(Vector3.Distance(transform.position, data.listPNJ.transform.GetChild(i).transform.position));
            if (Vector3.Distance(transform.position, data.listPNJ.transform.GetChild(i).transform.position) < data.acceptancePlayerWithDamage)
            {
                takeDamage = true;
            
            }
        }

        if (takeDamage)
        {
            data.cancer += data.cancerNPCDamanage;
            if (!damageSound)
            {
                damageSound = true;
                switch (Random.Range(0, 4))
                {
                    case 0:
                        michel.Play("TouxF1");
                        break;
                    case 1:
                        michel.Play("TouxF2");
                        break;
                    case 2:
                        michel.Play("TouxH1");
                        break;
                    case 3:
                        michel.Play("TouxH2");
                        break;

                }
            }
        }
        else
        {
            michel.StopPlay("TouxF1");
            michel.StopPlay("TouxF2");
            michel.StopPlay("TouxH1");
            michel.StopPlay("TouxH2");
            damageSound = false;
        }
    }

    void OnDrive()
    {
     
        if (Input.GetKey(Accelerate)||Input.GetKey(Accelerate2))
        {
            if (!isMoveSound)
            {
                michel.Play("BoucleRoue");
                isMoveSound = true;

            }
            if (useRelativeForce)
            {
                rb.AddRelativeForce(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            else
            {
                rb.AddForce(gameObject.transform.forward * moveSpeed);
            }


            ParticleSystem.EmissionModule emission = fx1Z.GetComponent<ParticleSystem>().emission;
            emission.enabled = true;
            ParticleSystem.EmissionModule emission2 = fx2Z.GetComponent<ParticleSystem>().emission;
            emission2.enabled = true;

        }
        else if (Input.GetKeyUp(Accelerate) || Input.GetKeyUp(Accelerate2))
        {

            michel.StopPlay("BoucleRoue");
            isMoveSound = false;
            ParticleSystem.EmissionModule emission = fx1Z.GetComponent<ParticleSystem>().emission;
            emission.enabled = false;
            ParticleSystem.EmissionModule emission2 = fx2Z.GetComponent<ParticleSystem>().emission;
            emission2.enabled = false;
        }
     

        if (Input.GetKey(Decelerate)|| Input.GetKey(Decelerate2))
        {
            if (useRelativeForce)
            {
                rb.AddRelativeForce(-Vector3.forward * moveSpeed* Time.deltaTime);
            }
            else
            {
                rb.AddForce(-gameObject.transform.forward * moveSpeed);
            }
        }

        if (Input.GetKey(SteerLeft)||Input.GetKey(SteerLeft2))
        {
            if (!isDrift)
            {
                michel.Play("Drift");
                isDrift = true;
            }
            if (useRelativeForce)
            {
                rb.AddTorque(Vector3.up * -rotateSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.up * -rotateSpeed);
            }

        }
        else if(Input.GetKeyUp(SteerLeft) || Input.GetKey(SteerLeft2))
        {
            isDrift = false;
            michel.StopPlay("Drift");
        }


        if (Input.GetKey(SteerRight) || Input.GetKey(SteerRight2))
        {
            if (!isDrift)
            {
                michel.Play("Drift");
                isDrift = true;
            }
            if (useRelativeForce)
            {
                rb.AddTorque(Vector3.up * +rotateSpeed * Time.deltaTime);

            }
            else
            {
                transform.Rotate(Vector3.up * +rotateSpeed);
            }

        }
        else if (Input.GetKeyUp(SteerRight) || Input.GetKey(SteerRight2))
        {
            isDrift = false;
            michel.StopPlay("Drift");

        }



        if (Input.GetKeyDown(Boost))
        {
           
            if (data.boost == data.boostMax)
            {
                michel.Play("Boost");
                if (useRelativeForce)
                {
                    rb.AddForce(gameObject.transform.forward * moveSpeed * moveBoost* Time.deltaTime);
                }
                else
                {
                    rb.AddForce(gameObject.transform.forward * moveSpeed * moveBoost);
                }
                FindObjectOfType<AudioManager>().Play("Thrust");
                data.boost = 0.0f;
                isBoosted = true;

            }
        }


        if (isBoosted)
        {

            if (timerBoost > timerFXboost)
            {
                fxVitessBoost.SetActive(false);
                isBoosted = false;
                timerBoost = 0.0f;
            }
            else
            {
                fxVitessBoost.SetActive(true);
                timerBoost += Time.deltaTime;
            }

        }

    }


    void OnLimit()
    {
        if (rb.velocity.x > limSpeedMax)
        {
            rb.velocity = new Vector3(limSpeedMax, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.x < limSpeedMin)
        {
            rb.velocity = new Vector3(limSpeedMin, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z > limSpeedMax)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, limSpeedMax);
        }
        if (rb.velocity.z < limSpeedMin)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, limSpeedMin);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ramassable")
        {
            michel.Play("Objet");

            if (other.gameObject.GetComponent<Items>().isObligatoire)
            {
                for (int i = 0; i < data.canvasList.transform.GetChild(2).childCount; i++)
                {
                    if(data.canvasList.transform.GetChild(2).GetChild(i).gameObject.GetComponent<Text>().text.Contains(other.name))
                    {
                        data.canvasList.transform.GetChild(2).GetChild(i).gameObject.GetComponent<Text>().enabled = false;
                    }
                }

            }
            else
            {
                for (int i = 0; i < data.canvasList.transform.GetChild(3).childCount; i++)
                {
                    if (data.canvasList.transform.GetChild(3).GetChild(i).gameObject.GetComponent<Text>().text.Contains(other.name))
                    {
                        data.canvasList.transform.GetChild(3).GetChild(i).gameObject.GetComponent<Text>().enabled = false;
                    }
                }
            }





            dtn.score += other.GetComponent<Items>().scoreGive;
            for(int i = 0; i < data.myListItems.Count; i++)
            {
                if (data.myListItems[i].name.Contains(other.gameObject.name))
                {

                    data.myListItems[i].GetComponent<Items>().isObligatoire = false;
                }
            }
           

            Destroy(other.gameObject);

        }
    }

}

