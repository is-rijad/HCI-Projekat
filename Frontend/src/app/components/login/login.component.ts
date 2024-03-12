import {Component, isStandalone} from '@angular/core';
import {Navigator} from "../../navigator";
import {Validator} from "../../validator";
import {FormsModule} from "@angular/forms";
import {Alert, TipAlerta} from "../../alert";
import {LoginEndpoint} from "../../endpoints/korisnici-endpoint/login-endpoint/login-endpoint";
import {LoginReq} from "../../endpoints/korisnici-endpoint/login-endpoint/login-req";
import {HttpClientModule} from "@angular/common/http";
import {AuthServis} from "../../auth-servis";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    HttpClientModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  providers: [
    Validator,
    LoginEndpoint
  ]
})
export class LoginComponent {
  email:string = "";
  lozinka:string = "";
  constructor(protected navigator:Navigator,
              protected validator:Validator,
              private loginEndpoint:LoginEndpoint,
              private authServis:AuthServis) {
  }

  ulogujSe() {
    if(this.validator.validirajEmail(this.email) &&
        this.validator.validirajLozinku(this.lozinka)) {
      let req:LoginReq = {
        email:this.email,
        lozinka:this.lozinka
      }
      this.loginEndpoint.Akcija(req).subscribe({
        next: async res => {
          if (res.status == 200) {
            this.authServis.setLoginInfo(res.token)
            await this.navigator.navigirajSPodacima(Navigator.trazenaKomponenta, this.navigator.podaci);
          }
        else {
            this.lozinka=""

          Alert.alert = new Alert(TipAlerta.error, res.message);
        }
      },
      error: err => {
        this.lozinka = ""
        Alert.alert = new Alert(TipAlerta.error, err.error());
        }
      })
    }
    else {
      Alert.alert = new Alert(TipAlerta.error, "Unos nije validan!");
      this.lozinka=""
    }
  }
  protected readonly isStandalone = isStandalone;
}
