import {ModifikacijaEndpointReq} from "./modifikacija-endpoint-req";
import {ModifikacijaEndpointRes} from "./modifikacija-endpoint-res";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../base-endpoint";
import {Config} from "../../config";

@Injectable()
export class ModifikacijaEndpoint implements BaseEndpoint<ModifikacijaEndpointReq, ModifikacijaEndpointRes>{
  constructor(private httpClient : HttpClient) {}

  Akcija(req: ModifikacijaEndpointReq): Observable<ModifikacijaEndpointRes> {
    let url = Config.adresaServera + "Sobe/ModifikujSobu";
    return this.httpClient.post<ModifikacijaEndpointRes>(url, req);
  }
}
