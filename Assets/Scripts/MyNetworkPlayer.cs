using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{

    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColourRenderer = null;

    //Add a hook

    [SyncVar(hook = nameof(HandleDisplayTextUpdated))]
    [SerializeField]
    private string displayName = "Missing name";

    [SyncVar(hook = nameof(HandleDisplayColourUpdated))]
    [SerializeField]
    private Color displayColour = Color.black;

    [Server]
    public void SetDisplayColor(Color newDisplayColor)
    {
        displayColour = newDisplayColor;
    }

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }
    // methods to be used as the callback
    private void HandleDisplayColourUpdated(Color oldColour, Color newColour)
    {
        displayColourRenderer.material.SetColor("_Color", newColour); 
       
    }

    private void HandleDisplayTextUpdated(string oldText, string newText)
    {
        displayNameText.text = newText;
    }

   
}
