using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using TMPro;
using MLAPI.Transports.UNET;
using UnityEngine.EventSystems;
using System.Net;
using MLAPI.Transports.Tasks;

public class NetworkingManagerController : MonoBehaviour
{
    public string startHostingText = "Start Hosting";
    public string stopHostingText = "Stop Hosting";
    public string startClientText = "Join Host";
    public string stopClientText = "Leave Host";
    public Button hostButton;
    public Button clientButton;
    public TMP_InputField hostIpAddressField;
    public int hostPort = 7777;
    public TMP_Text connectionStatusLabel;

    public void Start() {
        NetworkingManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkingManager.Singleton.StartServer();
        connectionStatusLabel.text = "";
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientId, MLAPI.NetworkingManager.ConnectionApprovedDelegate callback)
    {
        Debug.Log("connection approval");
        //Your logic here
        bool approve = true;
        bool createPlayerObject = true;

        //ulong? prefabHash = SpawnManager.GetPrefabHashFromGenerator("MyPrefabHashGenerator"); // The prefab hash. Use null to use the default player prefab
        
        //If approve is true, the connection gets added. If it's false. The client gets disconnected
        //callback(createPlayerObject, prefabHash, approve, positionToSpawnAt, rotationToSpawnWith);
        callback(createPlayerObject, null, approve, null, null);
    }

    public void ClickHostButton()
    {
        // Game starts as server until user clicks a button.
        if (NetworkingManager.Singleton.IsServer) {
            Disconnect();
        }

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
        // Game starts as server until user clicks a button.
        if (NetworkingManager.Singleton.IsServer) {
            Disconnect();
        }

        if (NetworkingManager.Singleton.IsClient) {
            Disconnect();
            return;
        }

        IPAddress hostIpAddress;

        if (IPAddress.TryParse(hostIpAddressField.text, out hostIpAddress)) {
            NetworkingManager.Singleton.GetComponent<UnetTransport>().ConnectAddress = hostIpAddress.ToString();
            NetworkingManager.Singleton.StartClient();

            var clientButtonText = clientButton.GetComponentInChildren<Text>();
            clientButtonText.text = stopClientText;

            hostButton.interactable = false;
            hostIpAddressField.interactable = false;

            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void Update() {
        connectionStatusLabel.text = "";

        if(NetworkingManager.Singleton.IsServer || NetworkingManager.Singleton.IsHost) {
            foreach(var connectedClient in NetworkingManager.Singleton.ConnectedClients) {
                connectionStatusLabel.text += $"connectedClientId:{connectedClient.Key}\n";
            }

            foreach(var pendingClient in NetworkingManager.Singleton.PendingClients) {
                connectionStatusLabel.text += $"pendingClient:{pendingClient.Key}\n";
            }
        }

        if(NetworkingManager.Singleton.IsClient && !NetworkingManager.Singleton.IsHost) {
            connectionStatusLabel.text = $"Connecting to {hostIpAddressField.text}";
        }

        if(NetworkingManager.Singleton.IsConnectedClient) {
            connectionStatusLabel.text = $"Connected to {hostIpAddressField.text}";
        }
    }
}
