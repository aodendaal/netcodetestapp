# NetCodeTestApp

Requires Unity 6.4 (6000.4.4f1) or higher.

## Console Commands

- HostSession
- Initialize
- JoinSession `<joinCode>`
- LeaveSession
- ListPlayers
- quit
- SessionCode
- SessionState
- user-commands

## Note

`Initialize` must be called first before calling any multiplayer commands

Use `Escape` to toggle showing or hiding the dev console.

## Events Monitored

- Changed
- Deleted
- PlayerHasLeft `<player>`
- PlayerJoined `<player>`
- PlayerLeaving `<player>`
- PlayerPropertiesChanged
- RemovedFromSession
- SessionHostChanged `<player>`
- SessionMigrated
- StateChanged `<state>`
