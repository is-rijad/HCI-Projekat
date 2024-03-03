import {BaseEndpoint} from "../../base-endpoint";
import {GetAllCijeneZaSobuEndpointRes} from "./get-all-cijene-za-sobu-endpoint-res";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Config} from "../../../config";
import {Navigator} from "../../../navigator";

@Injectable()
export class GetAllCijeneZaSobuEndpoint implements BaseEndpoint<number, GetAllCijeneZaSobuEndpointRes> {
  constructor(private httpClient: HttpClient) {
  }

  Akcija(brojGostiju: number): Observable<GetAllCijeneZaSobuEndpointRes> {
    let url = Config.adresaServera + "Cijene/GetCijenuZaId?Id=" + Navigator.trenutniIdSobe + "&BrojOsoba=" + brojGostiju;
    return this.httpClient.get<GetAllCijeneZaSobuEndpointRes>(url);
  }

}
