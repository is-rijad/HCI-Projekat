import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../../base-endpoint";
import {OtkaziRezervacijuReq} from "./otkazi-rezervaciju-req";
import {OtkaziRezervacijuRes} from "./otkazi-rezervaciju-res";
import {Observable} from "rxjs";
import {Config} from "../../../config";

@Injectable()
export class OtkaziRezervacijuEnpoint implements BaseEndpoint<OtkaziRezervacijuReq, OtkaziRezervacijuRes>{
  constructor(private httpClient:HttpClient) {
  }

  Akcija(req: OtkaziRezervacijuReq): Observable<OtkaziRezervacijuRes> {
    let url = Config.adresaServera + `OtkaziRezervaciju`;
    return this.httpClient.delete<OtkaziRezervacijuRes>(url, {body: req, withCredentials:true});
  }

}
