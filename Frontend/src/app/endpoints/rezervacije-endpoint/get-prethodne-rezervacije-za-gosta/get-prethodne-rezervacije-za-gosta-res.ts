import {BaseResponse} from "../../base-response";
import {RezervacijaModel} from "../../../models/rezervacijaModel";

export interface GetPrethodneRezervacijeZaGostaRes extends BaseResponse {
  rezervacije: RezervacijaModel[]
}
