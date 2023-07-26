using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    /* * * Constant Variables * * */
    const string PLAYER_TAG = "Player";

    [Header("Info")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _waitDuration;
    [SerializeField] private float _minimumCheckDistance;
    
    [Header("Destination")]
    [SerializeField] private Transform[] _destinationList;

    [Header("Physics")]
    [SerializeField] private GameObject _landSpace;
    [SerializeField] private LayerMask _moveableLayerMask;

    /* * * Processing Variables * * */
    private Vector2[] destinationPos;
    private float waitCounter;

    /* * * Component Variables * * */
    private Rigidbody2D _rb;
    private Collider2D _landSpaceCollider;

    /* * * Control Variables * * */
    private int destinationIndex = 0;

    private void Awake() {
        _rb = this.GetComponent<Rigidbody2D>();
        _landSpaceCollider = _landSpace.GetComponent<Collider2D>();
        destinationPos = new Vector2[_destinationList.Length];
        for (int i=0;i < _destinationList.Length;i++) {
            destinationPos[i] = (Vector2)_destinationList[i].position;
        }
    }

    private void Update() {
        DEBUG();
        if (Vector2.Distance(transform.position,destinationPos[destinationIndex]) <= _minimumCheckDistance) {
            if (waitCounter > 0) waitCounter -= Time.deltaTime;
            else {
                waitCounter = _waitDuration;
                destinationIndex++;
            }
        }
        destinationIndex = (destinationIndex == destinationPos.Length) ? 0 : destinationIndex; 
    }

    private void FixedUpdate() {
        Move();
        AddPhysicOnLandSpace();
    }

    private void DEBUG() {
        Debug.DrawLine(destinationPos[0],destinationPos[destinationPos.Length - 1],Color.red);
        for (int i=1;i < destinationPos.Length;++i)
            Debug.DrawLine(destinationPos[i],destinationPos[i-1],Color.red);
    }

    private void Move() {
        if (Vector2.Distance(transform.position,destinationPos[destinationIndex]) <= _minimumCheckDistance) {
            _rb.velocity = Vector2.zero;
            return;
        }
        Vector2 directionMoveVec = (destinationPos[destinationIndex] - (Vector2)transform.position).normalized;
        _rb.velocity = directionMoveVec * _moveSpeed;
    }

    private void AddPhysicOnLandSpace() {
        Collider2D[] cols = Physics2D.OverlapBoxAll(_landSpaceCollider.bounds.center,_landSpaceCollider.bounds.size,0,_moveableLayerMask);
        foreach(var col in cols) {
            GameObject obj = col.gameObject;
            while (obj.transform.parent != null) obj = obj.transform.parent.gameObject;
            if (obj.CompareTag(PLAYER_TAG)) obj.GetComponent<Movement>().bonusVelocity = _rb.velocity;
        }
    }
}
