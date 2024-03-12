import {BaseEndpoint} from "../../base-endpoint";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {GetAllKreveteZaSobuEndpointRes} from "./get-all-krevete-za-sobu-endpoint-res";
import {Config} from "../../../config";
import {ActivatedRoute} from "@angular/router";

@Injectable()
export class GetAllKreveteZaSobuEndpoint implements BaseEndpoint<void, GetAllKreveteZaSobuEndpointRes> {
  id: any;

  constructor(private httpClient: HttpClient,
              private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(value => {
      this.id = value['id']
    })
  }

  Akcija(): Observable<GetAllKreveteZaSobuEndpointRes> {
    let url = Config.adresaServera + "Kreveti/GetZaSobu?id=" + this.id;
    return this.httpClient.get<GetAllKreveteZaSobuEndpointRes>(url);
  }

}
