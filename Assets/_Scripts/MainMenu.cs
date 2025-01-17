﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, MPLobbyListener
{
    private bool _showLobbyDialog;
    private string _lobbyMessage;
    public GUISkin guiSkin;

	private GUIStyle currentStyle = null;

    // Use this for initialization
    void Start () {
        MultiplayerController.Instance.TrySilentSignIn();
    }

    void OnGUI()
    {
        if (_showLobbyDialog)
        {
            //Debug.Log("Show lobby msg");
            GUI.skin = guiSkin;
			InitStyles();
			GUI.Box(new Rect ((Screen.width)/2 -(Screen.width)/8,(Screen.height)/2-(Screen.height)/8,(Screen.width)/4,(Screen.height)/4), _lobbyMessage, currentStyle);
        }
    }

	private void InitStyles()
	{
		if( currentStyle == null )
		{
			currentStyle = new GUIStyle( GUI.skin.box );
			//currentStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 1f, 1f, 1f ) );
			currentStyle.fontSize = 50;
			currentStyle.wordWrap = true;
			currentStyle.contentOffset = new Vector2 (0.0f, 40.0f);
			currentStyle.normal.textColor = Color.white;


		}
	}

	private Texture2D MakeTex( int width, int height, Color col )
	{
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}

    public void PlayGame()
    {
        Debug.Log("Pressed play");
        _lobbyMessage = "Starting a multi-player game...";
        _showLobbyDialog = true;
        MultiplayerController.Instance.lobbyListener = this;
        MultiplayerController.Instance.SignInAndStartMPGame();

        /*if (_showLobbyDialog)
        {
            Debug.Log("Show lobby msg");
            GUI.skin = guiSkin;
            GUI.Box(new Rect(Screen.width * 0.25f, Screen.height * 0.4f, Screen.width * 0.5f, Screen.height * 0.5f), _lobbyMessage);
        }*/
        //SceneManager.LoadScene(1);
    }

    public void Multiplayer()
    {
        Debug.Log("Pressed multi");
        SceneManager.LoadScene(1);
    }

    public void Logout()
    {
        Debug.Log("Pressed Logout");
        if (MultiplayerController.Instance.IsAuthenticated())
        {
            MultiplayerController.Instance.SignOut();
        }
    }

    public void SetLobbyStatusMessage(string message)
    {
        _lobbyMessage = message;
    }

    public void HideLobby()
    {
        _lobbyMessage = "";
        _showLobbyDialog = false;
    }
}
