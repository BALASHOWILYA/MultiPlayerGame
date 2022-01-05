using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar]
    [SerializeField]
    private string displayName = "Missing name";

    [SyncVar]
    [SerializeField]
    private Color displayColour = new Color(0,0,0);

    [Server]
    public void SetDisplayColor(Color RGB)
    {
        displayColour = RGB;
    }

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }
}
