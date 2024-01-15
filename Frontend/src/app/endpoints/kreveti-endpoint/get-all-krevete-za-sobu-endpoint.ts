import {BaseEndpoint} from "../base-endpoint";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {GetAllKreveteZaSobuEndpointRes} from "./get-all-krevete-za-sobu-endpoint-res";

@Injectable()
export class GetAllKreveteZaSobuEndpoint implements BaseEndpoint<void, GetAllKreveteZaSobuEndpointRes> {
  constructor(private httpClient:HttpClient) {}

  Akcija(url: string, req: void): Observable<GetAllKreveteZaSobuEndpointRes> {
    return this.httpClient.get<GetAllKreveteZaSobuEndpointRes>(url);
  }

}
