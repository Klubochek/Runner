using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Threading.Tasks;

public class Ad : MonoBehaviour
{
    private InterstitialAd _interstitial;

    void Start()
    {
        MobileAds.Initialize(initstatus => { });
        RequestInterstitial();
    }
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif
        _interstitial = new InterstitialAd(adUnitId);
     
        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.LoadAd(request);
    }
    public void ShowAd()
    {
        if (_interstitial.IsLoaded())
        {
            _interstitial.Show();
        }
    }
}
