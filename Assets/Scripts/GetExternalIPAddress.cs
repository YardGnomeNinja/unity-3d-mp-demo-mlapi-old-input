using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class GetExternalIPAddress : MonoBehaviour
{
    public TMP_Text yourIpAddressField;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest("https://api.ipify.org"));
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(uri);

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError)
        {
            Debug.Log("An error occurred: " + unityWebRequest.error);
            PopulateYourIPAddressValue("Unable to determine.");
        }
        else
        {
            PopulateYourIPAddressValue(unityWebRequest.downloadHandler.text);
        }
    }

    void PopulateYourIPAddressValue(string value)
    {
        yourIpAddressField.text = value;
    }
}
