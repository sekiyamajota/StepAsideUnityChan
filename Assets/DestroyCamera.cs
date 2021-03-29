using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//追加　アイテムにアタッチ
public class DestroyCamera : MonoBehaviour
{
    //カメラのオブジェクト
    private GameObject mycamera;

    void Start()
    {
        //カメラのオブジェクトを取得
        this.mycamera = GameObject.Find("Main Camera");
    }

    void Update()
    {
        if (this.mycamera.transform.position.z > this.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}
