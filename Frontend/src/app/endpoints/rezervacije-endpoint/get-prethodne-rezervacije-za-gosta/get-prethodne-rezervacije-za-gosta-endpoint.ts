import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../../base-endpoint";
import {GetPrethodneRezervacijeZaGostaReq} from "./get-prethodne-rezervacije-za-gosta-req";
import {GetPrethodneRezervacijeZaGostaRes} from "./get-prethodne-rezervacije-za-gosta-res";
import {Observable} from "rxjs";
import {Config} from "../../../config";
import {GetBuduceRezervacijeZaGostaRes} from "../get-buduce-rezervacije-za-gosta/get-buduce-rezervacije-za-gosta-res";

@Injectable()
export class GetPrethodneRezervacijeZaGostaEndpoint implements BaseEndpoint<GetPrethodneRezervacijeZaGostaReq, GetPrethodneRezervacijeZaGostaRes>{
  constructor(private httpClient:HttpClient) {
  }

  Akcija(req: GetPrethodneRezervacijeZaGostaReq): Observable<GetPrethodneRezervacijeZaGostaRes> {
    let url = Config.adresaServera + `GetPrethodneRezervacijeZaGosta?GostId=${req.gostId}`;
    return this.httpClient.get<GetPrethodneRezervacijeZaGostaRes>(url);

  }

}
