using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenceController : MonoBehaviour {
    public static ScenceController instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
    }

    private IEnumerator Next_Level_IENum() {
        UIController.instance.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerController.instance.OnScenceLoad();
        UIController.instance.FadeOut();
    }
    
    public void NextLevel() {
        StartCoroutine(Next_Level_IENum());
    }
}
