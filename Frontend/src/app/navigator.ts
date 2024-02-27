import {Injectable} from "@angular/core";
import {Router} from "@angular/router";

@Injectable({providedIn:"root"})
export class Navigator {
  static trenutniIdSobe : number = 0;
  static trenutniElementi : HTMLCollectionOf<Element> | null = null;

  constructor(private router : Router) {}


  async navigiraj(url: string, params:any[] = []) {
    let urlNiz : any[] = [];
    urlNiz.push(`../${url}`);
    for (let i of params) {
      urlNiz.push(i);
    }
    await this.router.navigate(urlNiz);
    let elementi = document.getElementsByClassName(url);
    if (Navigator.trenutniElementi != null) {
      for (let i = 0; i < Navigator.trenutniElementi.length; i++) {
        Navigator.trenutniElementi[i].classList.remove("active");
      }
    }
    Navigator.trenutniElementi = elementi;
    for(let i = 0; i < Navigator.trenutniElementi.length; i++) {
      Navigator.trenutniElementi[i].classList.add("active");
    }
  }
}
