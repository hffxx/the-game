export type Vector2 = { x: number; y: number };

export const normalizeInput = (input: Vector2): Vector2 => {
  const length = Math.hypot(input.x, input.y);
  if (length === 0) return { x: 0, y: 0 };
  return { x: input.x / length, y: input.y / length };
};

export const integratePosition = (
  position: Vector2,
  direction: Vector2,
  speed: number,
  deltaSeconds: number
): Vector2 => {
  return {
    x: position.x + direction.x * speed * deltaSeconds,
    y: position.y + direction.y * speed * deltaSeconds
  };
};
