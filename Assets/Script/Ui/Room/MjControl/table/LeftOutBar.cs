using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftOutBar : MonoBehaviour {

    public List<GameObject> mj = new List<GameObject>();

    private int mjTrueID = 0;

    public void reset()
    {
        if (mj.Count > 0)
        {
            foreach (GameObject item in mj)
            {
                Destroy(item);
            }
        }
        mj.Clear();
        mjTrueID = 0;
    }
    //重连
    public void ReJoinOut(List<int> olist = null)
    {
        if (olist != null && olist.Count > 0)
        {
            for (int i = 0; i < olist.Count; i++)
            {
                int mjid = olist[i];
                chupai(mjid.ToString());
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void chupai(string mjId)
    {
        mjTrueID++;
        GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_left_out_card")) as GameObject;
        mjCard.transform.SetParent(this.transform, false);
        mj.Add(mjCard);
        if (mj.Count <= 6)
        {
            mjCard.transform.localPosition = new Vector3(mj.Count * -7, mjTrueID * -34, 0);
        }
        else if (mj.Count >6&& mj.Count<=12)
        {
            mjCard.transform.localPosition = new Vector3(-61+ (mjTrueID - 6) * -7, (mjTrueID - 6) * -34, 0);
            mjCard.transform.SetSiblingIndex(mjTrueID-7);
        }
        else if (mj.Count >12 && mj.Count <= 18) {
            mjCard.transform.localPosition = new Vector3(-122+ (mjTrueID - 12) * -7, (mjTrueID - 12) * -34, 0);
            mjCard.transform.SetSiblingIndex(mjTrueID-13);
        }
        else if (mj.Count > 18)
        {
            mjCard.transform.localPosition = new Vector3(-183+ (mjTrueID - 18) * -7, (mjTrueID - 18) * -34, 0);
            mjCard.transform.SetSiblingIndex(mjTrueID-19);
        }

        mjCard.GetComponent<Mj_left_out>().setPic(mjId);
        mjCard.name = mjTrueID.ToString();

    }
}
