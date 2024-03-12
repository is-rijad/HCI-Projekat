import {BaseEndpoint} from "../../base-endpoint";
import {GetAllAranzmaneZaSobuEndpointRes} from "./get-all-aranzmane-za-sobu-endpoint-res";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Config} from "../../../config";
import {Navigator} from "../../../navigator";
import {ActivatedRoute} from "@angular/router";

@Injectable()
export class GetAllAranzmaneZaSobuEndpoint implements BaseEndpoint<void, GetAllAranzmaneZaSobuEndpointRes> {
  id:any;
  constructor(private httpClient: HttpClient,
              private activatedRoute:ActivatedRoute) {
    this.activatedRoute.params.subscribe(value => {
      this.id = value['id']
    })
  }

  Akcija(): Observable<GetAllAranzmaneZaSobuEndpointRes> {
    let url = Config.adresaServera + "Aranzmani/GetZaSobu?id=" + this.id;
    return this.httpClient.get<GetAllAranzmaneZaSobuEndpointRes>(url);
  }

}
