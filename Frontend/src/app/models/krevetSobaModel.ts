import {KrevetModel} from "./krevetModel";

export interface KrevetSobaModel {
    id: number
    sobaId: number
    krevetId: number
    krevet: KrevetModel
    brojKreveta: number
}
