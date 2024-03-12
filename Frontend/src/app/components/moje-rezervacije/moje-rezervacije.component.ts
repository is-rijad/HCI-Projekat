import {Component, OnInit} from '@angular/core';
import {ZauzetaSobaModel} from "../../models/zauzetaSobaModel";
import {DatePipe, NgForOf} from "@angular/common";
import {
  GetBuduceRezervacijeZaGostaRes
} from "../../endpoints/rezervacije-endpoint/get-buduce-rezervacije-za-gosta/get-buduce-rezervacije-za-gosta-res";
import {
  GetBuduceRezervacijeZaGostaEndpoint
} from "../../endpoints/rezervacije-endpoint/get-buduce-rezervacije-za-gosta/get-buduce-rezervacije-za-gosta-endpoint";
import {
  GetPrethodneRezervacijeZaGostaEndpoint
} from "../../endpoints/rezervacije-endpoint/get-prethodne-rezervacije-za-gosta/get-prethodne-rezervacije-za-gosta-endpoint";
import {Slike} from "../../slike";
import {HttpClientModule} from "@angular/common/http";
import {RezervacijaModel} from "../../models/rezervacijaModel";
import {Modal} from "../../modal";
import {
  OtkaziRezervacijuEnpoint
} from "../../endpoints/rezervacije-endpoint/otkazi-rezervaciju/otkazi-rezervaciju-enpoint";
import {OtkaziRezervacijuReq} from "../../endpoints/rezervacije-endpoint/otkazi-rezervaciju/otkazi-rezervaciju-req";
import {Alert, TipAlerta} from "../../alert";

@Component({
  selector: 'app-moje-rezervacije',
  standalone: true,
  imports: [
    NgForOf,
    HttpClientModule,
    DatePipe
  ],
  templateUrl: './moje-rezervacije.component.html',
  styleUrl: './moje-rezervacije.component.css',
  providers: [
    GetBuduceRezervacijeZaGostaEndpoint,
    GetPrethodneRezervacijeZaGostaEndpoint,
    OtkaziRezervacijuEnpoint
  ]
})
export class MojeRezervacijeComponent implements OnInit{
  buduceRezervacijeUpaljene: boolean = true;
  buduceRezervacije: RezervacijaModel[] = [];
  prosleRezervacije: RezervacijaModel[] = [];

  constructor(private getBuduceRezervacijeZaGostaEndpoint:GetBuduceRezervacijeZaGostaEndpoint,
              private getPrethodneRezervacijeZaGostaEndpoint:GetPrethodneRezervacijeZaGostaEndpoint,
              private modal:Modal,
              private otkaziRezervacijuEnpoint:OtkaziRezervacijuEnpoint) {
  }

  ngOnInit(): void {
    this.getBuduceRezervacije();
    this.getPrethodneRezervacije();
    let elementi = document.getElementsByClassName("kartica-button")!;
    elementi[0].classList.add("active");
    for (let i = 0; i < elementi.length; i++) {
      elementi[i].addEventListener("click", (event: Event) => {
        if (this.buduceRezervacijeUpaljene) {
          elementi[0].classList.remove("active");
          elementi[1].classList.add("active");
          document.getElementById("panel-prethodne-rezervacije")!.style.display = "block"
          document.getElementById("panel-buduce-rezervacije")!.style.display = "none"
        } else {
          elementi[1].classList.remove("active");
          elementi[0].classList.add("active");
          document.getElementById("panel-prethodne-rezervacije")!.style.display = 'none'
          document.getElementById("panel-buduce-rezervacije")!.style.display = "block"

        }
        this.buduceRezervacijeUpaljene = !this.buduceRezervacijeUpaljene;
      })
    }
  }
  private getBuduceRezervacije() {
    this.getBuduceRezervacijeZaGostaEndpoint.Akcija().subscribe({
      next: res => {
        this.buduceRezervacije = res.rezervacije
      },
      complete: () => this.buduceRezervacije.forEach(br => br.rezervacija.slikaSobe = Slike.getRandomSliku())
    })
  }
  private getPrethodneRezervacije() {
    this.getPrethodneRezervacijeZaGostaEndpoint.Akcija().subscribe({
      next: res => {
        this.prosleRezervacije = res.rezervacije
      },
      complete: () => this.prosleRezervacije.forEach(br => br.rezervacija.slikaSobe = Slike.getRandomSliku())
    })
  }
  protected async otkaziRezervaciju(id: number, naziv: string) {
    this.modal.napraviDijalog(`Da li ste sigurni da želite otkazati ${naziv} rezervaciju`);
    while (this.modal.potvdna == null) {
      await new Promise(r => setTimeout(r, 200))
    }
    if(this.modal.potvdna) {
      let req:OtkaziRezervacijuReq = {
        rezervacijaId:id
      };
      this.otkaziRezervacijuEnpoint.Akcija(req).subscribe({
        next: res => {
          if(res.status == 200)
            Alert.alert = new Alert(TipAlerta.success, res.message);
          else
            Alert.alert = new Alert(TipAlerta.error, res.message);
        },
        complete: () => this.getBuduceRezervacije()
      })
    }
  }
}
