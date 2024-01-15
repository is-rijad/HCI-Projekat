import {Injectable} from "@angular/core";
import {Router} from "@angular/router";

@Injectable({providedIn:"root"})
export class Navigator {
  trenutniIdSobe : number = 0;
  trenutniElementi : HTMLCollectionOf<Element> | null = null;

  constructor(private router : Router) {}


  async navigiraj(url: string, params:any[] = []) {
    let urlNiz : any[] = [];
    urlNiz.push(`../${url}`);
    for (let i of params) {
      urlNiz.push(i);
    }
    await this.router.navigate(urlNiz);
    let elementi = document.getElementsByClassName(url);
    if (this.trenutniElementi != null) {
      for (let i = 0; i < this.trenutniElementi.length; i++) {
        this.trenutniElementi[i].classList.remove("active");
      }
    }
    this.trenutniElementi = elementi;
    for(let i = 0; i < this.trenutniElementi.length; i++) {
      this.trenutniElementi[i].classList.add("active");
    }
  }
}
