using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    /* * * Singleton Variable * * */
    public static PlayerController instance;
    public Vector3 startPosition {set; get;}

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
        startPosition = transform.position;
    }
}
