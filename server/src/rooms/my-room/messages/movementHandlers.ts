import { Room } from "colyseus";
import { normalizeInput, Vector2 } from "../../movement";
import { State } from "../schema/State";

export const registerMovementHandlers = (room: Room<State>) => {
  room.onMessage("move", (client, input: Vector2) => {
    const player = room.state.players.get(client.sessionId);
    if (!player) return;
    const dir = normalizeInput(input);
    player.dx = dir.x;
    player.dy = dir.y;
  });
};
