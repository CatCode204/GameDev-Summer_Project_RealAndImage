using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    /* * * Constant Variables * * */
    const string START_SPAWN = "Spawn";
    /* * * Singleton Variable * * */
    public static PlayerController instance;
    
    private PlayerInput _playerInput;
    public Vector3 _spawnPosition {set; private get;}
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
        _playerInput = this.GetComponent<PlayerInput>();
    }
    private void Start() {
        OnScenceLoad();
    }

    public void OnScenceLoad() {
        DisablePlayerInput();
        _spawnPosition = GameObject.FindGameObjectWithTag(START_SPAWN).transform.position;
        transform.position = _spawnPosition;
        EnablePlayerInput();
    }

    public void EnablePlayerInput() {
        _playerInput.enabled = true;
    }

    public void DisablePlayerInput() {
        _playerInput.enabled = false;
    }

    public void Spawn() {
        DisablePlayerInput();
        UIController.instance.FadeIn(1);
        transform.position = _spawnPosition;
        UIController.instance.FadeOut(1);
        EnablePlayerInput();
    }
}
