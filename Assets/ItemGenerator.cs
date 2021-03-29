using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スクリプトでアイテムを生成するためには、何のPrefabからインスタンスを作り、
//それをどこに配置するのかを記述する
public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //発展追加
    private int positionZ;
    private GameObject unitychan;

    /*!!
    アイテムはすべてStart関数で生成している。z方向（画面奥方向）に15mずつスペースをあけてアイテムを
    生成するが、このままではアイテムが整列してしまい不自然なので、
    z方向へランダムに配置されるようにRandom.Range関数を使ってz方向の位置を調節している

    生成するアイテムの種類もRandom.Range関数を使って決めている
    乱数はアイテムの位置をランダムにするだけでなく、アイテムの種類を確率で決めるときにも使える
    */
    void Start()
    {
        //発展追加
        //Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
        this.positionZ = startPos;
        ItemGenerate(this.positionZ);
    }

    //発展追加
    void Update()
    {
        if (this.positionZ < this.unitychan.transform.position.z + 50)
        {
            this.positionZ += 5;
            ItemGenerate(this.positionZ);
        }
    }

    //発展追加
    int ItemGenerate(int startpositionZ)
    {
        this.positionZ = startpositionZ + 25;

        if (this.positionZ > goalPos)
        {
            this.positionZ = goalPos;
        }

        //一定の距離ごとにアイテムを生成
        for (int i = startpositionZ; i < this.positionZ; i += 15)
        {
            //まずどのアイテムを出すのかをランダムに設定(1~10)
            //Randomクラスの「Range」関数は、第一引数以上、第二引数未満の整数をランダムに返す
            int num = Random.Range(1, 11);
            //20%
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成(Prefabからインスタンスを生成)
                //20%の確率でこの処理を繰り返す(-1~1)
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    //「Instantiate () 」は、()内に指定したPrefabのインスタンスをGameObject型として生成
                    //また生成したインスタンスは、GameObject型の変数に代入する
                    GameObject cone = Instantiate(conePrefab);
                    //コーンの位置x(-4~4)、y(高さはそのまま)、z(80~350のランダム)
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                }
            }
            else
            {
                //80%
                //レーンごとにアイテムを生成(-1~1の3レーン)
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める(1~10)
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定(-5~5)
                    int offsetZ = Random.Range(-5, 6);


                    //80%でこの処理を繰り返す
                    if (1 <= item && item <= 6)　//(1~6)60%コイン配置
                    {
                        //コインを生成(Prefabからインスタンスを生成)
                        //コインのインスタンスを作るため、coinPrefab変数を指定している
                        GameObject coin = Instantiate(coinPrefab);

                        //コインの位置x(-3.4or0or3.4)、y(高さはそのまま)、z(80~350のランダム+-5~5)(一直線にさせないため)
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9) //(7~9)30 % 車配置
                    {
                        //車を生成(Prefabからインスタンスを生成)
                        GameObject car = Instantiate(carPrefab);

                        //車の位置x(-3.4or0or3.4)、y(高さはそのまま)、z(80~350のランダム+-5~5)(一直線にさせないため)
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                    }
                    //10%何もなし(10)
                }
            }
        }

        return this.positionZ;
    }
}
