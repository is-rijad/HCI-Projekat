import {Component, OnInit} from '@angular/core';
import {OtvoriDetaljeEndpoint} from "../../endpoints/otvori-detalje-endpoint/otvori-detalje-endpoint";
import {HttpClientModule} from "@angular/common/http";
import {Navigator} from "../../navigator";
import {SobaModel} from "../../models/sobaModel";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AranzmanSobaModel} from "../../models/aranzmanSobaModel";
import {delay} from "rxjs";
import {Slike} from "../../slike";
import {HandlerSlika} from "../../handlerSlika";
import {
  NapraviRezervacijuEndpointReq
} from "../../endpoints/rezervacije-endpoint/napravi-rezervaciju/napravi-rezervaciju-endpoint-req";
import {Alert, TipAlerta} from "../../alert";
import {
  ProvjeriRezervacijuEndpoint
} from "../../endpoints/rezervacije-endpoint/provjeri-rezervaciju/provjeri-rezervaciju-endpoint";
import {Konstante} from "../../../konstante";


@Component({
  selector: 'app-pregled-detalja',
  standalone: true,
  imports: [HttpClientModule, NgForOf, ReactiveFormsModule, FormsModule, NgIf, DatePipe],
  templateUrl: './pregled-detalja.component.html',
  styleUrl: './pregled-detalja.component.css',
  providers: [
    OtvoriDetaljeEndpoint,
    HandlerSlika,
    ProvjeriRezervacijuEndpoint
  ]
})
export class PregledDetaljaComponent implements OnInit {
  soba: SobaModel = {
    aranzmani: [],
    cijene: [],
    kreveti: [],
    balkon: false,
    bazen: false,
    besplatnoOtkazivanje: false,
    brojGostiju: 0,
    cijenaZaDjecu: 0,
    djecaDo: 0,
    dozvoljeniLjubimci: false,
    id: 0,
    klima: false,
    minibar: false,
    nazivSobe: "",
    opis: "",
    prilagodjenInvalidima: false,
    brojSlika: Slike.nizSlika.length,
    spa: false,
    teretana: false
  };
  brojOdraslih: number = 1;
  brojDjece: number = 0;
  aranzman: AranzmanSobaModel | undefined = undefined;
  cijenaSobe = 0;
  datumDanas = new Date();
  datumSutra = new Date(new Date().setDate(this.datumDanas.getDate() + 1));
  datumPrijave: Date = this.datumDanas;
  datumOdjave: Date = this.datumSutra;
  datumPrijaveElement: any;
  datumOdjaveElement: any;
  protected readonly Slike = Slike;

  constructor(private otvoriDetaljeEndpoint: OtvoriDetaljeEndpoint,
              protected handlerSlika: HandlerSlika,
              private provjeriRezervacijuEndpoint: ProvjeriRezervacijuEndpoint,
              private navigator: Navigator) {
    let podaci = this.navigator.router.getCurrentNavigation()?.extras.state;
    if (podaci == undefined) {
      history.back()
    } else {
      this.datumPrijave = podaci['datumPrijave']
      this.datumOdjave = podaci['datumOdjave']
      this.brojOdraslih = podaci['brojOdraslih']
      this.brojDjece = podaci['brojDjece']
    }
  }

