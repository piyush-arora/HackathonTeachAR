using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(loadARMode());

        		
	}

    IEnumerator loadARMode()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("FBScene");

    }
    

}
