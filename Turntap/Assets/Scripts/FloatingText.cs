using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Animator animator;
    private Text damageText;

    // Use this for initialization
    void Awake() {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //Debug.Log(clipInfo.Length);
        //Destruye el objeto apenas acabe la animacion
        Destroy(gameObject, clipInfo[0].clip.length);
        damageText = animator.GetComponent<Text>();
	}
	
	// Update is called once per frame
	public void setText (string text) {
        animator.GetComponent<Text>().text = text;
	}
}
