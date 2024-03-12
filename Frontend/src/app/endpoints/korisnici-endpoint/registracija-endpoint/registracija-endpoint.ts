import {BaseResponse} from "../../base-response";
import {BaseEndpoint} from "../../base-endpoint";
import {RegistracijaEndpointReq} from "./registracija-endpoint-req";
import {Observable} from "rxjs";
import {Config} from "../../../config";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export class RegistracijaEndpoint implements BaseEndpoint<RegistracijaEndpointReq, BaseResponse> {
  constructor(private httpClient:HttpClient) {
  }

  Akcija(req: RegistracijaEndpointReq): Observable<BaseResponse> {
    let url = Config.adresaServera + "Korisnici/RegistrujSe"
    return this.httpClient.post<BaseResponse>(url, req);
  }
}
