import {KrevetModel} from "./krevetModel";

export interface KrevetSobaModel {
  id: number | null
  sobaId: number
  krevetId: number
  krevet: KrevetModel
  brojKreveta: number
}
