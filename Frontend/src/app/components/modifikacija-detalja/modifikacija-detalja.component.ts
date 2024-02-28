import {
  Component,
  OnInit
} from '@angular/core';
import {HttpClientModule} from "@angular/common/http";
import {ModifikacijaEndpoint} from "../../endpoints/modifikacija-detalja-endpoint/modifikacija-endpoint";
import {SobaModel} from "../../models/sobaModel";
import {OtvoriDetaljeEndpoint} from "../../endpoints/otvori-detalje-endpoint/otvori-detalje-endpoint";
import {FormsModule} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {ModifikacijaEndpointReq} from "../../endpoints/modifikacija-detalja-endpoint/modifikacija-endpoint-req";
import {Navigator} from "../../navigator";
import {Slike} from "../../slike";
import {HandlerSlika} from "../../handlerSlika";
import {GetAllAranzmaneEndpoint} from "../../endpoints/aranzmani-endpoint/get-all-aranzmane/get-all-aranzmane-endpoint";
import {GetAllKreveteEnpoint} from "../../endpoints/kreveti-endpoint/get-all-krevete/get-all-krevete-enpoint";
import {
  GetAllCijeneZaSobuEndpoint
} from "../../endpoints/aranzmani-endpoint/get-cijene-za-sobu/get-all-cijene-za-sobu-endpoint";
import {Alert, TipAlerta} from "../../alert";
import {
  GetAllAranzmaneZaSobuEndpoint
} from "../../endpoints/aranzmani-endpoint/get-aranzmane-za-sobu/get-all-aranzmane-za-sobu-endpoint";
import {
  GetAllKreveteZaSobuEndpointRes
} from "../../endpoints/kreveti-endpoint/get-all-krevete-za-sobu/get-all-krevete-za-sobu-endpoint-res";
import {
  GetAllKreveteZaSobuEndpoint
} from "../../endpoints/kreveti-endpoint/get-all-krevete-za-sobu/get-all-krevete-za-sobu-endpoint";

@Component({
  selector: 'app-modifikacija-detalja',
  standalone: true,
  imports: [
    HttpClientModule,
    FormsModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './modifikacija-detalja.component.html',
  styleUrl: './modifikacija-detalja.component.css',
  providers: [
    ModifikacijaEndpoint,
    OtvoriDetaljeEndpoint,
    HandlerSlika,
    GetAllKreveteZaSobuEndpoint,
    GetAllAranzmaneZaSobuEndpoint,
    GetAllCijeneZaSobuEndpoint
  ]
})
export class ModifikacijaDetaljaComponent implements OnInit {
  soba: SobaModel = {aranzmani: [], cijene: [], kreveti: [], balkon: false, bazen: false, besplatnoOtkazivanje: false, brojGostiju: 0, cijenaZaDjecu: 0, djecaDo: 0, dozvoljeniLjubimci: false, id: 0, klima: false, minibar: false, nazivSobe: "", opis: "", prilagodjenInvalidima: false, brojSlika: 0, spa: false, teretana: false};
  protected sviKrevetiUcitani: boolean = false;
  protected sviAranzmaniUcitani: boolean = false;
  protected sveCijeneUcitane: boolean = false;
  constructor(private modifikacijaEndpoint: ModifikacijaEndpoint,
              private otvoriDetaljeEndpoint: OtvoriDetaljeEndpoint,
              private getAllAranzmaneEndpoint:GetAllAranzmaneZaSobuEndpoint,
              private getAllKreveteEnpoint:GetAllKreveteZaSobuEndpoint,
              private getAllCijeneZaSobuEndpoint:GetAllCijeneZaSobuEndpoint,
              protected navigator : Navigator,
              protected handlerSlika:HandlerSlika) {
  }


  ngOnInit(): void {
    this.soba.brojSlika = Slike.nizSlika.length;

    if(Navigator.trenutniIdSobe != 0) {
      this.otvoriDetaljeEndpoint.Akcija().subscribe({
        next: res => {
          this.soba = res.soba;
        }
      })
    }
      this.getAllAranzmaneEndpoint.Akcija().subscribe({
        next: res => {
          this.soba.aranzmani = res.aranzmani
          this.sviAranzmaniUcitani = true;
        },
        complete: () => this.aranzmaniUcitani()
      })
      this.getAllKreveteEnpoint.Akcija().subscribe({
        next: res => {
          this.soba.kreveti = res.kreveti
        },
        complete: () => this.sviKrevetiUcitani = true
      })
      this.getAllCijeneZaSobuEndpoint.Akcija().subscribe({
        next: res => {
          this.soba.cijene = res.cijene
          },
        complete: () => this.sveCijeneUcitane = true
      })
    }

  private async aranzmaniUcitani() {
    this.soba.aranzmani = this.soba.aranzmani.sort((a, b) => b.doplata - a.doplata)
    while (document.getElementsByClassName("aranzman-check").length < this.soba.aranzmani.length) {
          await new Promise(r => setTimeout(r, 500));
      }
      for (let i = 0; i < this.soba.aranzmani.length; i++) {
          let aranzmanCheck = document.getElementById(`aranzman-${this.soba.aranzmani[i].aranzmanId}`) as HTMLInputElement;
          let aranzmanDoplata = document.getElementById(`doplata-${this.soba.aranzmani[i].aranzmanId}`) as HTMLInputElement;

          if (this.soba.aranzmani[i].doplata > 0)
              aranzmanCheck.checked = true;
          else
              aranzmanDoplata.disabled = true;

          aranzmanCheck?.addEventListener("click", (event) => {
              aranzmanDoplata.disabled = !aranzmanDoplata.disabled;
          })
      }
      this.sviAranzmaniUcitani = true;
  }

    spremiPromjene() {
      this.soba.aranzmani.forEach(a => a.id = null);
        this.soba.cijene.forEach(c => c.id = null);
        this.soba.kreveti.forEach(k => k.id = null);
      let request : ModifikacijaEndpointReq = {
          aranzmani: this.soba.aranzmani.filter(a => a.doplata > 0),
          cijene: this.soba.cijene.filter(c => c.cijenaSobe > 0),
          kreveti: this.soba.kreveti.filter(k => k.brojKreveta > 0),
          soba: this.soba
      }
      this.modifikacijaEndpoint.Akcija(request).subscribe({
          next: (res) => {
            if (res.status == 200) {
              Alert.alert = new Alert(TipAlerta.success, "Promjene su uspješno spremljene")
            }
          }
      })
    }

  protected readonly Slike = Slike;
}
