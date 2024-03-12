import {SobaModel} from "./sobaModel";
import {AranzmanSobaModel} from "./aranzmanSobaModel";

export interface ZauzetaSobaModel {
  id: number;
  sobaId: number;
  soba?: SobaModel;
  gostId: number;
  sobaAranzmanId: number;
  sobaAranzman?: AranzmanSobaModel;
  brojOsoba: number;
  brojDjece: number;
  cijena: number;
  datumDolaska: Date;
  datumOdlaska: Date;
  slikaSobe: string;
}
