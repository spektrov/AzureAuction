import {Guid} from "guid-typescript";

export interface TokenResponse {
  accessToken: string;
  refreshToken: string;
  firstName: string;
  userId: string
}
