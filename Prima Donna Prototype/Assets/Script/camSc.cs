using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camSc : MonoBehaviour
{
    //public Animator camAnim;
    public GameObject character;
    private Vector3 camPos;
    public bool gameStartCam = false;
    public bool aCam = false;
    // Start is called before the first frame update
    void Start()
    {
        camPos = transform.position - character.transform.position;
       // camAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        transform.position = character.transform.position + camPos;
        if (gameStartCam)
        {
            StartCoroutine(startCam());
        }
        if (aCam)
        {
            StartCoroutine(awayCam());
        }
    }

    IEnumerator startCam()
    {
        yield return new WaitForSeconds(0.01f);
        if (transform.position.y < 11)
        {
            transform.position += new Vector3(0, 0.2f, 0);
            camPos = transform.position - character.transform.position;
        }
        else
        {
            gameStartCam = false;
            StopCoroutine(startCam());
        }
    }

    IEnumerator awayCam()
    {


        yield return new WaitForSeconds(0.1f);
        if (transform.position.y <= 13)
        {
            transform.position += new Vector3(0, 0.12f, -0.03f);
            camPos = transform.position - character.transform.position;
        }
        else
        {
            aCam = false;
            StartCoroutine(closeAwayCam());
            StopCoroutine(awayCam());
        }

    }

    IEnumerator closeAwayCam()
    {
        yield return new WaitForSeconds(0.3f);
        if (transform.position.y >= 11)
        {
            transform.position -= new Vector3(0, 0.2f, -0.1f);
            camPos = transform.position - character.transform.position;
        }
        else
        {
            StopCoroutine(closeAwayCam());
        }

    }
}
