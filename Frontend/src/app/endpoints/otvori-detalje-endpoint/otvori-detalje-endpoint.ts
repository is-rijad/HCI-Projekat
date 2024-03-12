import {Config} from "../../config";
import {HttpClient} from "@angular/common/http";
import {BaseEndpoint} from "../base-endpoint";
import {OtvoriDetaljeEndpointRes} from "./otvori-detalje-endpoint-res";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {ActivatedRoute} from "@angular/router";

@Injectable()
export class OtvoriDetaljeEndpoint implements BaseEndpoint<void, OtvoriDetaljeEndpointRes> {
  id: any;

  constructor(private httpClient: HttpClient,
              private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(value => {
      this.id = value['id']
    })
  }

  Akcija(): Observable<OtvoriDetaljeEndpointRes> {
    let url = Config.adresaServera + "Sobe/GetSobuId/?Id=" + this.id;
    return this.httpClient.get<OtvoriDetaljeEndpointRes>(url);
  }
}
