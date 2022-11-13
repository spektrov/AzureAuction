import {Guid} from "guid-typescript";

export interface BidRequest {
  userId? : Guid;
  lotId : Guid;
  price : number;
  ts : Date;
}
