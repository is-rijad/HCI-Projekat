import {Injectable} from "@angular/core";
import {BaseEndpoint} from "../base-endpoint";
import {PretragaEndpointReq} from "../pretraga-endpoint/pretraga-endpoint-req";
import {PretragaEndpointRes} from "../pretraga-endpoint/pretraga-endpoint-res";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {GetSlikeZaSobuEndpointReq} from "./getSlikeZaSobuEndpointReq";
import {Config} from "../../config";
import {GetSlikeZaSobuEndpointRes} from "./getSlikeZaSobuEndpointRes";

@Injectable()
export class GetSlikeZaSobuEndpoint implements BaseEndpoint<GetSlikeZaSobuEndpointReq, GetSlikeZaSobuEndpointRes>{
  constructor(private httpClient : HttpClient) {}

  Akcija(req: GetSlikeZaSobuEndpointReq): Observable<GetSlikeZaSobuEndpointRes> {
    let url = Config.adresaServera + "Soba/GetSlikeZaSobu?SobaId=" + req.sobaId;
    return this.httpClient.get<GetSlikeZaSobuEndpointRes>(url);
  }
}
