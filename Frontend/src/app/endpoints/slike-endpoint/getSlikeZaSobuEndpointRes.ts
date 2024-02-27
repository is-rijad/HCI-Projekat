import {BaseResponse} from "../base-response";
import {SlikaModel} from "../../models/slikaModel";

export class GetSlikeZaSobuEndpointRes extends BaseResponse {
  slike : SlikaModel[] = [];
}
