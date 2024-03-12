import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterOutlet} from '@angular/router';
import {PretragaComponent} from "./components/pretraga/pretraga.component";
import {Navigator} from "./navigator";
import {Alert, TipAlerta} from "./alert";
import {Modal} from "./modal";
import {LogoutEndpoint} from "./endpoints/korisnici-endpoint/logout-endpoint/logout-endpoint";
import {HttpClientModule} from "@angular/common/http";
import {AuthServis} from "./auth-servis";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, PretragaComponent, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [
    LogoutEndpoint
  ]
})
export class AppComponent {
  title = 'Hotel Leo';
  protected readonly Navigator = Navigator;
  protected readonly Alert = Alert;

  constructor(public navigator: Navigator,
              protected modal: Modal,
              private logoutEndpoint: LogoutEndpoint,
              protected authServis: AuthServis) {
  }

  odjaviSe() {
    this.logoutEndpoint.Akcija().subscribe({
      next: async res => {
        if (res.status == 200) {
          this.authServis.setLoginInfo(undefined)
          await this.navigator.navigiraj('pocetna');
        } else {
          Alert.alert = new Alert(TipAlerta.error, res.message);
        }
      },
      error: err => {
        Alert.alert = new Alert(TipAlerta.error, err.error());
      }
    })
  }
}
