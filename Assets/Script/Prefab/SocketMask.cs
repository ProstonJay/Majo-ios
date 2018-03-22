using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SocketMask : MonoBehaviour {


    private Image loadimg;

    Transform root;

    // Use this for initialization
    void Start () {
        //获得脚本挂的Transform
        root = this.transform;
        //获取场景中按钮的引用
        loadimg = root.Find("Image_lod").GetComponent<Image>();
        Tweener tweener = loadimg.rectTransform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360);//旋转动画    
        tweener.SetLoops(-1);
        //设置移动类型
        tweener.SetEase(Ease.Linear);
        tweener.onComplete = delegate () {
            Debug.Log("移动完毕事件");
        };
       // loadimg.material.DOFade(0, 1f).onComplete = delegate () {
           // Debug.Log("褪色完毕事件");
        //};
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
