using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;
    string gameId = "4115840";
    string placementId = "Interstitial_Android";

   
    bool testMode = true;

    void Start()
    {
        
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);

       if(Instance == null)
        {
            Instance = this;
        }
    }
    public void ShowAds()
    {
        Invoke("ShowInterstitialAd", 1);
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
   
}
