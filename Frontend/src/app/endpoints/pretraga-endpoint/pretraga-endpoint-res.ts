import {BaseResponse} from "../base-response";

export class PretragaEndpointRes extends BaseResponse {
  sobe: PretragaEndpointResSoba[] = [];
}

export interface PretragaEndpointResSoba {
  id: number,
  nazivSobe: string,
  cijena: number,
  brojGostiju: number,
  opis: string,
  slika: string,
}
