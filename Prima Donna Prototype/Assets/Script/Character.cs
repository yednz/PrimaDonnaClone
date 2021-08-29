using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Joystick joy;
    public AutoJoy aJoy;
    public GameObject player;
    private Vector3 movement;
    public Animator anim;
    public bool joyStickActive = true;
    public GameObject stanPartical;
    public GameObject bigStanPartical;
    public GameObject yellPartical;
    public GameObject[] ragdooll;
    private boyScript boySc;
    public Image FillL, FillR;

    public GameObject stanNote, bigStanNote, yellNote;
    public GameObject clone;
    public GameObject sizeUp;

    public GameObject final;
    public GameObject finalLine;
    public float finalSpeed;
    public GameObject TryAgain;
    public GameObject FinalPanel;

    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        boySc = GameObject.FindGameObjectWithTag("enemy").GetComponentInParent<boyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (joyStickActive)
        {
            movement = new Vector3(joy.result.x, 0, joy.result.y) * Time.deltaTime * 5f;
            transform.Translate(movement);
            if (movement.x != 0 || movement.z != 0)
            {
                aJoy.convertAddFill();
            }
            else
            {
                aJoy.zeroFill();//Bugfix*
            }
        }
        
    }

    public void look()
    {
        var lookPos = new Vector3(joy.result.x, 0, joy.result.y);
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        player.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f);

        anim.SetBool("walk", true);
    }
    public void yell()
    {
        anim.SetBool("yell", true);
        joyStickActive = false;
        yellPartical.SetActive(true);
        StartCoroutine(stopYell());
        Instantiate(yellNote, yellPartical.transform.position,yellPartical.transform.rotation,clone.transform);
    }
    IEnumerator stopYell()
    {
        yield return new WaitForSeconds(0.57f);
        anim.SetBool("yell", false);
        joyStickActive = true;
        StopCoroutine(stopYell());
        StartCoroutine(afterYellStan());

    }


    public void stan()
    {
        anim.SetBool("stading", true);
        joyStickActive = false;
        stanPartical.SetActive(true);
        StartCoroutine(stopStan());
        Instantiate(stanNote, stanPartical.transform.position, stanPartical.transform.rotation, clone.transform);
    }
    IEnumerator stopStan()
    {

        yield return new WaitForSeconds(0.49f);
        anim.SetBool("stading", false);
        joyStickActive = true;
        StopCoroutine(stopStan());
        StartCoroutine(afterYellStan());

    }

    public void bigStan()
    {
        anim.SetBool("stading", true);
        joyStickActive = false;
        bigStanPartical.SetActive(true);
        StartCoroutine(stopBigStan());
        Instantiate(bigStanNote, bigStanPartical.transform.position, stanPartical.transform.rotation, clone.transform);
    }
    IEnumerator stopBigStan()
    {

        yield return new WaitForSeconds(0.49f);
        anim.SetBool("stading", false);
        joyStickActive = true;
        StopCoroutine(stopBigStan());
        StartCoroutine(afterYellStan());
    }
    IEnumerator afterYellStan()
    {
        yield return new WaitForSeconds(0.5f);
        bigStanPartical.SetActive(false);
        stanPartical.SetActive(false);
        yellPartical.SetActive(false);

    }

    public void ragdollActive(bool state)
    {
        anim.enabled = false;
        foreach (var rb in ragdooll)
        {
            rb.GetComponent<Rigidbody>().isKinematic = !state;//false
            rb.GetComponent<Collider>().enabled = state;//true
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "enemy")//deathGirl
        {
            col.gameObject.GetComponentInParent<boyScript>().attack();
            joyStickActive = false;
            aJoy.destroyFill();
            ragdollActive(true);
            TryAgain.SetActive(true);
        }
        if (col.tag == "line")//deathGirl
        {
            joyStickActive = false;
            aJoy.destroyFill();
            ragdollActive(true);
            aJoy.gameStart = false;
            boySc.boyAnim.SetBool("zombieWalk", false);
            TryAgain.SetActive(true);

        }

        if (col.tag == "bait")
        {
            col.gameObject.SetActive(false);
            FillL.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            FillR.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            stanPartical.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            bigStanPartical.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            yellPartical.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

            sizeUp.SetActive(true);
            StartCoroutine(afterBait());
        }
        if (col.tag == "finalLine")
        {
            aJoy.destroyFill();
            yellPartical.transform.localScale = new Vector3(4, 4, 4);
            yellPartical.transform.GetChild(0).gameObject.transform.localScale = new Vector3(4, 4, 4);
            joyStickActive = false;
            StartCoroutine(finalLap());
        }
    }
    
    public void scaleNormalize()
    {
        StartCoroutine(normalized());
    }
    IEnumerator normalized()
    {
        yield return new WaitForSeconds(0.5f);
        FillL.transform.localScale = new Vector3(1, 1, 1);
        FillR.transform.localScale = new Vector3(1, 1, 1);
        stanPartical.transform.localScale = new Vector3(1, 1, 1);
        bigStanPartical.transform.localScale = new Vector3(1, 1, 1);
        yellPartical.transform.localScale = new Vector3(1, 1, 1);
        StopCoroutine(normalized());
    }
    IEnumerator afterBait()
    {
        yield return new WaitForSeconds(0.2f);
        sizeUp.SetActive(false);
        StopCoroutine(afterBait());
    }


    IEnumerator finalLap()
    {
        yield return new WaitForSeconds(0.01f);
        if (transform.position.z != final.transform.position.z)
        {
            transform.position = Vector3.MoveTowards(transform.position,final.transform.position, finalSpeed * Time.deltaTime);

            StartCoroutine(finalLap());
        }
        else
        {
            anim.SetBool("yell", true);
            joyStickActive = false;
            yellPartical.SetActive(true);
            Instantiate(yellNote, yellPartical.transform.position, yellPartical.transform.rotation, clone.transform);
            StartCoroutine(finalStopYell());


            aJoy.gameStart = false;

            StopCoroutine(finalLap());
           
        }
    }
    IEnumerator finalStopYell()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("yell", false);
        anim.SetBool("walk", false);
        FinalPanel.SetActive(true);

        StopCoroutine(finalStopYell());
        StartCoroutine(afterYellStan());
    }



}
