using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {
    /* * * Constant Variables * * */
    const string PLAYER_TAG = "Player";
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(PLAYER_TAG))
            StartCoroutine(PlayerController.instance.Spawn());
    }
}
