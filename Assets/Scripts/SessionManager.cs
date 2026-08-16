using UnityEngine;
using Unity.Services.Core;
using System;
using Unity.Services.Authentication;
using Unity.Services.Multiplayer;
using TMPro;
using System.Threading.Tasks;

public class SessionManager : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;

    public static SessionManager Instance { get; private set; }

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        OnEnable();
    }

    public async Task Initialize()
    {
        try
        {
            UnityServices.Initialized += () => Debug.Log("Unity Services Initialized");
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"Sign in anonymously succeeded! PlayerID: {AuthenticationService.Instance.PlayerId}");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public async Task CreateSession()
    {
        var options = new SessionOptions
        {
            MaxPlayers = 4
        }.WithDistributedAuthorityNetwork();

        var session = await MultiplayerService.Instance.CreateSessionAsync(options);
        Debug.Log($"Session {session.Id} created! Join code: {session.Code}");
    }

    private void OnEnable()
    {
        // Subscribe to the log event when this object is active
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        // Unsubscribe when inactive to avoid memory leaks
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Optional: Format based on log type (Error = Red, Warning = Yellow, etc.)
        string color = "white";
        switch (type)
        {
            case LogType.Error:
            case LogType.Exception:
                color = "red";
                break;
            case LogType.Warning:
                color = "yellow";
                break;
        }

        // Format the message with Unity's rich text tags
        string newMessage = $"<color={color}>{logString}</color>";

        logText.text = newMessage;
    }
}
