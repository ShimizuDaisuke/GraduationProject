//======================================================================================= 
//! @file       Mash.cs
//! @brief      切り取りの範囲を変更する 
//! @author     長尾昌輝
//======================================================================================= 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mash : MonoBehaviour
{
    //消しカスのスコア
    [SerializeField]
    private GameObject Player = default;

    private Player1UPGage Script_Player1UPGage;

    //イメージをいじくるための変数
    private RectTransform imageRectSize;

    // 画像の元の位置
    private Vector2 basePos;

    //大きさ
    private Vector2 baseSize = new Vector2(100.0f, 100.0f);

    // 現在のpポイント
    [SerializeField]
    private int NowP;

    // Start is called before the first frame update
    void Start()
    {
        Script_Player1UPGage = Player.GetComponent<Player1UPGage>();

        // 「RectTransform」というクラスを取得
        imageRectSize = GetComponent<RectTransform>();

        // 画像の位置を取得
        basePos = imageRectSize.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        NowP = Script_Player1UPGage.AccumulateGage;

        // カバーの体力によって、画像の位置をずらす
        imageRectSize.localPosition = new Vector2(basePos.x, (basePos.y - baseSize.y) + (float)NowP);

    }
}
