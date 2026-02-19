import { Client, Room } from "colyseus";
import { registerMovementHandlers } from "./messages/movementHandlers";
import { Player } from "./schema/Player";
import { State } from "./schema/State";
import { createMovementSystem } from "./systems/movementSystem";

export class MyRoom extends Room<State> {
  maxClients = 20;
  private speed = 6;

  onCreate() {
    this.setState(new State());

    registerMovementHandlers(this);
    this.setSimulationInterval(createMovementSystem(this.state, this.speed));
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
