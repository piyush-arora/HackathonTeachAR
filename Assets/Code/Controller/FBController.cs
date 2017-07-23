using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class FBController : MonoBehaviour
{

    List<string> permissions = new List<string>() { "public_profile", "email" };
    //public static LoginResponse loginResponse;
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
            FB.LogInWithReadPermissions(permissions, AuthCallback);
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
            
        }
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            //User.userId = aToken.UserId; 
            Debug.Log("TOKEN USERID " + aToken.UserId);
            FetchFBProfile();
            //FetchFBProfilePic();

        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
 
    IEnumerator LoadARModeScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("ARMode");
    }

    private void FetchProfileCallback(IGraphResult result)
    {

        Debug.Log(result.RawResult);

        Dictionary<string, object> FBUserDetails = (Dictionary<string, object>)result.ResultDictionary;
        User.firstName = FBUserDetails["first_name"].ToString();
        User.lastName = FBUserDetails["last_name"].ToString();
        User.userId = FBUserDetails["id"].ToString();

        StartCoroutine(LoadARModeScene(2));
    }


    private void FetchFBProfile()
    {
        FB.API("/me?fields=first_name,last_name,email,location{location}", HttpMethod.GET, FetchProfileCallback, new Dictionary<string, string>() { });
    }

    

    private void OnHideUnityCallBack(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }



    public void onLoginButtonPressed()
    {
        StartCoroutine(LoadARModeScene(2));
        //// if fb sdk is not initialized
        //if (!FB.IsInitialized)
        //{
        //    // Initialize the Facebook SDK
        //    FB.Init(InitCallback, OnHideUnityCallBack);
        //}
        //else
        //{
        //    // Already initialized, signal an app activation App Event
        //    FB.ActivateApp();
        //    FB.LogInWithReadPermissions(permissions, AuthCallback);
        //}


    }



}
