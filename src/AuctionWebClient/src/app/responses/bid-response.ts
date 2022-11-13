import {Guid} from "guid-typescript";

export interface BidResponse  {
  id : Guid;
  userId : Guid;
  lotId : Guid;
  price : number;
  ts : Date;
}
