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

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing name";

    [SyncVar(hook = nameof(HandleDisplayColourUpdated))]
    [SerializeField]
    private Color displayColour = Color.black;

    #region Server

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

    //method that call from the client

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        //BlackListed words
        List<string> BlackListed = new List<string>();
        BlackListed.Add("fuck");
        BlackListed.Add("asshole");

        //WhiteSpace
        BlackListed.Add(" ");

        foreach (string blackWord in BlackListed)
        {
            if (newDisplayName.IndexOf(blackWord)>-1)
            {
                Debug.Log("wrong name");
                return;
            }
        }
        if (newDisplayName.Length < 2 || newDisplayName.Length > 20)
        {
            Debug.Log("The name have more than 10 letters");
            return;  
        }
        else
        {
            RpcLogNewName(newDisplayName);

            SetDisplayName(newDisplayName);
        }
    }

    #endregion

    #region Client
    // methods to be used as the callback
    private void HandleDisplayColourUpdated(Color oldColour, Color newColour)
    {
        displayColourRenderer.material.SetColor("_Color", newColour); 
       
    }

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("ilya");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion


}
