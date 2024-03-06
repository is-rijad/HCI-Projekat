import {ZauzetaSobaModel} from "../../../models/zauzetaSobaModel";
import {BaseResponse} from "../../base-response";
import {RezervacijaModel} from "../../../models/rezervacijaModel";

export interface GetBuduceRezervacijeZaGostaRes extends BaseResponse{
  rezervacije:RezervacijaModel[]
}
