import {Config} from "../../config";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../base-endpoint";
import {OtvoriDetaljeEndpointRes} from "./otvori-detalje-endpoint-res";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {Navigator} from "../../navigator";

@Injectable()
export class OtvoriDetaljeEndpoint implements BaseEndpoint<void, OtvoriDetaljeEndpointRes> {
  constructor(private httpClient: HttpClient) {
  }

  Akcija(): Observable<OtvoriDetaljeEndpointRes> {
    let url = Config.adresaServera + "Sobe/GetSobuId/?Id=" + Navigator.trenutniIdSobe;
    return this.httpClient.get<OtvoriDetaljeEndpointRes>(url);
  }
}
