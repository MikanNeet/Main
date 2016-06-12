using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Message : MonoBehaviour {

    // カウンタ
    private int cnt = 0;

    // 表示されている文字数
    private int charaNum = 1;

    // 文章の階層
    private int hieNum = 0;

    // 表示する文章の番号
    private int sentNum = 0;

    // 一番最後に表示した文章
    private string lastSent;

    // 終了したかどうかのフラグ
    private bool endFlg = false;

    // はいボタンのオブジェクト
    GameObject yesObj;

    // いいえボタンのオブジェクト
    GameObject noObj;

    // メッセージのテキストオブジェクト
    GameObject textObj;

    // テキスト
    List<List<MsgText>> text = new List<List<MsgText>>();

    void Start () {

        // 文章の設定
        text.Add(new List<MsgText>());
        text.Add(new List<MsgText>());
        text.Add(new List<MsgText>());
        text.Add(new List<MsgText>());


        text[0].Add(new MsgText("こんにちは", false));
        text[0].Add(new MsgText("今日暇？", true));
        text[0].Add(new MsgText("そうかい", false));
        text[1].Add(new MsgText("あそぼ", true));
        text[1].Add(new MsgText("うまるダイブ！", false));
        text[2].Add(new MsgText("いえーい", false));

        textObj = transform.FindChild("Message").gameObject;

        // テキストオブジェクト取得
        textObj = textObj.transform.Find("MsgText").gameObject;

        // はいボタン取得
        yesObj = transform.FindChild("YesButton").gameObject;

        // いいえボタン取得
        noObj = transform.FindChild("NoButton").gameObject;

        // カウンタ
        cnt = 0;

        // 表示されている文字数
        charaNum = 1;

        // 表示する文章の番号
        sentNum = 0;

        // 文章の階層
        hieNum = 0;

        // はい、いいえボタンの表示、非表示
        yesObj.SetActive(text[hieNum][sentNum].getSelect());
        noObj.SetActive(text[hieNum][sentNum].getSelect());

    }

	/**
     * メソッドです。
     * 更新です。
     */
	void Update () {

        // 終了していたらreturn
        if (endFlg) return;

        // 文字の表示
        if (cnt % 4 == 0)
        {
            if (charaNum <= text[hieNum][sentNum].getMessage().Length)
            {
                // 文字の更新
                textObj.GetComponent<Text>().text = text[hieNum][sentNum].getMessage().Substring(0, charaNum);

                lastSent = hieNum.ToString() + "-" + sentNum.ToString() + ":" + text[hieNum][sentNum].getMessage();
            }
            charaNum++;
        }

        // カウントアップ
        cnt++;
	}

    /**
     * メソッドです。
     * メッセージを押したときに実行されます。
     */
    private void msgPush()
    {
        // 次の文章の表示
        sentenseUpdate();
    }

    /**
    * メソッドです。
    * はいボタンを押したときに実行されます。
    */
    private void yesPush()
    {
        // 次の階層へ
        hieNum++;

        // 文章の更新
        sentNum = 0;

        // 表示される文字数を1に
        charaNum = 1;

        // はい、いいえボタンの表示、非表示
        yesObj.SetActive(text[hieNum][sentNum].getSelect());
        noObj.SetActive(text[hieNum][sentNum].getSelect());
    }


    /**
     * メソッドです。
     * 文章を次に更新します。
     */
    private void sentenseUpdate()
    {
        // 文章の更新
        sentNum++;

        // メッセージが終わったら
        if (sentNum >= text[hieNum].Count)
        {
            // 最後のメッセージ表示
            //Debug.Log(lastSent);

            // 終了
            endFlg = true;
            return;
        }

        // 表示される文字数を1に
        charaNum = 1;

        // はい、いいえボタンの表示、非表示
        yesObj.SetActive(text[hieNum][sentNum].getSelect());
        noObj.SetActive(text[hieNum][sentNum].getSelect());
    }

    /**
     * メソッドです。
     * このメッセージが終了しているか判定します。
     */
    public bool getEndFlg()
    {
        return endFlg;
    }

    /**
     * メソッドです。
     * 最後に表示された文章を表示します。
     */
    public string getLastMessage()
    {
        return lastSent;
    }

}
