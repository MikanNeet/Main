using UnityEngine;
using System.Collections;

/**
 * MgsTextクラスです。
 * テキスト
 */
public class MsgText {

    // テキスト
    private string text;

    // 選択肢かどうか
    private bool yesflg;

    /**
     * コンストラクタです。
     * 
     */
    public MsgText(string str, bool flg)
    {
        text = str;
        yesflg = flg;
    }

    // メッセージ取得
    public string getMessage()
    {
        return text;
    }

    // 選択肢かどうかを取得
    public bool getSelect()
    {
        return yesflg;
    }
}
