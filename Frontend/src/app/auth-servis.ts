import {Injectable} from "@angular/core";
import {KorisnickiNalog} from "./models/korisnicki-nalog";
import {CookieService} from "ngx-cookie-service";
import {Token} from "./models/token";

@Injectable({providedIn: 'root'})
export class AuthServis {
  constructor(private cookieService:CookieService) {
  }
  isLogiran() : boolean {
    if(this.getInfo() == null)
      return false;
    return this.getInfo()!.isLogiran;
  }
  isMenadzer() : boolean {
    if(this.getInfo() == null)
      return false;
    return this.getInfo()!.korisnickiNalog.isMenadzer;
  }
  setLoginInfo(info?:Token) {
    if(info == undefined)
      this.cookieService.set("auth-token", "");
    else
      this.cookieService.set("auth-token", JSON.stringify(info), {
        secure:true
      });
  }
  getInfo():Token | null {
    if(this.cookieService.get("auth-token") == "")
      return null;
    return JSON.parse(this.cookieService.get("auth-token")) as Token;
  }
}
