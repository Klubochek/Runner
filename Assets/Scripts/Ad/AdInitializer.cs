using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdInitializer : MonoBehaviour,IUnityAdsInitializationListener
{
    public void OnInitializationComplete()
    {
        Debug.Log("AdInitComplete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("AdInitFailed");
    }

    void Awake()
    {
        Advertisement.Initialize("5070875", true,this);
    }

    
}
