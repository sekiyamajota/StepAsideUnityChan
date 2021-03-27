using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWallManager : MonoBehaviour
{
    //Unityちゃんのオブジェクト
    private GameObject unitychan;
    //DestroyWallManagerとカメラの距離
    private float difference;

    // Use this for initialization
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
        //DestroyWallManagerとカメラの位置（z座標）の差を求める
        this.difference = this.unitychan.transform.position.z - this.transform.position.z;
    }

    void Update()
    {
        //Unityちゃんの位置に合わせてDestroyWallManagerの位置を移動
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);
    }

    //トリガーモードで他のオブジェクトと接触した場合の処理
    private void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag" || other.gameObject.tag == "CoinTag")
        {
            //DestroyWallに接触したオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }
}

