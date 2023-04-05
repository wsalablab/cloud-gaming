using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;

public static class PlayfabAuth
{
    // Const - Save email/password
    public const string PlayfabAuthPlayerPrefsKeyUsername = "playfab_auth_username";
    public const string PlayfabAuthPlayerPrefsKeyEmail = "playfab_auth_email";
    public const string PlayfabAuthPlayerPrefsKeyPassword = "playfab_auth_password";

    // Getter
    public static bool IsLoggedIn
    {
        get
        {
            // TODO: Implement check that we are logged in
            return PlayFabClientAPI.IsClientLoggedIn();
        }
    }

    // Functions
    public static void TryRegisterWithEmail(string email, string password, Action registerResultCallback, Action errorCallback)
    {
        PlayfabAuth.TryRegisterWithEmail(email, password, email, registerResultCallback, errorCallback);
    }

    public static void TryRegisterWithEmail(string email, string password, string username, Action registerResultCallback, Action errorCallback)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            Username = username
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, result =>
        {
            // Save email/password to PlayerPrefs
            PlayerPrefs.SetString(PlayfabAuthPlayerPrefsKeyEmail, email);
            PlayerPrefs.SetString(PlayfabAuthPlayerPrefsKeyPassword, password);

            // Callback
            registerResultCallback.Invoke();
        }, error =>
        {
            // Callback
            errorCallback.Invoke();
        });
    }

    public static void TryLoginWithEmail(string email, string password, Action loginResultCallback, Action errorCallback)
    {
        // TODO: Request playfab for login
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };
         PlayFabClientAPI.LoginWithEmailAddress(request, result =>
        {
            // Save email/password to PlayerPrefs
            PlayerPrefs.SetString(PlayfabAuthPlayerPrefsKeyEmail, email);
            PlayerPrefs.SetString(PlayfabAuthPlayerPrefsKeyPassword, password);

            // Callback
            loginResultCallback.Invoke();
        }, error =>
        {
            // Callback
            errorCallback.Invoke();
        });
    }

    // Logout
    public static void Logout(Action logoutResultCallback, Action errorCallback)
    {
        // Clear all keys from PlayerPrefs
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyUsername);
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyEmail);
        PlayerPrefs.DeleteKey(PlayfabAuth.PlayfabAuthPlayerPrefsKeyPassword);

        // Callback
        logoutResultCallback.Invoke();
    }
}
