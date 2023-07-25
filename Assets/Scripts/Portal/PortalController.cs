using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PortalController : MonoBehaviour {
    /* * * Constant Variables * * */
    const string PLAYER_TAG = "Player";

    [Header("Info")]
    [SerializeField] private GameObject _destinationPortal;
    [SerializeField] private float _radius;

    private Vector3 destinationPos;

    private void Awake() {
        destinationPos = _destinationPortal.transform.position;
    }

    public void OnUse(InputAction.CallbackContext value) {
        Debug.Log("OK");
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,_radius);
    }
}
