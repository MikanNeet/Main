using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/**
 * メッセージの動作確認用のクラスです。
 * 
 */
public class MessageTest : MonoBehaviour {

    // メッセージ
    public GameObject msg;

	void Start () {
	
	}
	
	void Update () {
        // メッセージ継続中はreturn
        if (msg == null) return;
        if (!msg.GetComponent<Message>().getEndFlg()) return;

        // メッセージ終了後
        Debug.Log("最後の文章は:" + msg.GetComponent<Message>().getLastMessage());

        Destroy(msg.gameObject);
	}
}
