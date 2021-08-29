using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boyScript : MonoBehaviour
{
    public Transform target;
    public float speed = 1;
    public AutoJoy joyStickSc;
    public Animator boyAnim;
    public GameObject[] ragdoll;
    private bool deathBoy = false;
    public GameObject handL, handR;
    public GameObject hearthPartical;
    public GameObject hips;
    // Start is called before the first frame update
    void Start()
    {
        boyAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (joyStickSc.gameStart == true && deathBoy == false)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
            boyAnim.SetBool("zombieWalk", true);
            transform.LookAt(target);
        }
    }

    public void ragdollActive(bool state)
    {
        boyAnim.enabled = false;
        foreach (var rb in ragdoll)
        {
            rb.GetComponent<Rigidbody>().isKinematic = !state;//false
            rb.GetComponent<Collider>().enabled = state;//true
        }
    }

    private void OnParticleCollision(GameObject other)//boy Death
    {
        ragdollActive(true);
        deathBoy = true;
        handL.tag = "Untagged";
        handR.tag = "Untagged";
        hearthPartical.SetActive(false);
        hips.GetComponent<Rigidbody>().AddForce(-gameObject.transform.forward*5000f);
    }

    public void attack()
    {
        //Vector3 dir = target.position - transform.position;
        //dir = dir.normalized;
        //GetComponent<Rigidbody>().AddForce(dir * 15f);
        ragdollActive(true);
    }
}
   
