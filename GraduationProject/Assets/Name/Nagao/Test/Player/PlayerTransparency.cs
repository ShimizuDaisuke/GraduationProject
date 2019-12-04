using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransparency : MonoBehaviour
{

    // プレイヤーの監督
    [SerializeField]
    private GameObject PlayerDirector = default;
    // スクリプト：Playerを点滅させる処理
    private PlayerFlashing Script_PlayerFlashing;

    // Start is called before the first frame update
    void Start()
    {
        Script_PlayerFlashing = PlayerDirector.GetComponent<PlayerFlashing>();

        //プレイヤーの色を決める RGB+α
        //GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが刃物に触れたなら
        if (Script_PlayerFlashing.IsPlayerFlashing == true)
        {
            //float sin = Mathf.Sin(Time.deltaTime);
            float T = 0.3f;
            float f = 1.0f / T; 
            float sin = Mathf.Sin(2 * Mathf.PI * f * Time.time);

            //α値の変更
            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, sin);
        }
        else
        {
            //α値のリセット
            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

       
    }
}
