import {Injectable} from "@angular/core";
import {Slike} from "./slike";

@Injectable()
export class HandlerSlika {
  trenutniIndexSlike = 1;
  isModalVidljiv = false;

  public   promijeniSliku(zaIndex : number) {
    if(this.trenutniIndexSlike == 1 && zaIndex < 0) return;
    if(this.trenutniIndexSlike == Slike.nizSlika.length && zaIndex > 0) return;
    this.trenutniIndexSlike += zaIndex;
    (document.getElementById("slika-slika") as HTMLImageElement).src = Slike.nizSlika[this.trenutniIndexSlike - 1];
    if(document.getElementById("full-screen-slika") != null)
      (document.getElementById("full-screen-slika") as HTMLImageElement).src = Slike.nizSlika[this.trenutniIndexSlike - 1];
  }
}