  ngOnInit(): void {
    this.datumPrijaveElement = (document.getElementById("datum-prijave") as HTMLInputElement);
    this.datumOdjaveElement = (document.getElementById("datum-odjave") as HTMLInputElement);
    this.datumPrijaveElement.valueAsDate = this.datumPrijave;
    this.datumOdjaveElement.valueAsDate = this.datumOdjave;
    this.datumPrijaveElement.min = this.datumDanas.toISOString().split('T')[0]
    this.datumOdjaveElement.min = new Date(new Date().setDate(this.datumPrijave.getDate() + 1)).toISOString().split('T')[0]


    this.otvoriDetaljeEndpoint.Akcija().subscribe({
      next: res => {
        this.soba = res.soba;
      },
      complete: async () => {
        await this.cijeneUcitane();
        let aranzmaniSelect = document.getElementById("aranzman");
        aranzmaniSelect!.innerHTML = "";
        for (let aranzmanSobaModel of this.soba.aranzmani) {
          if (this.soba.aranzmani.indexOf(aranzmanSobaModel) == 0) {
            aranzmaniSelect!.innerHTML += `
                <option selected value='${aranzmanSobaModel.id}'>${aranzmanSobaModel.aranzman.nazivAranzmana}</option>`
          } else {
            aranzmaniSelect!.innerHTML += `
                <option value='${aranzmanSobaModel.id}'>${aranzmanSobaModel.aranzman.nazivAranzmana}</option>`;
          }
        }
        aranzmaniSelect!.innerHTML += `
                <option value='0'>Bez aranžmana</option>`
        this.izracunajCijenu();
      }
    })

    window.addEventListener("keyup", (event) => {
      if (this.handlerSlika.isModalVidljiv) {
        if (event.key == "Escape") {
          this.handlerSlika.isModalVidljiv = !this.handlerSlika.isModalVidljiv;
        } else if (event.key == "ArrowLeft") {
          this.handlerSlika.promijeniSliku(-1);
        } else if (event.key == "ArrowRight") {
          this.handlerSlika.promijeniSliku(1);
        }
      }

    })
  }

  datediff(first: number, second: number) {
    return Math.round((second - first) / (1000 * 60 * 60 * 24));
  }

  provjeriRezervaciju() {
    let req: NapraviRezervacijuEndpointReq = {
      brojDjece: this.brojDjece,
      brojOsoba: this.brojOdraslih,
      datumDolaska: this.datumPrijave,
      datumOdlaska: this.datumOdjave,
      sobaAranzmanId: this.aranzman?.id!,
      sobaId: this.soba.id
    }
    this.provjeriRezervacijuEndpoint.Akcija(req).subscribe({
      next: async res => {
        if (res.status == 200) {
          await this.navigator.navigirajSPodacima('potvrda', res)
        } else
          Alert.alert = new Alert(TipAlerta.error, res.message)
      },
      error: err => {
        Alert.alert = new Alert(TipAlerta.error, Konstante.greskaKomunikacija)
      }
    })
  }

  protected izracunajCijenu() {
    let aranzmanId = Number((document.getElementById("aranzman") as HTMLSelectElement).value)
    this.aranzman = this.soba.aranzmani.find(a => a.id == aranzmanId);
    let cijenaZaOdrasle = this.soba.cijene.find(c => c.brojOsoba == this.brojOdraslih)?.cijenaSobe;
    if (cijenaZaOdrasle == undefined) return;

    let cijenaZaDjecu = this.soba.cijenaZaDjecu * this.brojDjece

    this.datumPrijave = this.datumPrijaveElement.valueAsDate;
    this.datumOdjave = this.datumOdjaveElement.valueAsDate;

    let brojNoci = this.datediff(this.datumPrijave.valueOf(), this.datumOdjave.valueOf())

    this.cijenaSobe = (((cijenaZaOdrasle + cijenaZaDjecu) * ((this.aranzman?.doplata! / 100) + 1)) * brojNoci);

    if (this.cijenaSobe <= 0) {
      this.datumOdjave = new Date(new Date().setDate(this.datumPrijave.getDate() + 1))
      this.datumOdjaveElement.valueAsDate = this.datumOdjave;
      let brojNoci = this.datediff(this.datumPrijave.valueOf(), this.datumOdjave.valueOf())

      this.cijenaSobe = (((cijenaZaOdrasle + cijenaZaDjecu) * ((this.aranzman?.doplata! / 100) + 1)) * brojNoci);
    }
    this.datumOdjaveElement.min = new Date(new Date().setDate(this.datumPrijave.getDate() + 1)).toISOString().split('T')[0]

    if (isNaN(this.cijenaSobe))
      this.cijenaSobe = 0
  }

  private async cijeneUcitane() {
    while (this.soba.cijene.length < this.soba.brojGostiju) {
      await new Promise(() => setTimeout(() => delay(2000)))
    }
    this.izracunajCijenu();
  }
}
