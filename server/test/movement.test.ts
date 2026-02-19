import { describe, expect, it } from "vitest";
import { integratePosition, normalizeInput } from "../src/rooms/movement";

describe("movement", () => {
  it("normalizes input vectors", () => {
    const result = normalizeInput({ x: 3, y: 4 });
    expect(result.x).toBeCloseTo(0.6, 5);
    expect(result.y).toBeCloseTo(0.8, 5);
  });

  it("integrates position with speed and delta", () => {
    const result = integratePosition({ x: 1, y: 1 }, { x: 1, y: 0 }, 5, 0.2);
    expect(result).toEqual({ x: 2, y: 1 });
  });
});
