using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour {

    public Image jj;
    public Image numb1;
    public Image numb2;
    public Image numb3;
    public Image numb4;


    //1.red -1.blu
    public void showNumber(int numb,bool isKeep=false)
    {
        resetNumber();
        int clor = 0;
        if (numb >= 0)
        {
            clor = 1;
        }
        else
        {
            numb =numb * -1;
        }

        char[] strch = numb.ToString().ToCharArray();

        jj.gameObject.SetActive(true);
        if (clor == 1)
        {
            Sprite sp = Resources.Load("Sprite/number/r_+", typeof(Sprite)) as Sprite;
            jj.sprite = sp;
        }
        else
        {
            Sprite sp = Resources.Load("Sprite/number/b_-", typeof(Sprite)) as Sprite;
            jj.sprite = sp;
        }

        for (int i = 0; i < strch.Length; i++)
        {
            if (i == 0)
            {
                numb1.gameObject.SetActive(true);
                if (clor == 1)
                {
                    Sprite sp = Resources.Load("Sprite/number/r_" + strch[i], typeof(Sprite)) as Sprite;
                    numb1.sprite = sp;
                }
                else
                {
                    Sprite sp = Resources.Load("Sprite/number/b_" + strch[i], typeof(Sprite)) as Sprite;
                    numb1.sprite = sp;
                }
            }
            else if (i == 1)
            {
                numb2.gameObject.SetActive(true);
                if (clor == 1)
                {
                    Sprite sp = Resources.Load("Sprite/number/r_" + strch[i], typeof(Sprite)) as Sprite;
                    numb2.sprite = sp;
                }
                else
                {
                    Sprite sp = Resources.Load("Sprite/number/b_" + strch[i], typeof(Sprite)) as Sprite;
                    numb2.sprite = sp;
                }
            }
            else if (i == 2)
            {
                numb3.gameObject.SetActive(true);
                if (clor == 1)
                {
                    Sprite sp = Resources.Load("Sprite/number/r_" + strch[i], typeof(Sprite)) as Sprite;
                    numb3.sprite = sp;
                }
                else
                {
                    Sprite sp = Resources.Load("Sprite/number/b_" + strch[i], typeof(Sprite)) as Sprite;
                    numb3.sprite = sp;
                }
            }
            else if (i == 3)
            {
                numb4.gameObject.SetActive(true);
                if (clor == 1)
                {
                    Sprite sp = Resources.Load("Sprite/number/r_" + strch[i], typeof(Sprite)) as Sprite;
                    numb4.sprite = sp;
                }
                else
                {
                    Sprite sp = Resources.Load("Sprite/number/b_" + strch[i], typeof(Sprite)) as Sprite;
                    numb4.sprite = sp;
                }
            }

        }
        transform.DOPunchPosition(new Vector3(-200, 0, 0), 0.05f);
        if (isKeep == false)
        {
            Invoke("resetNumber", 1.5f);
        }
    }

    private void resetNumber()
    {
        jj.gameObject.SetActive(false);
        numb1.gameObject.SetActive(false);
        numb2.gameObject.SetActive(false);
        numb3.gameObject.SetActive(false);
        numb4.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        //showNumber(9850,true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
