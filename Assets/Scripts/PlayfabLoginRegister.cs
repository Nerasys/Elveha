using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayfabLoginRegister : MonoBehaviour
{
    // Start is called before the first frame update

    #region variable
    private string userNameLogin;
    private string userPassword;
    public string username;

    private string ID;

    private string registerUserEmail;
    private string registerUserPassword;
    private string registerUsername;

    private float timerRegister;
    private float timerLogin;

    public GameObject errorMessage;
    public InputField IFRegisterUsername;
    public InputField IFRegisterPassword;
    public InputField IFRegisterEmail;

    public InputField IFLoginPassword;
    public InputField IFLoginUsername;

    public GameObject loginPanel;
    public GameObject registerPanel;
    public GameObject gamePanel;


    #endregion

    void Start()
    {
        timerRegister = 0.0f;
        PlayerPrefs.DeleteAll();
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "7768F";

        }

    }



    #region LOGIN/Register

    public void OnCancelButton()
    {

        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        registerUserEmail = "";
        registerUsername = "";
        registerUserPassword = "";
        IFRegisterEmail.text = "";
        IFRegisterUsername.text = "";
        IFRegisterPassword.text = "";

    }


    // BUTTON REGISTER
    public void OnRegisterButton()
    {

        loginPanel.SetActive(false);
        registerPanel.SetActive(true);

    }

    //MESSAGE D' ERREUR ET REUSSITE FUTUR EVENT POUR ACTIVER DES TEXTS 

    private void OnLoginSuccess(LoginResult log)
    {
      
        PlayerPrefs.SetString("EMAIL", userNameLogin);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        ID = log.PlayFabId;

        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            PlayFabId = log.PlayFabId,
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true
            }
        },
         LaunchGame,
         error => Debug.LogError(error.GenerateErrorReport()));



    }

    private void LaunchGame(GetPlayerProfileResult result)
    {

        username = result.PlayerProfile.DisplayName;
        gamePanel.SetActive(true);
        loginPanel.SetActive(false);

    }

    //void OnGetStats(GetPlayerStatisticsResult result)
    //{
    //    Debug.Log("Received the following Statistics:");
    //    foreach (var eachStat in result.Statistics)
    //    {
    //        //Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
    //        switch (eachStat.StatisticName)
    //        {
    //            case "PlayerLevel":
    //                dataManager.SetPlayerLevel(eachStat.Value);
    //                break;


    //        }
    //    }


 
    //}





    private void OnLoginFailure(PlayFabError error)
    {
    
        Debug.Log("Je ne te connais pas");
        Debug.Log(error.GenerateErrorReport());
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {

        Debug.Log("Congratulations, tu es connecté via register");
        PlayerPrefs.SetString("EMAIL", userNameLogin);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        PlayerPrefs.SetString("USERNAME", username);

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = registerUsername }, OnDisplayName, OnRegisterFailure);
        /*PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
        new StatisticUpdate { StatisticName = "PlayerLevel", Value = dataManager.GetPlayerLevel() }
         }
        },
       result2 => { Debug.Log("User statistics updated"); },
       error => { Debug.LogError(error.GenerateErrorReport()); });
        //  Debug.Log(dataManager.GetEmail());
        //  AddOrUpdateContactEmail(dataManager.GetEmail());
        */

    }

    private void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
      
        loginPanel.SetActive(true);

        registerPanel.SetActive(false);
    }



    private void OnRegisterFailure(PlayFabError error)
    {

        Debug.Log(error.HttpCode);
        Debug.Log(error.GenerateErrorReport());

  
    }

    //REQUETE DE CONNEXION

    public void OnClickLogin()
    {

        userNameLogin = IFLoginUsername.text;
        userPassword = IFLoginPassword.text;

        var request = new LoginWithPlayFabRequest
        {
            Username = userNameLogin,
            Password = userPassword
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);

    }


    public void OnClickRegister()
    {
        registerUserEmail = IFRegisterEmail.text;
        registerUsername = IFRegisterUsername.text;
        registerUserPassword = IFRegisterPassword.text;

      
            var registerRequest = new RegisterPlayFabUserRequest();
            registerRequest.Email = registerUserEmail;
            registerRequest.Password = registerUserPassword;
            registerRequest.Username = registerUsername;

            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
        
      


    }

    // Update is called once per frame




    void AddOrUpdateContactEmail(string emailAddress)
    {
        var request = new AddOrUpdateContactEmailRequest
        {
            EmailAddress = emailAddress
        };
        PlayFabClientAPI.AddOrUpdateContactEmail(request, result =>
        {
            Debug.Log("The player's account has been updated with a contact email");
        }, FailureCallback);
    }

    void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion

    #region Player_Stats







    #endregion

}
