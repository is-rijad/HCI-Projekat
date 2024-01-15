import {BaseEndpoint} from "../../base-endpoint";
import {GetAllCijeneZaSobuEndpointRes} from "./get-all-cijene-za-sobu-endpoint-res";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export class GetAllCijeneZaSobuEndpoint implements BaseEndpoint<void, GetAllCijeneZaSobuEndpointRes> {
  constructor(private httpClient:HttpClient) {}

  Akcija(url: string, req: void): Observable<GetAllCijeneZaSobuEndpointRes> {
    return this.httpClient.get<GetAllCijeneZaSobuEndpointRes>(url);
  }

}
