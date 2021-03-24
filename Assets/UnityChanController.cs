using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;
    //Unityちゃんを移動させるコンポーネントを入れる
    private Rigidbody myRigidbody;
    //前方向の速度
    private float velocityZ = 16f;
    //横方向の速度
    private float velocityX = 10f;
    //上方向の速度
    private float velocityY = 10f;
    //左右の移動できる範囲
    private float movableRange = 3.4f;

    void Start()
    {
        //Animatorコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        //AnimatorクラスのSetFloat関数は、第一引数に与えられたパラメータに、第二引数の値を代入する関数
        //また、第一引数のバラメータがアニメーション再生の条件に使われている
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();
    }


    //ユニティちゃんが道路をはみ出してしまわないように、左右の移動できる範囲をif文で設定している
    void Update()
    {
        //横方向の入力による速度(入力がない場合は0で動かないように保つ)
        float inputVelocityX = 0;
        //上方向の入力による速度
        float inputVelocityY = 0;

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
        if (Input.GetKey(KeyCode.LeftArrow) && -this.movableRange < this.transform.position.x)
        {
            //左方向への速度を代入
            inputVelocityX = -this.velocityX;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.movableRange)
        {
            //右方向への速度を代入
            inputVelocityX = this.velocityX;
        }

        //ジャンプしていない時にスペースが押されたらジャンプする
        //多段ジャンプを防ぐため、地面付近にいる時にだけスペースでジャンプするようにしている
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生
            //AnimatorクラスのSetBool関数は、第一引数に第二引数の値を代入する関数
            //また、第一引数のパラメータがアニメーション再生の条件に使われている
            this.myAnimator.SetBool("Jump", true);
            //上方向への速度を代入
            inputVelocityY = this.velocityY;
        }
        else
        {
            //現在のY軸の速度を代入
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //Jumpステート(Jump中)は何度もアニメーションしない様にJumpにfalseをセットする
        //AnimatorクラスのGetCurrentAnimatorStateInfo(0)で現在のアニメーションの状態を取得し、
        //「IsName」関数で取得したステートの名前が引数の文字列と一致しているかどうかを調べる
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //Unityちゃんに速度を与える
        //Rigidbodyクラスのvelocityは、Vector3型の変数で、物体の持つ速度を表す
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, this.velocityZ);
    }
}
