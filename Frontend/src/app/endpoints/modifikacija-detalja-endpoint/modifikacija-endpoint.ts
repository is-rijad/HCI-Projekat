import {ModifikacijaEndpointReq} from "./modifikacija-endpoint-req";
import {ModifikacijaEndpointRes} from "./modifikacija-endpoint-res";
import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../base-endpoint";

@Injectable()
export class ModifikacijaEndpoint implements BaseEndpoint<ModifikacijaEndpointReq, ModifikacijaEndpointRes>{
  constructor(private httpClient : HttpClient) {}

  Akcija(url: string, req: ModifikacijaEndpointReq): Observable<ModifikacijaEndpointRes> {
    return this.httpClient.post<ModifikacijaEndpointRes>(url, req);
  }
}
