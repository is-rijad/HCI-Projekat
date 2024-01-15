import {KrevetModel} from "./krevetModel";
import {AranzmanModel} from "./aranzmanModel";
import {AranzmanSobaModel} from "./aranzmanSobaModel";

export interface SobaModel {
  id: number;
  slike: string;
  nazivSobe: string;
  brojGostiju: number;
  opis: string;

  besplatnoOtkazivanje: boolean;
  klima: boolean;
  bazen: boolean;
  spa: boolean;
  prilagodjenInvalidima: boolean;
  teretana: boolean;
  dozvoljeniLjubimci: boolean;
  minibar: boolean;
  balkon: boolean;

  kreveti:KrevetModel[];
  aranzmani:AranzmanSobaModel[];
}
