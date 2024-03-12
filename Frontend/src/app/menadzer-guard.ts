import {Injectable} from "@angular/core";
import {KorisnickiNalog} from "./models/korisnicki-nalog";
import {CookieService} from "ngx-cookie-service";
import {Token} from "./models/token";
import {ActivatedRouteSnapshot, CanActivate, CanActivateFn, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";
import {AuthServis} from "./auth-servis";
import {Navigator} from "./navigator";

@Injectable({providedIn: 'root'})
export class MenadzerGuard implements CanActivate{
  constructor(private authServis:AuthServis, private navigator:Navigator) {
  }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authServis.isMenadzer())
      return true;
    else {
      return false;
    }
  }
}
