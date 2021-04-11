using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void Start()
    {
        //回転を開始する角度を設定
        //すべてのコインの回転が揃わないように、
        //Random.Range関数を使って回転を開始する角度をランダムに設定している
        this.transform.Rotate(0, Random.Range(0, 360), 0);
    }

    void Update()
    {
        //回転
        //Rotate関数を使ってY軸を中心にコインが回転し続けるようにしている
        this.transform.Rotate(0, 3, 0);

        Debug.Log("kk");
    }
}