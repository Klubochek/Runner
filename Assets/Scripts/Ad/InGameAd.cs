using UnityEngine;
using UnityEngine.Advertisements;

public class InGameAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public void LoadAd()
    {
        Advertisement.Load("Interstitial_Android", this);
    }
    public void ShowAd()
    {
        Advertisement.Show("Interstitial_Android", this);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("AdsAdLoaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("UnityAdsFailedToLoad");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("UnityAdsShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("AdsShowComplete");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("UnityAdsShowFailure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("AdsShowStart");
    }
}
