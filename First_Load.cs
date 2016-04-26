using UnityEngine;
using System.Collections;

public class First_Load : MonoBehaviour {
    void Awake() {
        Screen.fullScreen = false;

    }
    void Start() {

        
    }

    public void Sta(){
        Application.LoadLevel("Main");
    }
}
