import {Config} from "../../config";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../base-endpoint";
import {OtvoriDetaljeEndpointRes} from "./otvori-detalje-endpoint-res";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";

@Injectable()
export class OtvoriDetaljeEndpoint implements BaseEndpoint<void, OtvoriDetaljeEndpointRes>{
  constructor(private httpClient : HttpClient) {
  }

  Akcija(url: string): Observable<OtvoriDetaljeEndpointRes> {
    return this.httpClient.get<OtvoriDetaljeEndpointRes>(url);
  }
}
