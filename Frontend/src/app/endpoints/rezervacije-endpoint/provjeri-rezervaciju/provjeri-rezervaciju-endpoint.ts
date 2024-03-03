import {BaseEndpoint} from "../../base-endpoint";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Config} from "../../../config";
import {NapraviRezervacijuEndpointReq} from "../napravi-rezervaciju/napravi-rezervaciju-endpoint-req";
import {ProvjeriRezervacijuEndpointRes} from "./provjeri-rezervaciju-endpoint-res";

@Injectable()
export class ProvjeriRezervacijuEndpoint implements BaseEndpoint<NapraviRezervacijuEndpointReq, ProvjeriRezervacijuEndpointRes> {
  constructor(private httpClient: HttpClient) {
  }

  Akcija(req: NapraviRezervacijuEndpointReq): Observable<ProvjeriRezervacijuEndpointRes> {
    let url = Config.adresaServera + "Rezervacije/ProvjeriRezervaciju"
    return this.httpClient.post<ProvjeriRezervacijuEndpointRes>(url, req);
  }
}
