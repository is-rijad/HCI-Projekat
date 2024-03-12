import {BaseEndpoint} from "../../base-endpoint";
import {NapraviRezervacijuEndpointReq} from "./napravi-rezervaciju-endpoint-req";
import {BaseResponse} from "../../base-response";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Config} from "../../../config";

@Injectable()
export class NapraviRezervacijuEndpoint implements BaseEndpoint<NapraviRezervacijuEndpointReq, BaseResponse> {
  constructor(private httpClient: HttpClient) {
  }

  Akcija(req: NapraviRezervacijuEndpointReq): Observable<BaseResponse> {
    let url = Config.adresaServera + "Rezervacije/NapraviRezervaciju"
    return this.httpClient.post<BaseResponse>(url, req, {withCredentials:true});
  }
}
