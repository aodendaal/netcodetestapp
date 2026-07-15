using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using QFSW.QC;
using System.Linq;
public class QuantumSessionManager : MonoBehaviour
{
    private ISession currentSession;

    [Command()]
    public async void Initialize()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log($"Sign in anonymously succeeded! PlayerID: {AuthenticationService.Instance.PlayerId}");
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    [Command()]
    public async void HostSession()
    {
        var options = new SessionOptions
        {
            MaxPlayers = 2
        }.WithDistributedAuthorityNetwork();

        currentSession = await MultiplayerService.Instance.CreateSessionAsync(options);
        Debug.Log($"Session {currentSession.Id} created! Join code: {currentSession.Code}");
        SubscribeToSessionEvents();
    }

    private void SubscribeToSessionEvents()
    {
        if (currentSession != null)
        {
            currentSession.Changed += () => { Debug.LogWarning($"Session changed."); };
            currentSession.Deleted += () => { Debug.LogWarning("Session has been deleted."); };
            currentSession.PlayerHasLeft += (player) => { Debug.LogWarning($"Player {player} has left the session."); };
            currentSession.PlayerJoined += (player) => { Debug.LogWarning($"Player {player} joined the session."); };
            currentSession.PlayerLeaving += (player) => { Debug.LogWarning($"Player {player} is leaving the session."); };
            currentSession.PlayerPropertiesChanged += () => { Debug.LogWarning("Player properties changed."); };
            currentSession.RemovedFromSession += () => { Debug.LogWarning("You have been removed from the session."); };
            currentSession.SessionHostChanged += (player) => { Debug.LogWarning($"Session host changed to {player}."); };
            currentSession.SessionMigrated += () => { Debug.LogWarning($"Session migrated."); };
            currentSession.StateChanged += (state) => { Debug.Log($"State changed to {state}."); };
        }
    }

    [Command()]
    public async void JoinSession(string joinCode)
    {
        try
        {
            currentSession = await MultiplayerService.Instance.JoinSessionByCodeAsync(joinCode);
            Debug.Log($"Joined session {currentSession.Id}!");
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    [Command()]
    public async void LeaveSession()
    {
        try
        {
            if (currentSession != null)
            {
                await currentSession.LeaveAsync();
                Debug.Log("Left session");
            }
            else
            {
                Debug.Log("No session to leave");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    [Command()]
    public void ListPlayers()
    {
        if (currentSession != null)
        {
            var players = currentSession.Players;
            Debug.Log($"({currentSession.PlayerCount}) Players in session {currentSession.Id}:");
            foreach (var player in players)
            {
                Debug.Log($"- Player ID: {player.Id}");
            }
        }
        else
        {
            Debug.Log("No session to list players from");
        }
    }

    // [Command()]
    // public async void KickPlayer(string playerId)
    // {
    //     if (currentSession != null)
    //     {
    //         var player = currentSession.Players.FirstOrDefault(p => p.Id == playerId);
    //         if (player != null)
    //         {
    //             await currentSession.KickAsync(player);
    //             Debug.Log($"Kicked player {playerId} from session {currentSession.Id}");
    //         }
    //     }
    // }

    [Command()]
    public void SessionState()
    {
        if (currentSession != null)
        {
            Debug.Log($"Session name: {currentSession.Name}");
            Debug.Log($"Session state: {currentSession.State}");
            Debug.Log($"Session max players: {currentSession.MaxPlayers}");
            Debug.Log($"Session current players: {currentSession.Players.Count}");
            Debug.Log($"Session code: {currentSession.Code}");
            Debug.Log($"Session id: {currentSession.Id}");
            Debug.Log($"Session owner: {currentSession.Host}");
        }
        else
        {
            Debug.Log("No session to get state from");
        }
    }

    [Command()]
    public void SessionCode()
    {
        if (currentSession != null)
        {
            Debug.Log($"Session code: {currentSession.Code}");
        }
        else
        {
            Debug.Log("No session to get code from");
        }
    }
}
