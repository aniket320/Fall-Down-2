using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour, IUnityAdsListener
{

    string gameId = "4115840";
    string placementId = "Interstitial_Android";

  
    bool testMode = true;
    //private Coin coin;

    void Start()
    {
       
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);


        Advertisement.AddListener(this);
    }
    public void showAds()
    {
        Invoke("ShowInterstitialAd", 3);
    }
    public void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(placementId))
        {
            Advertisement.Show(placementId);
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }



    //Implement IUnityAdsListener interface methods :
    public void OnUnityAdsDidFinish(string msurfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            //coin.coinCount += 10;
        }
        else if (showResult == ShowResult.Skipped)
        {
            GameManager.instace.leaveGame();
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string surfacingId)
    {
        // If the ready Ad Unit or legacy Placement is rewarded, show the ad:
        if (surfacingId == gameId)
        {
            // Optional actions to take when theAd Unit or legacy Placement becomes ready (for example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

}
