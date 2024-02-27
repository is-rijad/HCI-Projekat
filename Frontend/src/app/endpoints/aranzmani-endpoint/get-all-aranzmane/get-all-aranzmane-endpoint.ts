import {BaseEndpoint} from "../../base-endpoint";
import {GetAllAranzmaneEndpointRes} from "./get-all-aranzmane-endpoint-res";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Config} from "../../../config";
import {Navigator} from "../../../navigator";

@Injectable()
export class GetAllAranzmaneEndpoint implements BaseEndpoint<void, GetAllAranzmaneEndpointRes> {
  constructor(private httpClient:HttpClient) {}

  Akcija(req: void): Observable<GetAllAranzmaneEndpointRes> {
    let url = Config.adresaServera + "Aranzmani/GetZaSobu?Id=" + Navigator.trenutniIdSobe
    return this.httpClient.get<GetAllAranzmaneEndpointRes>(url);
  }

}
