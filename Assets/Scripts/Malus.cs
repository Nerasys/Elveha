using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malus : MonoBehaviour
{
    // Start is called before the first frame update
    public enum TypeMalus
    {
        flaque

    };

    public TypeMalus type;

    public float limitMinFlaque = -30.0f;
    public float limitMaxFlaque = -30.0f;

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           
            if (type == TypeMalus.flaque)
            {
                other.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 360, 0);
              
            }

        }
    }

}
