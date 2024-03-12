import {Component, OnInit} from '@angular/core';
import {PretragaEndpointResSoba} from "../../endpoints/pretraga-endpoint/pretraga-endpoint-res";
import {NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {PretragaEndpoint} from "../../endpoints/pretraga-endpoint/pretraga-endpoint";
import {HttpClientModule} from "@angular/common/http";
import {PretragaEndpointReq} from "../../endpoints/pretraga-endpoint/pretraga-endpoint-req";
import {Navigator} from "../../navigator";
import {Slike} from "../../slike";
import {AranzmanModel} from "../../models/aranzmanModel";
import {GetAllAranzmaneEndpoint} from "../../endpoints/aranzmani-endpoint/get-all-aranzmane/get-all-aranzmane-endpoint";
import {AuthServis} from "../../auth-servis";

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
    Navigator,
    GetAllAranzmaneEndpoint
  ],
  templateUrl: './pretraga.component.html',
  styleUrl: './pretraga.component.css'
})
export class PretragaComponent implements OnInit {
  dostupneSobe: PretragaEndpointResSoba[] | null = null;
  filtriranjeUpaljeno: boolean = false;
  aranzmani: AranzmanModel[] = [];

  datumDanas = new Date();
  datumSutra = new Date(new Date().setDate(this.datumDanas.getDate() + 1));
  datumPrijaveElement: any;
  datumOdjaveElement: any;
  podaci: PretragaEndpointReq = {
    aranzmanId: 0,
    balkon: false,
    bazen: false,
    besplatnoOtkazivanje: false,
    brojBracnihKreveta: 0,
    brojDjece: 0,
    brojDjecjihKreveta: 0,
    brojObicnihKreveta: 0,
    brojOdraslih: 1,
    datumOdjave: this.datumDanas,
    datumPrijave: this.datumSutra,
    dozvoljeniLjubimci: false,
    filterPoCijeni: 0,
    klima: false,
    minibar: false,
    prilagodjenInvalidima: false,
    spa: false,
    teretana: false
  };
  protected readonly Slike = Slike;

  constructor(private pretragaEndpoint: PretragaEndpoint,
              private navigator: Navigator,
              private getAllAranzmaneEndpoint: GetAllAranzmaneEndpoint,
              private authServis:AuthServis) {
    this.podaci = (this.navigator.podaci) as PretragaEndpointReq
    if (this.podaci == undefined || Object.keys(this.podaci).length == 0) {
      this.podaci = {
        aranzmanId: 0,
        balkon: false,
        bazen: false,
        besplatnoOtkazivanje: false,
        brojBracnihKreveta: 0,
        brojDjece: 0,
        brojDjecjihKreveta: 0,
        brojObicnihKreveta: 0,
        brojOdraslih: 1,
        datumOdjave: this.datumDanas,
        datumPrijave: this.datumSutra,
        dozvoljeniLjubimci: false,
        filterPoCijeni: 0,
        klima: false,
        minibar: false,
        prilagodjenInvalidima: false,
        spa: false,
        teretana: false
      }
    }
    this.getAllAranzmaneEndpoint.Akcija().subscribe(res => this.aranzmani = res.aranzmani);
  }

  ngOnInit(): void {
    (document.getElementById("filter-po-cijeni") as HTMLSelectElement).value = this.podaci.filterPoCijeni.toString();
    this.datumPrijaveElement = (document.getElementById("datum-prijave") as HTMLInputElement)
    this.datumOdjaveElement = (document.getElementById("datum-odjave") as HTMLInputElement)
    this.datumPrijaveElement = this.datumPrijaveElement as HTMLInputElement;
    this.datumOdjaveElement = this.datumOdjaveElement as HTMLInputElement;
    this.datumPrijaveElement.valueAsDate = this.podaci.datumPrijave;
    this.datumOdjaveElement.valueAsDate = this.podaci.datumOdjave;
    this.datumPrijaveElement.min = this.datumDanas.toISOString().split('T')[0]
    this.datumOdjaveElement.min = new Date(new Date().setDate(this.podaci.datumPrijave.getDate() + 1)).toISOString().split('T')[0]


    let elementi = document.getElementsByClassName("lijevi-panel-button")!;
    elementi[0].classList.add("active");
    for (let i = 0; i < elementi.length; i++) {
      elementi[i].addEventListener("click", (event: Event) => {
        if (this.filtriranjeUpaljeno) {
          elementi[1].classList.remove("active");
          elementi[0].classList.add("active");
          document.getElementById("lijevi-panel-pretraga")!.style.display = "block"
          document.getElementById("lijevi-panel-filtriranje")!.style.display = "none"
        } else {
          elementi[0].classList.remove("active");
          elementi[1].classList.add("active");
          document.getElementById("lijevi-panel-pretraga")!.style.display = 'none'
          document.getElementById("lijevi-panel-filtriranje")!.style.display = "block"

        }
        this.filtriranjeUpaljeno = !this.filtriranjeUpaljeno;
      })
    }
    this.dohvatiSobe();

  }

  async otvoriDetalje(id: number) {
    Navigator.trenutniIdSobe = id;
    this.podaci.filterPoCijeni = Number((document.getElementById("filter-po-cijeni") as HTMLSelectElement).value);
    if(!this.authServis.isMenadzer())
      await this.navigator.navigiraj('pregled', [id], this.podaci)
    else
      await this.navigator.navigiraj('modifikacija', [id])
  }

  ocistiFiltere() {
    this.podaci = {
      aranzmanId: 0,
      balkon: false,
      bazen: false,
      besplatnoOtkazivanje: false,
      brojBracnihKreveta: 0,
      brojDjece: 0,
      brojDjecjihKreveta: 0,
      brojObicnihKreveta: 0,
      brojOdraslih: 1,
      datumOdjave: this.datumDanas,
      datumPrijave: this.datumSutra,
      dozvoljeniLjubimci: false,
      filterPoCijeni: 0,
      klima: false,
      minibar: false,
      prilagodjenInvalidima: false,
      spa: false,
      teretana: false
    };
    (document.getElementById("filter-aranzman") as HTMLSelectElement).selectedIndex = 0;
    (document.getElementById("filter-po-cijeni") as HTMLSelectElement).selectedIndex = 0;
    this.dohvatiSobe();
  }

  protected dohvatiSobe() {
    this.podaci.datumPrijave = this.datumPrijaveElement.valueAsDate;
    this.podaci.datumOdjave = this.datumOdjaveElement.valueAsDate;
    if (this.podaci.datumPrijave >= this.podaci.datumOdjave) {
      this.podaci.datumOdjave = new Date(new Date().setDate(this.podaci.datumPrijave.getDate() + 1))
      this.datumOdjaveElement.valueAsDate = this.podaci.datumOdjave;
    }
    this.podaci.aranzmanId = Number((document.getElementById("filter-aranzman") as HTMLSelectElement).value);
    this.podaci.filterPoCijeni = Number((document.getElementById("filter-po-cijeni") as HTMLSelectElement).value);

    this.pretragaEndpoint.Akcija(this.podaci).subscribe({
      next: res => {
        this.dostupneSobe = res.sobe;
      },
      complete: () => this.dostupneSobe?.forEach(s => s.slika = Slike.getRandomSliku())
    })

  }
}
