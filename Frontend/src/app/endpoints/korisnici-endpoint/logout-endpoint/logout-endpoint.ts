import {Injectable} from "@angular/core";
import {BaseEndpoint} from "../../base-endpoint";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Config} from "../../../config";
import {LogoutRes} from "./logout-res";

@Injectable()
export class LogoutEndpoint implements BaseEndpoint<void, LogoutRes> {
  constructor(private httpClient: HttpClient) {
  }

  Akcija(req: void): Observable<LogoutRes> {
    let url = Config.adresaServera + "Korisnici/OdjaviSe"
    return this.httpClient.get<LogoutRes>(url, {withCredentials: true});
  }

}
