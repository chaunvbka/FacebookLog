using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FacebookManager : Singleton<FacebookManager>
{
    private string status = "Ready";
    private string lastResponse = string.Empty;
    private string title = "";

    public string Status
    {
        get
        {
            return this.status;
        }

        set
        {
            this.status = value;
        }
    }

    public string LastResponse
    {
        get
        {
            return this.lastResponse;
        }

        set
        {
            this.lastResponse = value;
        }
    }

    public void FBInit()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(this.OnInitComplete, this.OnHideUnity);
        }
        this.Status = "FB.Init() called with " + FB.AppId;

        DebugManager.Instance.ShowLog(title, "Status: " + Status);
    }

    private void OnInitComplete()
    {
        title = "OnInitComplete";
        this.Status = "Success";
        DebugManager.Instance.ShowLog(title, "Status: " + Status);
        DebugManager.Instance.ShowLog(title, "IsInitialized: " + FB.IsInitialized.ToString());
        DebugManager.Instance.ShowLog(title, "IsLoggedIn: " + FB.IsLoggedIn.ToString());

        if (AccessToken.CurrentAccessToken != null)
        {
            string token = AccessToken.CurrentAccessToken.ToString();
            DebugManager.Instance.ShowLog(title, "AccessToken: " + token);
        }
        else
        {
            DebugManager.Instance.ShowLog(title, "AccessToken: null");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        title = "OnHideUnity";
        this.Status = "Success";
        DebugManager.Instance.ShowLog(title, "Status: " + Status);

        this.LastResponse = string.Format("Success Response: isGameShown {0}\n", isGameShown);
        DebugManager.Instance.ShowLog(title, LastResponse);

        DebugManager.Instance.ShowLog(title, "IsInitialized: " + FB.IsInitialized.ToString());
        DebugManager.Instance.ShowLog(title, "IsLoggedIn: " + FB.IsLoggedIn.ToString());
    }

    public void FBLogIn()
    {
        string title = "FBLogin request";
        if (FB.IsInitialized)
        {
            FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, LogInResponse);
        }

        // It is generally good behavior to split asking for read and publish
        // permissions rather than ask for them all at once.
        //
        // In your own game, consider postponing this call until the moment
        // you actually need it.

        //FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, LogInResponse);

        DebugManager.Instance.ShowLog(title, "IsLoggedIn: " + FB.IsLoggedIn.ToString());
    }

    public void FBLogOut()
    {
        string title = "FBLogOut request";
        DebugManager.Instance.ShowLog(title, "IsLoggedIn: " + FB.IsLoggedIn.ToString());
        FB.LogOut();
    }

    private void LogInResponse(IResult result)
    {
        title = "LogInResponse";
        if (result == null)
        {
            this.LastResponse = "Null Response\n";
            DebugManager.Instance.ShowLog(title, LastResponse);
            return;
        }

        // Some platforms return the empty string instead of null.
        if (!string.IsNullOrEmpty(result.Error))
        {
            this.Status = "Error - Check log for details";
            this.LastResponse = "Error Response:\n" + result.Error;
        }
        else if (result.Cancelled)
        {
            this.Status = "Cancelled - Check log for details";
            this.LastResponse = "Cancelled Response:\n" + result.RawResult;
        }
        else if (!string.IsNullOrEmpty(result.RawResult))
        {
            this.Status = "Success - Check log for details";
            this.LastResponse = "Success Response:\n" + result.RawResult;
        }
        else
        {
            this.LastResponse = "Empty Response\n";
        }
        DebugManager.Instance.ShowLog(title, LastResponse);

        //Get AccessToken
        if (AccessToken.CurrentAccessToken != null)
        {
            string token = AccessToken.CurrentAccessToken.ToString();
            DebugManager.Instance.ShowLog(title, "AccessToken: " + token);
        }
        else
        {
            DebugManager.Instance.ShowLog(title, "AccessToken: null");
        }
        DebugManager.Instance.ShowLog(title, "TokenString: " + AccessToken.CurrentAccessToken.TokenString);
    }
}
