import {BaseEndpoint} from "../../base-endpoint";
import {GetAllCijeneZaSobuEndpointRes} from "./get-all-cijene-za-sobu-endpoint-res";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Config} from "../../../config";
import {Navigator} from "../../../navigator";
import {ActivatedRoute} from "@angular/router";

@Injectable()
export class GetAllCijeneZaSobuEndpoint implements BaseEndpoint<number, GetAllCijeneZaSobuEndpointRes> {
  id:any;
  constructor(private httpClient: HttpClient,
              private activatedRoute:ActivatedRoute) {
    this.activatedRoute.params.subscribe(value => {
      this.id = value['id']
    })
  }

  Akcija(brojGostiju: number): Observable<GetAllCijeneZaSobuEndpointRes> {
    let url = Config.adresaServera + "Cijene/GetCijenuZaId?Id=" + this.id + "&BrojOsoba=" + brojGostiju;
    return this.httpClient.get<GetAllCijeneZaSobuEndpointRes>(url);
  }

}
