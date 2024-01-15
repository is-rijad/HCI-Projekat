import {BaseResponse} from "../base-response";

export class PretragaEndpointRes extends BaseResponse {
  sobe : PretragaEndpointResSoba[] = [];
}

export interface PretragaEndpointResSoba {
  id : number,
  nazivSobe : string,
  slike : string,
  brojGostiju : number,
  opis : string
}
