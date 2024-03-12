import { Component, isStandalone } from '@angular/core';
import {Navigator} from "../../navigator";
import {Validator} from "../../validator";
import {LoginEndpoint} from "../../endpoints/korisnici-endpoint/login-endpoint/login-endpoint";
import {LoginReq} from "../../endpoints/korisnici-endpoint/login-endpoint/login-req";
import {Alert, TipAlerta} from "../../alert";
import {RegistracijaEndpoint} from "../../endpoints/korisnici-endpoint/registracija-endpoint/registracija-endpoint";
import {HttpClientModule} from "@angular/common/http";
import {
  RegistracijaEndpointReq
} from "../../endpoints/korisnici-endpoint/registracija-endpoint/registracija-endpoint-req";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-registracija',
  standalone: true,
  imports: [HttpClientModule, FormsModule],
  templateUrl: './registracija.component.html',
  styleUrl: './registracija.component.css',
  providers: [
    RegistracijaEndpoint,
    Validator
  ]
})
export class RegistracijaComponent {
  req:RegistracijaEndpointReq = {
    datumRodjenja: new Date(), drzava: "", email: "", grad: "", ime: "", lozinka: "", prezime: ""

  }
  constructor(protected navigator:Navigator,
              protected validator:Validator,
              private registracijaEndpoint:RegistracijaEndpoint) {
  }

  registrujSe() {
    if(this.validator.validirajText(this.req.ime) &&
      this.validator.validirajText(this.req.prezime) &&
      this.validator.validirajText(this.req.grad) &&
      this.validator.validirajText(this.req.drzava) &&
      this.validator.validirajEmail(this.req.email) &&
      this.validator.validirajLozinku(this.req.lozinka)) {
      this.popraviText();

      this.registracijaEndpoint.Akcija(this.req).subscribe({
        next: async res => {
          if (res.status == 200)
            await this.navigator.navigiraj('login');
          else {
            this.req.lozinka = ""
            Alert.alert = new Alert(TipAlerta.error, res.message);
          }
        },
        error: err => {
          this.req.lozinka = ""
          Alert.alert = new Alert(TipAlerta.error, "Greška u komunikaciji sa serverom!")
        }
      })
    }
    else {
      Alert.alert = new Alert(TipAlerta.error, "Unos nije validan!");
      this.req.lozinka=""
    }
  }
  private popraviText() {
    this.req.ime = this.req.ime?.trim();
    this.req.ime = this.req.ime[0]?.toUpperCase() + this.req.ime?.substring(1);
    this.req.prezime = this.req.prezime?.trim();
    this.req.prezime = this.req.prezime[0]?.toUpperCase() + this.req.prezime?.substring(1);
    this.req.grad = this.req.grad?.trim();
    this.req.grad = this.req.grad[0]?.toUpperCase() + this.req.grad?.substring(1);
    this.req.drzava = this.req.drzava?.trim();
    this.req.drzava = this.req.drzava[0]?.toUpperCase() + this.req.drzava?.substring(1);
  }
  protected readonly isStandalone = isStandalone;
}
