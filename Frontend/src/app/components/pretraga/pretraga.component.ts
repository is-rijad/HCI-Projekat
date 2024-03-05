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
export class PretragaComponent implements OnInit {
  dostupneSobe: PretragaEndpointResSoba[] | null = null;
  filtriranjeUpaljeno: boolean = false;

  datumDanas = new Date();
  datumSutra = new Date(new Date().setDate(this.datumDanas.getDate() + 1));
  datumPrijave: Date = this.datumDanas;
  datumOdjave: Date = this.datumSutra;
  brojOdraslih: number = 1;
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
  datumPrijaveElement: any;
  datumOdjaveElement: any;
  constructor(private pretragaEndpoint: PretragaEndpoint,
              private navigator: Navigator) {
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
        } else {
          elementi[0].classList.remove("active");
          elementi[1].classList.add("active");
          document.getElementById("lijevi-panel-pretraga")!.style.display = 'none'
        }
        this.filtriranjeUpaljeno = !this.filtriranjeUpaljeno;
      })
    }
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
      brojDjece: this.brojDjece,
      brojOdraslih: this.brojOdraslih,
      datumOdjave: this.datumOdjave,
      datumPrijave: this.datumPrijave
    };
    this.pretragaEndpoint.Akcija(req).subscribe({
      next: res => {
        this.dostupneSobe = res.sobe;
      },
      complete: () => console.log(this.dostupneSobe)
    })
  }
}
