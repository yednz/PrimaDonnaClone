using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeGlass : MonoBehaviour
{
    public GameObject line;

    
    private void OnParticleCollision(GameObject other)//boy Death
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        
        for (int i = 1; i < 16; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
        }
        line.GetComponent<BoxCollider>().enabled = true;
    }
}
