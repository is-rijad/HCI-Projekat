import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../../base-endpoint";
import {GetBuduceRezervacijeZaGostaReq} from "./get-buduce-rezervacije-za-gosta-req";
import {GetBuduceRezervacijeZaGostaRes} from "./get-buduce-rezervacije-za-gosta-res";
import {Observable} from "rxjs";
import {Config} from "../../../config";

@Injectable()
export class GetBuduceRezervacijeZaGostaEndpoint implements BaseEndpoint<GetBuduceRezervacijeZaGostaReq, GetBuduceRezervacijeZaGostaRes>{
  constructor(private httpClient:HttpClient) {
  }

  Akcija(req: GetBuduceRezervacijeZaGostaReq): Observable<GetBuduceRezervacijeZaGostaRes> {
    let url = Config.adresaServera + `GetBuduceRezervacijeZaGosta?GostId=${req.gostId}`;
    return this.httpClient.get<GetBuduceRezervacijeZaGostaRes>(url);
  }

}
