import {BaseEndpoint} from "../../base-endpoint";
import {GetAllCijeneZaSobuEndpointRes} from "./get-all-cijene-za-sobu-endpoint-res";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Config} from "../../../config";
import {Navigator} from "../../../navigator";

@Injectable()
export class GetAllCijeneZaSobuEndpoint implements BaseEndpoint<void, GetAllCijeneZaSobuEndpointRes> {
  constructor(private httpClient:HttpClient) {}

  Akcija(): Observable<GetAllCijeneZaSobuEndpointRes> {
    let url = Config.adresaServera + "Cijene/GetCijenuZaId?Id=" + Navigator.trenutniIdSobe;
    return this.httpClient.get<GetAllCijeneZaSobuEndpointRes>(url);
  }

}
