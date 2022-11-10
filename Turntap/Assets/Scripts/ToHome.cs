using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToHome : MonoBehaviour {

	public void toHome()
    {
        SceneManager.LoadScene("first");
    }
}
