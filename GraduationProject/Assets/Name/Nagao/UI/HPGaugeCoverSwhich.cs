using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPGaugeCoverSwhich : MonoBehaviour
{
    //プレイヤーの変数
    private GameObject player;

    // プレイヤーのスクリプト
    private PlayerHit Script_PlayerHit;

    //カバーのUI
    [SerializeField]
    private GameObject coverUI = default;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーを探す
        player = GameObject.FindGameObjectWithTag("Player");

        // プレイヤーのスクリプトを取得
        Script_PlayerHit = player.GetComponent<PlayerHit>();
    }

    // Update is called once per frame
    void Update()
    {
        //Playerがカバーをしている時
        if (Script_PlayerHit.FixCover == true)
        {
            coverUI.SetActive(true);
        }
        else
        {
            coverUI.SetActive(false);
        }
    }
}
