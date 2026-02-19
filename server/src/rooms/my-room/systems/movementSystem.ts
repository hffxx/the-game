import { integratePosition } from "../../movement";
import { State } from "../schema/State";

export const createMovementSystem = (state: State, speed: number) => {
  return (deltaTime: number) => {
    const deltaSeconds = deltaTime / 1000;
    state.players.forEach((player) => {
      const next = integratePosition(
        { x: player.x, y: player.y },
        { x: player.dx, y: player.dy },
        speed,
        deltaSeconds
      );
      player.x = next.x;
      player.y = next.y;
    });
  };
};
