import {BaseEndpoint} from "../../base-endpoint";
import {GetAllAranzmaneZaSobuEndpointRes} from "./get-all-aranzmane-za-sobu-endpoint-res";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export class GetAllAranzmaneZaSobuEndpoint implements BaseEndpoint<void, GetAllAranzmaneZaSobuEndpointRes> {
  constructor(private httpClient:HttpClient) {}

  Akcija(url: string, req: void): Observable<GetAllAranzmaneZaSobuEndpointRes> {
    return this.httpClient.get<GetAllAranzmaneZaSobuEndpointRes>(url);
  }

}
