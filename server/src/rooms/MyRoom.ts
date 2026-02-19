import { Room, Client } from "colyseus";
import { Schema, MapSchema, type } from "@colyseus/schema";
import { integratePosition, normalizeInput, Vector2 } from "./movement";

export class Player extends Schema {
  @type("number") x = 0;
  @type("number") y = 0;
  @type("number") dx = 0;
  @type("number") dy = 0;
}

export class State extends Schema {
  @type({ map: Player }) players = new MapSchema<Player>();
}

export class MyRoom extends Room<State> {
  maxClients = 20;
  private speed = 6;

  onCreate() {
    this.setState(new State());

    this.onMessage("move", (client, input: Vector2) => {
      const player = this.state.players.get(client.sessionId);
      if (!player) return;
      const dir = normalizeInput(input);
      player.dx = dir.x;
      player.dy = dir.y;
    });

    this.setSimulationInterval((deltaTime) => {
      const deltaSeconds = deltaTime / 1000;
      this.state.players.forEach((player) => {
        const next = integratePosition(
          { x: player.x, y: player.y },
          { x: player.dx, y: player.dy },
          this.speed,
          deltaSeconds
        );
        player.x = next.x;
        player.y = next.y;
      });
    });
  }

  onJoin(client: Client) {
    const player = new Player();
    player.x = Math.random() * 5;
    player.y = Math.random() * 5;
    this.state.players.set(client.sessionId, player);
  }

  onLeave(client: Client) {
    this.state.players.delete(client.sessionId);
  }
}
