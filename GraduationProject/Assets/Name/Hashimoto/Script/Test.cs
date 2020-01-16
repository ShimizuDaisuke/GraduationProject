using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// 
///   テ   ス   ト
/// 
/// 
/// 
/// </summary>
public class Test : MonoBehaviour
{
    // カメラの監督
    [SerializeField]
    private GameObject CameraDirectorObj;

    // プレイヤー
    private GameObject PlayerObj;

    // レイの長さ
    private float  Length= 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        PlayerObj = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        int flag = 0;

        // レイをプレイヤーを中心にZ向きに前後2つ作成する
        Ray[] ray_playerup = { new Ray(PlayerObj.transform.position, Vector3.forward),       // Z軸に沿って前向き
                                   new Ray(PlayerObj.transform.position, Vector3.back) };   // Z軸に沿って後向き

        // プレイヤーから飛ばしたレイに当たったオブジェクトの入れ物
        RaycastHit hit;

        // Z軸に沿って前後にあるレイ
        foreach (Ray ray in ray_playerup)
        {
            // レイの可視化
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * Length, Color.red);

            if (CameraDirectorObj.GetComponent<CameraDirector>().IsMove2D3DCameraPos)
            {
                // プレイヤーからレイを飛ばして何からのオブジェクトに衝突した場合
                if (Physics.Raycast(ray, out hit, Length))
                {
                    // 当たったもの
                    //Debug.Log("当たったもの: " + hit.transform.gameObject.name);

                    // 当たったオブジェクトが  カメラ2Dの時にプレイヤーが入ってはいけない領域の場合
                    if ((hit.transform.gameObject.tag == "Camera2DNoArea"))
                    {
                        Debug.Log(PlayerObj.name + "は " + hit.transform.gameObject.name + "に当たった");

                        flag = 0;
                        // プレイヤーが入ってはいけない領域に当たった
                        return;
                    }
                    else
                    {
                        flag = 1;
                    }

                }
            }

        }

        //if(flag == 1) Debug.Log("当たっていない");

    }
}
