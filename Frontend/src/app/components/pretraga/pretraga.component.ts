import {
  AfterContentInit,
  AfterViewInit, ApplicationRef,
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnInit,
  ViewChild
} from '@angular/core';
import {PretragaEndpointResSoba} from "../../endpoints/pretraga-endpoint/pretraga-endpoint-res";
import {NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {PretragaEndpoint} from "../../endpoints/pretraga-endpoint/pretraga-endpoint";
import {HttpClientModule} from "@angular/common/http";
import {PretragaEndpointReq} from "../../endpoints/pretraga-endpoint/pretraga-endpoint-req";
import {Navigator} from "../../navigator";
import {Slike} from "../../slike";
import {min} from "rxjs";
import {AranzmanModel} from "../../models/aranzmanModel";
import {GetAllAranzmaneEndpoint} from "../../endpoints/aranzmani-endpoint/get-all-aranzmane/get-all-aranzmane-endpoint";

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
  datumPrijave: Date = this.datumDanas;
  datumOdjave: Date = this.datumSutra;
  brojOdraslih: number = 1;
  brojDjece: number = 0;

  besplatnoOtkazivanje: boolean = false;
  klima: boolean = false;
  bazen: boolean = false;
  spa: boolean = false;
  prilagodjenInvalidima: boolean = false;
  teretana: boolean = false;
  dozvoljeniLjubimci: boolean = false;
  minibar: boolean = false;
  balkon: boolean = false;
  datumPrijaveElement: any;
  datumOdjaveElement: any;
  brojBracnihKreveta: number = 0;
  brojObicnihKreveta: number = 0;
  brojDjecjihKreveta: number = 0;
  constructor(private pretragaEndpoint: PretragaEndpoint,
              private navigator: Navigator,
              private getAllAranzmaneEndpoint:GetAllAranzmaneEndpoint) {
  }

  ngOnInit(): void {
    this.datumPrijaveElement = (document.getElementById("datum-prijave") as HTMLInputElement)
    this.datumOdjaveElement = (document.getElementById("datum-odjave") as HTMLInputElement)
    this.datumPrijaveElement = this.datumPrijaveElement as HTMLInputElement;
    this.datumOdjaveElement = this.datumOdjaveElement as HTMLInputElement;
    this.datumPrijaveElement.valueAsDate = this.datumPrijave;
    this.datumOdjaveElement.valueAsDate = this.datumOdjave;
    this.datumPrijaveElement.min = this.datumDanas.toISOString().split('T')[0]
    this.datumOdjaveElement.min = new Date(new Date().setDate(this.datumPrijave.getDate() + 1)).toISOString().split('T')[0]

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
    this.getAllAranzmaneEndpoint.Akcija().subscribe(res => this.aranzmani = res.aranzmani);
    this.dohvatiSobe();

  }

  async otvoriDetalje(id: number, podaci: any) {
    Navigator.trenutniIdSobe = id;
    await this.navigator.navigiraj('pregled', [id], podaci)
  }

  protected readonly Slike = Slike;

  protected dohvatiSobe() {
    this.datumPrijave = this.datumPrijaveElement.valueAsDate;
    this.datumOdjave = this.datumOdjaveElement.valueAsDate;
    if (this.datumPrijave >= this.datumOdjave) {
      this.datumOdjave = new Date(new Date().setDate(this.datumPrijave.getDate() + 1))
      this.datumOdjaveElement.valueAsDate = this.datumOdjave;
    }
      let req: PretragaEndpointReq = {
        aranzmanId: Number((document.getElementById("filter-aranzman") as HTMLSelectElement).value),
        brojBracnihKreveta: this.brojBracnihKreveta,
        brojDjecjihKreveta: this.brojDjecjihKreveta,
        brojObicnihKreveta: this.brojObicnihKreveta,
        filterPoCijeni: Number((document.getElementById("filter-po-cijeni") as HTMLSelectElement).value),
        balkon: this.balkon,
        bazen: this.bazen,
        besplatnoOtkazivanje: this.besplatnoOtkazivanje,
        dozvoljeniLjubimci: this.dozvoljeniLjubimci,
        klima: this.klima,
        minibar: this.minibar,
        prilagodjenInvalidima: this.prilagodjenInvalidima,
        spa: this.spa,
        teretana: this.teretana,
        brojDjece: this.brojDjece,
        brojOdraslih: this.brojOdraslih,
        datumOdjave: this.datumOdjave,
        datumPrijave: this.datumPrijave
    };
    this.pretragaEndpoint.Akcija(req).subscribe({
      next: res => {
        this.dostupneSobe = res.sobe;
      }
    })
  }

  ocistiFiltere() {
    this.besplatnoOtkazivanje = false;
    this.klima = false;
    this.bazen = false;
    this.spa = false;
    this.prilagodjenInvalidima = false;
    this.teretana = false;
    this.dozvoljeniLjubimci = false;
    this.minibar = false;
    this.balkon = false;
    this.brojBracnihKreveta = 0;
    this.brojObicnihKreveta = 0;
    this.brojDjecjihKreveta = 0;
    (document.getElementById("filter-aranzman") as HTMLSelectElement).selectedIndex = 0;
    (document.getElementById("filter-po-cijeni") as HTMLSelectElement).selectedIndex = 0;
    this.dohvatiSobe();
  }
}
