using System.Collections.Generic;
using System.Threading.Tasks;
using Colyseus;
using UnityEngine;

public class ColyseusManager : MonoBehaviour
{
    [Header("Connection")]
    public string serverUrl = "ws://localhost:2567";
    public string roomName = "my_room";

    [Header("Scene")]
    public GameObject playerPrefab;

    private ColyseusClient client;
    private ColyseusRoom<GameState> room;
    private readonly Dictionary<string, GameObject> playerViews = new();

    private async void Start()
    {
        await Connect();
    }

    private async Task Connect()
    {
        client = new ColyseusClient(serverUrl);
        room = await client.JoinOrCreate<GameState>(roomName);

        room.State.players.OnAdd += HandlePlayerAdd;
        room.State.players.OnRemove += HandlePlayerRemove;
    }

    private void HandlePlayerAdd(string sessionId, Player player)
    {
        if (playerPrefab == null)
        {
            Debug.LogWarning("ColyseusManager: playerPrefab not assigned.");
            return;
        }

        var view = Instantiate(playerPrefab);
        view.transform.position = new Vector3(player.x, player.y, 0f);
        playerViews[sessionId] = view;

        player.OnChange += (changes) =>
        {
            if (view == null) return;
            view.transform.position = new Vector3(player.x, player.y, 0f);
        };
    }

    private void HandlePlayerRemove(string sessionId, Player player)
    {
        if (playerViews.TryGetValue(sessionId, out var view))
        {
            Destroy(view);
            playerViews.Remove(sessionId);
        }
    }

    private void Update()
    {
        if (room == null) return;

        var input = new Dictionary<string, float>
        {
            { "x", Input.GetAxisRaw("Horizontal") },
            { "y", Input.GetAxisRaw("Vertical") }
        };

        room.Send("move", input);
    }

    private void OnApplicationQuit()
    {
        if (room != null)
        {
            room.Leave();
        }
    }
}
