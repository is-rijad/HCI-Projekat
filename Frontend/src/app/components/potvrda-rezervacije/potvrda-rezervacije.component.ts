import {Component} from '@angular/core';
import {Navigator} from "../../navigator";
import {
  ProvjeriRezervacijuEndpointRes
} from "../../endpoints/rezervacije-endpoint/provjeri-rezervaciju/provjeri-rezervaciju-endpoint-res";
import {FormsModule} from "@angular/forms";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {
  NapraviRezervacijuEndpoint
} from "../../endpoints/rezervacije-endpoint/napravi-rezervaciju/napravi-rezervaciju-endpoint";
import {
  NapraviRezervacijuEndpointReq
} from "../../endpoints/rezervacije-endpoint/napravi-rezervaciju/napravi-rezervaciju-endpoint-req";
import {Alert, TipAlerta} from "../../alert";
import {HttpClientModule} from "@angular/common/http";
import {Konstante} from "../../../konstante";

@Component({
  selector: 'app-potvrda-rezervacije',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    NgIf,
    DatePipe,
    HttpClientModule
  ],
  templateUrl: './potvrda-rezervacije.component.html',
  styleUrl: './potvrda-rezervacije.component.css',
  providers: [
    NapraviRezervacijuEndpoint
  ]
})
export class PotvrdaRezervacijeComponent {
  podaci: ProvjeriRezervacijuEndpointRes

  constructor(private navigator: Navigator,
              private napraviRezervacijuEndpoint: NapraviRezervacijuEndpoint) {
    this.podaci = this.navigator.podaci as ProvjeriRezervacijuEndpointRes;
    if (this.podaci == null || Object.keys(this.podaci).length == 0) {
      history.back();
    }
  }

  napraviRezervaciju() {
    let req: NapraviRezervacijuEndpointReq = {
      brojDjece: this.podaci.detaljiRezervacije.brojDjece,
      brojOsoba: this.podaci.detaljiRezervacije.brojOsoba,
      datumDolaska: this.podaci.detaljiRezervacije.datumDolaska,
      datumOdlaska: this.podaci.detaljiRezervacije.datumOdlaska,
      sobaAranzmanId: this.podaci.detaljiRezervacije.sobaAranzmanId,
      sobaId: this.podaci.detaljiRezervacije.sobaId
    }
    this.napraviRezervacijuEndpoint.Akcija(req).subscribe({
      next: res => {
        if (res.status == 200) {
          Alert.alert = new Alert(TipAlerta.success, res.message)
          setTimeout(() => {
            this.navigator.podaci = null;
            this.navigator.navigiraj('pocetna')
          }, 2500);
        } else
          Alert.alert = new Alert(TipAlerta.error, res.message)
      },
      error: err => {
        Alert.alert = new Alert(TipAlerta.error, Konstante.greskaKomunikacija)
      }
    })
  }
}
