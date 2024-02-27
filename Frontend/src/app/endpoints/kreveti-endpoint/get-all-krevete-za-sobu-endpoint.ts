import {BaseEndpoint} from "../base-endpoint";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {GetAllKreveteZaSobuEndpointRes} from "./get-all-krevete-za-sobu-endpoint-res";
import {Config} from "../../config";
import {Navigator} from "../../navigator";

@Injectable()
export class GetAllKreveteZaSobuEndpoint implements BaseEndpoint<void, GetAllKreveteZaSobuEndpointRes> {
  constructor(private httpClient:HttpClient) {}

  Akcija(): Observable<GetAllKreveteZaSobuEndpointRes> {
    let url = Config.adresaServera + "Kreveti/GetZaSobu?id=" + Navigator.trenutniIdSobe;
    return this.httpClient.get<GetAllKreveteZaSobuEndpointRes>(url);
  }

}
