import {SobaModel} from "../../../models/sobaModel";
import {AranzmanSobaModel} from "../../../models/aranzmanSobaModel";
import {ZauzetaSobaModel} from "../../../models/zauzetaSobaModel";
import {BaseResponse} from "../../base-response";

export interface ProvjeriRezervacijuEndpointRes extends BaseResponse {
  detaljiRezervacije: ZauzetaSobaModel
  soba: SobaModel
  sobaAranzman: AranzmanSobaModel
}
