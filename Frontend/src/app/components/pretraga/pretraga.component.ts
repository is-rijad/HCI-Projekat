import {AfterContentInit, Component, OnInit} from '@angular/core';
import {PretragaEndpointRes, PretragaEndpointResSoba} from "../../endpoints/pretraga-endpoint/pretraga-endpoint-res";
import {NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {Router} from "@angular/router";
import {PretragaEndpoint} from "../../endpoints/pretraga-endpoint/pretraga-endpoint";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {PretragaEndpointReq} from "../../endpoints/pretraga-endpoint/pretraga-endpoint-req";
import {Config} from "../../config";
import {Navigator} from "../../navigator";
import {Slike} from "../../slike";

@Component({
  selector: 'app-pretraga',
  standalone: true,
  imports: [
    NgForOf,
    FormsModule,
    NgIf,
    HttpClientModule,
    NgOptimizedImage
  ],
  providers: [
    PretragaEndpoint,
    Navigator
  ],
  templateUrl: './pretraga.component.html',
  styleUrl: './pretraga.component.css'
})
export class PretragaComponent implements OnInit{
  dostupneSobe : PretragaEndpointResSoba[] | null = null;
  filtriranjeUpaljeno : boolean = false;

  datumPrijave: Date = new Date();
  datumOdjave: Date = new Date();
  brojOdraslih: number = 0;
  brojDjece: number = 0;

  besplatnoOtkazivanje: boolean = true;
  klima: boolean = true;
  bazen: boolean = true;
  spa: boolean = true;
  prilagodjenInvalidima: boolean = true;
  teretana: boolean = true;
  dozvoljeniLjubimci: boolean = true;
  minibar: boolean = true;
  balkon: boolean = true;

  constructor(private pretragaEndpoint : PretragaEndpoint,
              private navigator : Navigator) {
  }
  ngOnInit(): void {
    let req :PretragaEndpointReq = {

    }
    this.pretragaEndpoint.Akcija(req).subscribe({
      next: res => {
         this.dostupneSobe = res.sobe;
      }
    })
    let elementi = document.getElementsByClassName("lijevi-panel-button")!;
    elementi[0].classList.add("active");
    for (let i = 0; i < elementi.length; i++) {
      elementi[i].addEventListener("click", (event : Event) => {
        if (this.filtriranjeUpaljeno) {
          elementi[1].classList.remove("active");
          elementi[0].classList.add("active");
        }
        else {
          elementi[0].classList.remove("active");
          elementi[1].classList.add("active");
        }
        this.filtriranjeUpaljeno = !this.filtriranjeUpaljeno;
      })
    }
  }

  async otvoriDetalje(id: number) {
    Navigator.trenutniIdSobe = id;
    await this.navigator.navigiraj('modifikacija', [id])
  }

  protected readonly Slike = Slike;
}
