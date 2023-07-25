using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [Header("Setting")]
    [SerializeField] private Transform playerTrans;
    [Range(0,1)] [SerializeField] private float _smoothTime;
    [SerializeField] private Vector3 _positionOffSet;

    [Header("Limitation")]
    [SerializeField] private Vector2 _minLimit;
    [SerializeField] private Vector2 _maxLimit;

    [Header("Zoom")]
    [Range(0,1)] [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _zoomSize;
    /* * * Control Variables * * */
    private Vector3 velVec = Vector3.zero;

    /* * * Component Variables * * */
    private Camera _cam;

    private void Awake() {
        _cam = Camera.main;
    }

    private void LateUpdate() {
        EditZoom();
        Move();
    }

    private void Move() {
        Vector3 targetPosition = playerTrans.position + _positionOffSet;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x,_minLimit.x,_maxLimit.x),Mathf.Clamp(targetPosition.y,_minLimit.y,_maxLimit.y),targetPosition.z);
        transform.position = Vector3.SmoothDamp(transform.position,targetPosition,ref velVec,_smoothTime);
    }
    private void EditZoom() {
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize,_zoomSize,_zoomSpeed);
    }
}
