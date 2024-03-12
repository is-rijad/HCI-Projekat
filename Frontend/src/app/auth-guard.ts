import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";
import {AuthServis} from "./auth-servis";
import {Navigator} from "./navigator";
import {Alert, TipAlerta} from "./alert";

@Injectable({providedIn: 'root'})
export class AuthGuard implements CanActivate {
  constructor(private authServis: AuthServis, private navigator: Navigator) {
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
