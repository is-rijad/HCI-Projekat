import {PretragaEndpointReq} from "./pretraga-endpoint-req";
import {PretragaEndpointRes} from "./pretraga-endpoint-res";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../base-endpoint";
import {Config} from "../../config";

@Injectable({providedIn: "root"})
export class PretragaEndpoint implements BaseEndpoint<PretragaEndpointReq, PretragaEndpointRes> {
  constructor(private httpClient: HttpClient) {
  }

  Akcija(req: PretragaEndpointReq): Observable<PretragaEndpointRes> {
    let url = Config.adresaServera + "Sobe/GetSobe";
    return this.httpClient.post<PretragaEndpointRes>(url, req);
  }
}
