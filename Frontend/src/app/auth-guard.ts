import {Injectable} from "@angular/core";
import {KorisnickiNalog} from "./models/korisnicki-nalog";
import {CookieService} from "ngx-cookie-service";
import {Token} from "./models/token";
import {ActivatedRouteSnapshot, CanActivate, CanActivateFn, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";
import {AuthServis} from "./auth-servis";
import {Navigator} from "./navigator";
import {Alert, TipAlerta} from "./alert";

@Injectable({providedIn: 'root'})
export class AuthGuard implements CanActivate{
  constructor(private authServis:AuthServis, private navigator:Navigator) {
  }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authServis.isLogiran())
      return true;
    else {
      Alert.alert = new Alert(TipAlerta.error, "Niste logirani!");
      Navigator.trazenaKomponenta = state.url.substring(1);
      setTimeout(async () => await this.navigator.navigirajSPodacima('login', this.navigator.podaci), 2000);
      return false;
    }
  }
}
