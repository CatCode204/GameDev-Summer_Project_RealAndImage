using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    /* *  * Constant Variables * * */
    const string FADE_IN_ANIM = "Fade_In";
    const string FADE_OUT_ANIM = "Fade_Out";
    public static UIController instance;

    [Header("Screen Transition")]
    [SerializeField] private Animator _animScreenTransition;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
    }

    public IEnumerator FadeIn(float seconds) {
        _animScreenTransition.Play(FADE_IN_ANIM);
        yield return new WaitForSeconds(seconds);
    }

    public IEnumerator FadeOut(float seconds) {
        _animScreenTransition.Play(FADE_OUT_ANIM);
        yield return new WaitForSeconds(seconds);
    }

    public void FadeIn() {
        _animScreenTransition.Play(FADE_IN_ANIM);
        Debug.Log("Fade In");
    }

    public void FadeOut() {
        _animScreenTransition.Play(FADE_OUT_ANIM);
        Debug.Log("Fade Out");
    }
}
