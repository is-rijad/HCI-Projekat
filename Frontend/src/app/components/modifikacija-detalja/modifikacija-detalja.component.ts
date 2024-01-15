import {
  AfterContentChecked,
  AfterContentInit,
  AfterViewChecked,
  AfterViewInit,
  Component,
  NgIterable,
  OnInit
} from '@angular/core';
import {HttpClientModule} from "@angular/common/http";
import {ModifikacijaEndpoint} from "../../endpoints/modifikacija-detalja-endpoint/modifikacija-endpoint";
import {SobaModel} from "../../models/sobaModel";
import {Config} from "../../config";
import {Navigator} from "../../navigator";
import {OtvoriDetaljeEndpoint} from "../../endpoints/otvori-detalje-endpoint/otvori-detalje-endpoint";
import {FormsModule} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";
import {Router} from "@angular/router";
import {AranzmanModel} from "../../models/aranzmanModel";
import {GetAllAranzmaneEndpoint} from "../../endpoints/aranzmani-endpoint/get-all-aranzmane/get-all-aranzmane-endpoint";
import {KrevetModel} from "../../models/krevetModel";
import {
  GetAllAranzmaneZaSobuEndpoint
} from "../../endpoints/aranzmani-endpoint/get-aranzmane-za-sobu/get-all-aranzmane-za-sobu-endpoint";
import {AranzmanSobaModel} from "../../models/aranzmanSobaModel";
import {GetAllKreveteZaSobuEndpoint} from "../../endpoints/kreveti-endpoint/get-all-krevete-za-sobu-endpoint";
import {KrevetSobaModel} from "../../models/krevetSobaModel";
import {CijenaModel} from "../../models/cijenaModel";
import {
  GetAllCijeneZaSobuEndpoint
} from "../../endpoints/aranzmani-endpoint/get-cijene-za-sobu/get-all-cijene-za-sobu-endpoint";

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
    GetAllKreveteZaSobuEndpoint,
    GetAllAranzmaneZaSobuEndpoint,
    GetAllCijeneZaSobuEndpoint]
})
export class ModifikacijaDetaljaComponent implements OnInit, AfterContentInit{
  soba : SobaModel | null = null;
  aranzmani : AranzmanSobaModel[] | null = null;
  kreveti : KrevetSobaModel[] | null = null;
  cijene : CijenaModel[] | null = null;
  constructor(private modifikacijaEndpoint: ModifikacijaEndpoint,
              private otvoriDetaljeEndpoint:OtvoriDetaljeEndpoint,
              private getAllKreveteZaSobuEndpoint:GetAllKreveteZaSobuEndpoint,
              private getAllAranzmaneZaSobuEndpoint:GetAllAranzmaneZaSobuEndpoint,
              private getAllCijeneZaSobuEndpoint : GetAllCijeneZaSobuEndpoint,
              private router : Router) {
  }

  ngAfterContentInit() : void {

  }

  ngOnInit(): void {
    let routerUrl = this.router.routerState.snapshot.url;
    let sobaId =routerUrl.charAt(routerUrl.length-1);
    let url = Config.adresaServera + "Sobe/GetSobuId/?Id=" + sobaId;
    this.otvoriDetaljeEndpoint.Akcija(url).subscribe({
      next: res => {
        this.soba = res.soba;
      }
    })
    url = Config.adresaServera + "Kreveti/GetZaSobu?id=" + sobaId;
    this.getAllKreveteZaSobuEndpoint.Akcija(url).subscribe({
      next: res => {
        this.kreveti = res.kreveti
      }
    })

    url = Config.adresaServera + "Aranzmani/GetZaSobu?id=" + sobaId;
    this.getAllAranzmaneZaSobuEndpoint.Akcija(url).subscribe({
      next: res => {
        this.aranzmani = res.aranzmani;
      }
    })

    url = Config.adresaServera + "Cijene/GetZaSobu?id=" + sobaId;
    this.getAllCijeneZaSobuEndpoint.Akcija(url).subscribe({
      next: res => {
        this.cijene = res.cijene;
      }
    })

    for (let i = this.cijene?.length!; i > 0; i++) {
      document.getElementById("cijena-items")!.innerHTML += `
        <div class="cijena-item">
          <label for="cijena-za-${this.cijene![i].brojOsoba}">Cijena za ${this.cijene![i].brojOsoba} osoba:</label>
          <input value="${this.cijene![i].cijenaSobe}" class="form-control" type="number" id="cijena-za-${this.cijene![i].brojOsoba}">
      </div>`
    }

    // for (let i = 0; i < this.aranzmani!.length; i++) {
    //   if (this.aranzmani![i].doplata > 0)
    //     this.aranzmani![i].isChecked = true;
    // }
    // for (let i = 0; i < this.aranzmani!.length; i++) {
    //   let aranzmanElement = document.getElementById("aranzman-" + this.aranzmani![i].aranzmanId) as HTMLInputElement;
    //   aranzmanElement?.addEventListener("click", (event) => {
    //     this.aranzmani![i].isChecked = !this.aranzmani![i].isChecked;
    //   })
    // }





  }

}
