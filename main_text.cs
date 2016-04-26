using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class main_text : MonoBehaviour {

    public Text money;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        money.text = "所持金:"+GameObject.Find("money").GetComponent<Umr_Coin>().getMoney().ToString()+"円";
	}
}
