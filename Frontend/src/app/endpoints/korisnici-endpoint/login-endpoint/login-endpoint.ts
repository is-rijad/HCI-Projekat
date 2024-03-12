import {Injectable} from "@angular/core";
import {BaseEndpoint} from "../../base-endpoint";
import {GetAllAranzmaneEndpointRes} from "../../aranzmani-endpoint/get-all-aranzmane/get-all-aranzmane-endpoint-res";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Config} from "../../../config";
import {LoginReq} from "./login-req";
import {LoginRes} from "./login-res";

@Injectable()
export class LoginEndpoint implements BaseEndpoint<LoginReq, LoginRes> {
  constructor(private httpClient: HttpClient) {
  }

  Akcija(req: LoginReq): Observable<LoginRes> {
    let url = Config.adresaServera + "Korisnici/UlogujSe"
    return this.httpClient.post<LoginRes>(url, req);
  }

}
