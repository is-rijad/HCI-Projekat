import {Injectable} from "@angular/core";
import {NavigationEnd, NavigationStart, Router, RouterEvent} from "@angular/router";

@Injectable({providedIn: "root"})
export class Navigator {
  static trazenaKomponenta:string="";
  static trenutniIdSobe: number = -1;
  static trenutniElementi: HTMLCollectionOf<Element> | null = null;
  podaci: any;

  constructor(public router: Router) {
    this.router.events.subscribe(async (event) => {
      if (event instanceof NavigationEnd) {
        this.handlerKartica((event as RouterEvent).url);
      }
    })
  }


  async navigiraj(url: string, params: any[] = [], extras: any = {}) {
    if (params[0] != -1) {
      let urlNiz: any[] = [];
      urlNiz.push(`../${url}`);
      for (let i of params) {
        urlNiz.push(i);
      }
      this.podaci = extras;
      await this.router.navigate(urlNiz, {state: extras});
    }
  }

  async navigirajSPodacima(url: string, extras: any) {
    this.podaci = extras;
    await this.router.navigate([`/${url}`], {state: extras})
  }

  private handlerKartica(url: string) {
    let indexDo = url.indexOf('/', 1);
    if (indexDo <= 0)
      url = url.substring(1);
    else
      url = url.substring(1, indexDo);

    let elementi = document.getElementsByClassName(url);
    if (Navigator.trenutniElementi != null) {
      for (let i = 0; i < Navigator.trenutniElementi.length; i++) {
        Navigator.trenutniElementi[i].classList.remove("active");
      }
    }
    Navigator.trenutniElementi = elementi;
    for (let i = 0; i < Navigator.trenutniElementi.length; i++) {
      Navigator.trenutniElementi[i].classList.add("active");
    }
  }
}
