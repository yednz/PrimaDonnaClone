using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AutoJoy : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform joyBG;
    public Joystick joy;
    public Character plSc;
    public bool gameStart = false;
    public Image fillR;
    public Image fillL;
    public camSc cam;





    public void OnPointerDown(PointerEventData ped)
    {
        if (plSc.joyStickActive)
        {
            joyBG.gameObject.SetActive(true);
            Vector2 diff = ped.position - (Vector2)GetComponent<RectTransform>().position;
            Vector2 modifiedDiff = diff * (1f / GetComponentInParent<Canvas>().scaleFactor);
            joyBG.localPosition = modifiedDiff;

            gameStart = true;
            cam.gameStartCam = true;
        }
    }
    public void OnDrag(PointerEventData ped)
    {
        if (plSc.joyStickActive)
        {
            joy.ChangeJoy(ped.position);
            plSc.look();
        }
        
    }
    public void OnPointerUp(PointerEventData ped)
    {
        if (plSc.joyStickActive)
        {
            joyBG.gameObject.SetActive(false);
            joy.ResetJoy();
            plSc.anim.SetBool("walk", false);
            yellStan();
            plSc.scaleNormalize();
        }
        
    }

    public void convertAddFill()
    {
        StartCoroutine(AddFill());
    }
    IEnumerator AddFill()
    {
        yield return new WaitForSeconds(0.001f);

        
        if (fillL.fillAmount >= 0.5f || fillR.fillAmount >= 0.5f)
        {
            StopCoroutine(AddFill());
        }
        else
        {
            fillL.fillAmount = +fillL.fillAmount + 0.002f;
            fillR.fillAmount = +fillR.fillAmount + 0.002f;
        }
    }
    public void yellStan()
    {
        StopCoroutine(AddFill());
        if (fillL.fillAmount >= 0.3f || fillR.fillAmount >= 0.3f)
        {

            cam.aCam = true;
            plSc.yell();
        }else
        if ((fillL.fillAmount >= 0.2f || fillR.fillAmount >= 0.2f) && (fillL.fillAmount < 0.3f || fillR.fillAmount < 0.3f))
        {
            plSc.bigStan();
        }else
        if ((fillL.fillAmount < 0.2f || fillR.fillAmount < 0.2f))
        {
            plSc.stan();
        }
        
        zeroFill();
    }
    public void zeroFill()
    {
        fillL.fillAmount = 0;
        fillR.fillAmount = 0;
    }

    public void destroyFill()
    {
        fillL.gameObject.SetActive(false);
        fillR.gameObject.SetActive(false);
        joyBG.gameObject.SetActive(false);
    }


}
