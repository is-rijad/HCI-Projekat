import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";
import {AuthServis} from "./auth-servis";
import {Navigator} from "./navigator";

@Injectable({providedIn: 'root'})
export class MenadzerGuard implements CanActivate {
  constructor(private authServis: AuthServis, private navigator: Navigator) {
  }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authServis.isMenadzer())
      return true;
    else {
      return false;
    }
  }
}
