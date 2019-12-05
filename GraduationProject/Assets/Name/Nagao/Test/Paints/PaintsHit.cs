using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要


public class PaintsHit : MonoBehaviour
{
    // スクリプト :Playerとの当たり判定
    private PlayerHit Script_PlayerHit;

    private float red, green, blue, alfa;   //パネルの色、不透明度を管理

    //色　0～255用
    [SerializeField]
    private float m_red, m_green, m_blue, m_alfa = 0.0f;

    //UIの表示時間
    [SerializeField]
    private float m_maxTime = 0.0f;


    private float m_time = 0.0f;

    Image fadeImage;                //透明度を変更するパネルのイメージ

    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        Script_PlayerHit = Player.GetComponent<PlayerHit>();

        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;        //赤
        green = fadeImage.color.g;      //緑
        blue = fadeImage.color.b;       //青
        alfa = fadeImage.color.a;       //α値
    }

    // Update is called once per frame
    void Update()
    {
        SetAlpha();

        //プレイヤーと絵の具が当たったら
        if (Script_PlayerHit.HitPaints == true)
        {
            //色を決めた値にする
            //red = m_red / 255.0f;           //赤
            //green = m_green / 255.0f;       //緑
            //blue = m_blue / 255.0f;         //青

            //α値の減少
            alfa = (m_alfa / 255.0f) - (m_time / m_maxTime);

            //無敵時間の最大値よりも小さい時
            if (m_maxTime > m_time)
            {
                //無敵時間のカウントアップ
                m_time += Time.deltaTime;
            }
            else
            {
                //UI解除
                Script_PlayerHit.HitPaints = false;

                //無敵時間のリセット
                m_time = 0.0f;
            }
        }
        else
        {
            //α値をリセット
            alfa = 0.0f;
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
