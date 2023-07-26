using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EndPoint : MonoBehaviour {
    /* * * Constant Variables * * */
    const string PLAYER_TAG = "Player";
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == PLAYER_TAG)
            ScenceController.instance.NextLevel();
    }
}
