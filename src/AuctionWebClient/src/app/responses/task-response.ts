import {Guid} from "guid-typescript";

export interface TaskResponse {
  id: Guid;
  name: string;
  isCompleted: boolean;
  ts: Date;
}
