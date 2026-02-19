import { Server } from "colyseus";
import { createServer } from "http";
import express from "express";
import { MyRoom } from "./rooms/MyRoom";

const port = Number(process.env.PORT ?? 2567);
const app = express();
const server = createServer(app);
const gameServer = new Server({ server });

gameServer.define("my_room", MyRoom);

gameServer.listen(port);
console.log(`Colyseus server listening on ws://localhost:${port}`);
