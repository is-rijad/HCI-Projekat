import { Observable } from "rxjs";
import {BaseEndpoint} from "../../base-endpoint";
import {GetAllKreveteEnpointRes} from "./get-all-krevete-enpoint-res";
import {Config} from "../../../config";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export class GetAllKreveteEnpoint implements BaseEndpoint<void, GetAllKreveteEnpointRes> {
  constructor(private httpClient:HttpClient) {
  }
  Akcija(req: void): Observable<GetAllKreveteEnpointRes> {
      let url = Config.adresaServera + "Kreveti/GetAll"
      return this.httpClient.get<GetAllKreveteEnpointRes>(url)
  }
}
