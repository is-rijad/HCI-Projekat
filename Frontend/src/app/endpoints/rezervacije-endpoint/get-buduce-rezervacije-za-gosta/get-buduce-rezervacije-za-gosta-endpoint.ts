import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../../base-endpoint";
import {GetBuduceRezervacijeZaGostaRes} from "./get-buduce-rezervacije-za-gosta-res";
import {Observable} from "rxjs";
import {Config} from "../../../config";

@Injectable()
export class GetBuduceRezervacijeZaGostaEndpoint implements BaseEndpoint<void, GetBuduceRezervacijeZaGostaRes>{
  constructor(private httpClient:HttpClient) {
  }

  Akcija(req: void): Observable<GetBuduceRezervacijeZaGostaRes> {
    let url = Config.adresaServera + `GetBuduceRezervacijeZaGosta`;
    return this.httpClient.get<GetBuduceRezervacijeZaGostaRes>(url, {withCredentials:true});
  }

}
