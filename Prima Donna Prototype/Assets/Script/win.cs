using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class win : MonoBehaviour
{
    public Animator anim;
    public GameObject[] ragdoll;
    public GameObject hips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnParticleCollision(GameObject other)//boy Death
    {
        ragdollActive(true);
        Debug.Log("winwin");
        hips.GetComponent<Rigidbody>().AddForce(-gameObject.transform.forward * 5000f);
    }
    public void ragdollActive(bool state)
    {
        anim.enabled = false;
        foreach (var rb in ragdoll)
        {
            rb.GetComponent<Rigidbody>().isKinematic = !state;//false
            rb.GetComponent<Collider>().enabled = state;//true
        }
    }
}
