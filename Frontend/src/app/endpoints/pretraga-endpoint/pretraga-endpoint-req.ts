import {SobaModel} from "../../models/sobaModel";

export interface PretragaEndpointReq {
  datumPrijave: Date;
  datumOdjave: Date;
  brojOdraslih: number;
  brojDjece: number;

  aranzmanId: number;
  brojBracnihKreveta: number;
  brojObicnihKreveta: number;
  brojDjecjihKreveta: number;
  filterPoCijeni: number;

  besplatnoOtkazivanje: boolean;
  klima: boolean;
  bazen: boolean;
  spa: boolean;
  prilagodjenInvalidima: boolean;
  teretana: boolean;
  dozvoljeniLjubimci: boolean;
  minibar: boolean;
  balkon: boolean;
}
