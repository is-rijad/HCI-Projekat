import {BaseEndpoint} from "../../base-endpoint";
import {GetAllAranzmaneZaSobuEndpointRes} from "./get-all-aranzmane-za-sobu-endpoint-res";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Config} from "../../../config";
import {Navigator} from "../../../navigator";

@Injectable()
export class GetAllAranzmaneZaSobuEndpoint implements BaseEndpoint<void, GetAllAranzmaneZaSobuEndpointRes> {
  constructor(private httpClient: HttpClient) {
  }

  Akcija(): Observable<GetAllAranzmaneZaSobuEndpointRes> {
    let url = Config.adresaServera + "Aranzmani/GetZaSobu?id=" + Navigator.trenutniIdSobe;
    return this.httpClient.get<GetAllAranzmaneZaSobuEndpointRes>(url);
  }

}
