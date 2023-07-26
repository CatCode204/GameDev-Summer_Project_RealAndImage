using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    /* * * Constant Variables * * */
    const string START_SPAWN = "Spawn";
    /* * * Singleton Variable * * */
    public static PlayerController instance;
    public Vector3 _spawnPosition {set; private get;}

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
    }
    private void Start() {
        OnScenceLoad();
    }

    public void OnScenceLoad() {
        _spawnPosition = GameObject.FindGameObjectWithTag(START_SPAWN).transform.position;
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn() {
        this.GetComponent<TrailRenderer>().enabled = false;
        transform.position = _spawnPosition;
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<TrailRenderer>().enabled = true;
    }
}
