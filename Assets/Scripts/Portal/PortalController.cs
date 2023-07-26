using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PortalController : MonoBehaviour {
    /* * * Constant Variables * * */
    const string PLAYER_TAG = "Player";
    const string PLAYER_ANCHOR_TAG = "Anchor";
    const string PORTAL_IN_TAG = "Portal In";

    [Header("Info")]
    [SerializeField] private GameObject _destinationPortal;
    [SerializeField] private float _radius;
    [SerializeField] private float _pullForce;
    [SerializeField] private float _pullTime;

    private Vector3 destinationPos;

    private void Awake() {
        destinationPos = _destinationPortal.transform.position;
    }

    public void OnUse(InputAction.CallbackContext value) {
        if (value.canceled) return;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position,_radius);
        foreach (var col in cols)
            if (col.gameObject.CompareTag(PLAYER_TAG)) StartCoroutine(OnPlayerPortalIn(col.gameObject));
    }

    private IEnumerator OnPlayerPortalIn(GameObject playerObj) {
        Rigidbody2D playerRb = playerObj.GetComponent<Rigidbody2D>();
        GameObject anchorChild = playerObj.transform.Find(PLAYER_ANCHOR_TAG).gameObject;
        Animator playerAnim = anchorChild.GetComponent<Animator>();
        TrailRenderer playerTailRen = playerObj.GetComponent<TrailRenderer>();
        PlayerController.instance.DisablePlayerInput();
        playerRb.velocity = Vector2.zero;
        playerTailRen.enabled = false;
        StartCoroutine(MoveToCenter(playerObj));
        playerRb.simulated = false;
        playerAnim.Play(PORTAL_IN_TAG);
        yield return new WaitForSeconds(1);
        playerObj.transform.position = destinationPos;
        yield return new WaitForSeconds(1);
        playerRb.simulated = true;
        playerTailRen.enabled = true;
        PlayerController.instance.EnablePlayerInput();
    } 

    private IEnumerator MoveToCenter(GameObject playerObj) {
        float counter = _pullTime;
        Transform playerTrans = playerObj.GetComponent<Transform>();
        while (counter > 0) {
            Vector2 directionForce = (Vector2)(transform.position - playerObj.transform.position);
            directionForce = directionForce.normalized;
            playerTrans.Translate(directionForce * _pullForce * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            counter -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,_radius);
    }
}
