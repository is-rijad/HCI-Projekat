import {AranzmanSobaModel} from "./aranzmanSobaModel";
import {CijenaModel} from "./cijenaModel";
import {KrevetSobaModel} from "./krevetSobaModel";

export interface SobaModel {
  id: number;
  brojSlika: number;
  nazivSobe: string;
  brojGostiju: number;
  opis: string;
  djecaDo: number;
  cijenaZaDjecu: number;

  besplatnoOtkazivanje: boolean;
  klima: boolean;
  bazen: boolean;
  spa: boolean;
  prilagodjenInvalidima: boolean;
  teretana: boolean;
  dozvoljeniLjubimci: boolean;
  minibar: boolean;
  balkon: boolean;

  kreveti: KrevetSobaModel[];
  aranzmani: AranzmanSobaModel[];
  cijene: CijenaModel[];
}
