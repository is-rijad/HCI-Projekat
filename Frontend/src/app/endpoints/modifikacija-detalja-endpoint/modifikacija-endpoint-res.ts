import {BaseResponse} from "../base-response";
import {SobaModel} from "../../models/sobaModel";

export class ModifikacijaEndpointRes extends BaseResponse {
  soba: SobaModel | null = null;
}

