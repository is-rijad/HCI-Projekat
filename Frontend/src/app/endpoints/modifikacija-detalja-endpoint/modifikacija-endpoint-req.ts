import {SobaModel} from "../../models/sobaModel";
import {KrevetSobaModel} from "../../models/krevetSobaModel";
import {AranzmanSobaModel} from "../../models/aranzmanSobaModel";
import {CijenaModel} from "../../models/cijenaModel";

export interface ModifikacijaEndpointReq {
  soba: SobaModel
  kreveti: KrevetSobaModel[]
  aranzmani: AranzmanSobaModel[]
  cijene: CijenaModel[]
}
