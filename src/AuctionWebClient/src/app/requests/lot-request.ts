import {Guid} from "guid-typescript";
import {UploadFileRequest} from "./upload-file-request";

export interface LotRequest {
  id : string;
  name: string;
  description: string;
  timeStart: Date;
  timeEnd: Date;
  startPrice : number;
  categoryId: Guid;
  holderId?: Guid
}
