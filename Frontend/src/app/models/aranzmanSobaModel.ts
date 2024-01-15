import {AranzmanModel} from "./aranzmanModel";

export interface AranzmanSobaModel {
  id: number
  sobaId: number
  aranzmanId: number
  aranzman : AranzmanModel
  doplata: number
  isChecked : boolean
}
