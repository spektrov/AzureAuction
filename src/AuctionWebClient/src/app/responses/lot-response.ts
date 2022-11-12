import {Guid} from "guid-typescript";
import {Category} from "../models/category";

export interface LotResponse {
  id?: Guid;
  name: string;
  description?: string;
  timeStart?: Date;
  timeEnd?: Date;
  startPrice? : number;
  maxPrice? : number;
  category?: Category;
  holderId?: Guid
}
