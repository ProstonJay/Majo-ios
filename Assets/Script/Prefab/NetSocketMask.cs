using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetSocketMask : MonoBehaviour {

    public Image pic;

	// Use this for initialization
	void Start () {
        pic.transform.gameObject.SetActive(false);
    }


    public void showUp(bool boo)
    {
        if (boo == true)
        {
            this.transform.gameObject.SetActive(true);
            Invoke("startRorion",1);
        }
        else
        {
            pic.transform.gameObject.SetActive(false);
            CancelInvoke("startRorion");
            value = -1;
            this.transform.gameObject.SetActive(false);
        }
    }

    private void startRorion()
    {
        value = 0;
        pic.transform.gameObject.SetActive(true);
        StartCoroutine(TimeOut());
    }

    private IEnumerator TimeOut()
    {
        yield return new WaitForSeconds(5f);
        GameEvent.DoMsgEvent("服务器连接失败！");
        showUp(false);
    }

    // Update is called once per frame
    private float value;
	void Update () {
        if (value >= 0)
        {
            value += 2.0f;
            if (value > 10)
            {
                value = 0;
            }
            pic.transform.Rotate(new Vector3(0, 0, value));

        }
    }


}
