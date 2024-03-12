import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../../base-endpoint";
import {GetPrethodneRezervacijeZaGostaRes} from "./get-prethodne-rezervacije-za-gosta-res";
import {Observable} from "rxjs";
import {Config} from "../../../config";

@Injectable()
export class GetPrethodneRezervacijeZaGostaEndpoint implements BaseEndpoint<void, GetPrethodneRezervacijeZaGostaRes> {
  constructor(private httpClient: HttpClient) {
  }

  Akcija(req: void): Observable<GetPrethodneRezervacijeZaGostaRes> {
    let url = Config.adresaServera + `GetPrethodneRezervacijeZaGosta`;
    return this.httpClient.get<GetPrethodneRezervacijeZaGostaRes>(url, {withCredentials: true});

  }

}
