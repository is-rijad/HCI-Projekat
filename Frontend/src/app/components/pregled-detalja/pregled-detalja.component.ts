import {Component, forwardRef, OnInit} from '@angular/core';
import {OtvoriDetaljeEndpoint} from "../../endpoints/otvori-detalje-endpoint/otvori-detalje-endpoint";
import {Config} from "../../config";
import {Router} from "@angular/router";
import {OtvoriDetaljeEndpointRes} from "../../endpoints/otvori-detalje-endpoint/otvori-detalje-endpoint-res";
import {HttpClientModule} from "@angular/common/http";
import {Navigator} from "../../navigator";
import {SobaModel} from "../../models/sobaModel";
import {NgForOf, NgIf} from "@angular/common";
import {FormControl, FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule} from "@angular/forms";
import {
  GetAllAranzmaneZaSobuEndpoint
} from "../../endpoints/aranzmani-endpoint/get-aranzmane-za-sobu/get-all-aranzmane-za-sobu-endpoint";
import {GetAllAranzmaneEndpoint} from "../../endpoints/aranzmani-endpoint/get-all-aranzmane/get-all-aranzmane-endpoint";
import {GetAllKreveteZaSobuEndpoint} from "../../endpoints/kreveti-endpoint/get-all-krevete-za-sobu/get-all-krevete-za-sobu-endpoint";
import {
  GetAllCijeneZaSobuEndpoint
} from "../../endpoints/aranzmani-endpoint/get-cijene-za-sobu/get-all-cijene-za-sobu-endpoint";
import {AranzmanSobaModel} from "../../models/aranzmanSobaModel";
import {KrevetSobaModel} from "../../models/krevetSobaModel";
import {CijenaModel} from "../../models/cijenaModel";
import {delay} from "rxjs";
import {Slike} from "../../slike";
import {HandlerSlika} from "../../handlerSlika";

@Component({
  selector: 'app-pregled-detalja',
  standalone: true,
  imports: [HttpClientModule, NgForOf, ReactiveFormsModule, FormsModule, NgIf],
  templateUrl: './pregled-detalja.component.html',
  styleUrl: './pregled-detalja.component.css',
  providers: [
    OtvoriDetaljeEndpoint,
    HandlerSlika
  ]
})
export class PregledDetaljaComponent implements OnInit{
  soba : SobaModel = {aranzmani: [], cijene: [], kreveti: [], balkon: false, bazen: false, besplatnoOtkazivanje: false, brojGostiju: 0, cijenaZaDjecu: 0, djecaDo: 0, dozvoljeniLjubimci: false, id: 0, klima: false, minibar: false, nazivSobe: "", opis: "", prilagodjenInvalidima: false, brojSlika: Slike.nizSlika.length, spa: false, teretana: false};
  brojOdraslih: number = 0;
  brojDjece: number = 0;
  aranzmanDoplata : any;
  cijenaSobe = 0;
  datumPrijave : Date = new Date();
  datumOdjave : Date = new Date(new Date().setDate(this.datumPrijave.getDate() + 1));
  datumPrijaveElement:any;
  datumOdjaveElement:any;
    constructor(private otvoriDetaljeEndpoint:OtvoriDetaljeEndpoint,
              protected handlerSlika:HandlerSlika) {
  }
  ngOnInit(): void {

    this.datumPrijaveElement = (document.getElementById("datum-prijave") as HTMLInputElement);
    this.datumOdjaveElement = (document.getElementById("datum-odjave") as HTMLInputElement);
    this.datumPrijaveElement.valueAsDate = this.datumPrijave;
    this.datumOdjaveElement.valueAsDate = this.datumOdjave;


    this.otvoriDetaljeEndpoint.Akcija().subscribe({
      next: res => {
        this.soba = res.soba;
      },
      complete: () => {
        this.cijeneUcitane();
        let aranzmaniSelect = document.getElementById("aranzman");
        aranzmaniSelect!.innerHTML = "";
        for (let aranzmanSobaModel of this.soba.aranzmani) {
          if(this.soba.aranzmani.indexOf(aranzmanSobaModel) == 0) {
            aranzmaniSelect!.innerHTML += `
                <option selected value='${aranzmanSobaModel.doplata}'>${aranzmanSobaModel.aranzman.nazivAranzmana}</option>`
            }
          else {
            aranzmaniSelect!.innerHTML += `
                <option value='${aranzmanSobaModel.doplata}'>${aranzmanSobaModel.aranzman.nazivAranzmana}</option>`;
          }
        }
        aranzmaniSelect!.innerHTML += `
                <option value='0'>Bez aranžmana</option>`
      }
    })

    window.addEventListener("keyup", (event) => {
      if(this.handlerSlika.isModalVidljiv) {
        if(event.key == "Escape") {
          this.handlerSlika.isModalVidljiv = !this.handlerSlika.isModalVidljiv;
        }
        else if (event.key == "ArrowLeft") {
          this.handlerSlika.promijeniSliku(-1);
        }

        else if (event.key == "ArrowRight") {
          this.handlerSlika.promijeniSliku(1);
        }
      }

    })
  }

  protected izracunajCijenu() {
    this.aranzmanDoplata = (document.getElementById("aranzman") as HTMLSelectElement).value;
    let cijenaZaOdrasle = this.soba.cijene.find(c => c.brojOsoba == this.brojOdraslih)?.cijenaSobe;
    if(cijenaZaOdrasle == undefined) return;

    let cijenaZaDjecu = this.soba.cijenaZaDjecu * this.brojDjece

    this.datumPrijave = this.datumPrijaveElement.valueAsDate;
    this.datumOdjave = this.datumOdjaveElement.valueAsDate;

    let brojNoci = this.datediff(this.datumPrijave.valueOf(), this.datumOdjave.valueOf())

    this.cijenaSobe = (((cijenaZaOdrasle + cijenaZaDjecu) * ((this.aranzmanDoplata / 100) + 1)) * brojNoci);

    if (isNaN(this.cijenaSobe))
      this.cijenaSobe = 0
  }
  datediff(first : number, second : number) {
    return Math.round((second - first) / (1000 * 60 * 60 * 24));
  }
  private async cijeneUcitane() {
    while (this.soba.cijene.length < this.soba.brojGostiju) {
      await new Promise(() => setTimeout(() => delay(2000)))
    }
    this.izracunajCijenu();
  }

  protected readonly Slike = Slike;
}
