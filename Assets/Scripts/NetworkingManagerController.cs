using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using TMPro;
using MLAPI.Transports.UNET;
using UnityEngine.EventSystems;
using System.Net;

public class NetworkingManagerController : MonoBehaviour
{
    public string startHostingText = "Start Hosting";
    public string stopHostingText = "Stop Hosting";
    public string startClientText = "Join Host";
    public string stopClientText = "Leave Host";
    public Button hostButton;
    public Button clientButton;
    public TMP_InputField hostIpAddressField;
    public int hostPort = 77777;

    public void ClickHostButton()
    {
        if (NetworkingManager.Singleton.IsHost) {
            Disconnect();
            return;
        }

        NetworkingManager.Singleton.StartHost();

        var hostButtonText = hostButton.GetComponentInChildren<Text>();
        hostButtonText.text = stopHostingText;

        clientButton.interactable = false;
        hostIpAddressField.interactable = false;

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Disconnect() 
    {
        if (NetworkingManager.Singleton.IsHost) 
        {
            NetworkingManager.Singleton.StopHost();
        }

        if (NetworkingManager.Singleton.IsClient) 
        {
            NetworkingManager.Singleton.StopClient();
        }

        if (NetworkingManager.Singleton.IsServer) 
        {
            NetworkingManager.Singleton.StopServer();
        }

        var hostButtonText = hostButton.GetComponentInChildren<Text>();
        hostButtonText.text = startHostingText;
        hostButton.interactable = true;

        hostIpAddressField.interactable = true;
        var clientButtonText = clientButton.GetComponentInChildren<Text>();
        clientButtonText.text = startClientText;
        clientButton.interactable = true;
    }

    public void ClickClientButton() 
    {
        if (NetworkingManager.Singleton.IsClient) {
            Disconnect();
            return;
        }

        IPAddress hostIpAddress;
        
        if (IPAddress.TryParse(hostIpAddressField.text, out hostIpAddress)) {
            NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = hostIpAddressField.text;
            NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectPort = hostPort;
            NetworkingManager.Singleton.StartClient();

            var clientButtonText = clientButton.GetComponentInChildren<Text>();
            clientButtonText.text = stopClientText;

            hostButton.interactable = false;
            hostIpAddressField.interactable = false;

            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
