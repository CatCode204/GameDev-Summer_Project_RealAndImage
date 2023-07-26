using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    /* *  * Constant Variables * * */
    const string FADE_IN_ANIM = "FadeIn";
    const string FADE_OUT_ANIM = "FadeOut";
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
        _animScreenTransition.SetTrigger(FADE_IN_ANIM);
        yield return new WaitForSeconds(seconds);
    }

    public IEnumerator FadeOut(float seconds) {
        _animScreenTransition.SetTrigger(FADE_OUT_ANIM);
        yield return new WaitForSeconds(seconds);
    }
}
