import {Guid} from "guid-typescript";

export interface LotRequest {
  name: string;
  description: string;
  timeStart: Date;
  timeEnd: Date;
  startPrice : number;
  categoryId: Guid;
  holderId?: Guid
}
