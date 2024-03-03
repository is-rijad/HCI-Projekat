import {AranzmanModel} from "./aranzmanModel";

export interface AranzmanSobaModel {
  id: number | null
  sobaId: number
  aranzmanId: number
  aranzman: AranzmanModel
  doplata: number
}
