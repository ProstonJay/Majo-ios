using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailPanel : MonoBehaviour {

    public Button closeBtn;
    public ScrollRect scroView;

    // Use this for initialization
    void Start () {
        closeBtn.onClick.AddListener(PressedClosed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showMail()
    {
        if (GameInfo.Instance.mailList == null|| GameInfo.Instance.mailList.Count==0)
        {
            return;
        }
        for (int i = 0; i < GameInfo.Instance.mailList.Count; i++)
        {
            MailData md = GameInfo.Instance.mailList[i];
            GameObject mailBox = Instantiate(Resources.Load("Prefab/GameObject_mailBox")) as GameObject;
            mailBox.GetComponent<MailBox>().setTxt(md);
            mailBox.transform.SetParent(scroView.GetComponent<ScrollRect>().content, false);
        }
    }

    //关闭
    public void PressedClosed()
    {
        foreach (Transform child in scroView.GetComponent<ScrollRect>().content.transform)
        {

            Destroy(child.gameObject);

        }
        this.transform.gameObject.SetActive(false);
        AudioMgr.Instance.SoundPlay("btnClose", 1f, 2);
    }
}
