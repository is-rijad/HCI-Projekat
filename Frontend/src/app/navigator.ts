import {Injectable} from "@angular/core";
import {NavigationEnd, Router, RouterEvent} from "@angular/router";

@Injectable({providedIn:"root"})
export class Navigator {
  static trenutniIdSobe : number = -1;
  static trenutniElementi : HTMLCollectionOf<Element> | null = null;

  constructor(private router : Router) {
    this.router.events.subscribe((event) => {
      if(event instanceof NavigationEnd) {
        this.handlerKartica((event as RouterEvent).url);
      }
    })
  }


  async navigiraj(url: string, params:any[] = []) {
    Navigator.trenutniIdSobe = params[0]
    if (params[0] != -1) {
      let urlNiz: any[] = [];
      urlNiz.push(`../${url}`);
      for (let i of params) {
        urlNiz.push(i);
      }
      await this.router.navigate(urlNiz);
    }
  }
  private handlerKartica(url: string) {
    let indexDo = url.indexOf('/', 1);
    console.log(indexDo)
    if(indexDo <= 0)
      url = url.substring(1);
    else
      url = url.substring(1, indexDo);
    console.log(url)

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
