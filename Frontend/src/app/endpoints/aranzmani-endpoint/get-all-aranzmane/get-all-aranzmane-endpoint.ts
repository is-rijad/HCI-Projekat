import {BaseEndpoint} from "../../base-endpoint";
import {GetAllAranzmaneEndpointRes} from "./get-all-aranzmane-endpoint-res";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export class GetAllAranzmaneEndpoint implements BaseEndpoint<void, GetAllAranzmaneEndpointRes> {
  constructor(private httpClient:HttpClient) {}

  Akcija(url: string, req: void): Observable<GetAllAranzmaneEndpointRes> {
    return this.httpClient.get<GetAllAranzmaneEndpointRes>(url);
  }

}
