import {Injectable} from "@angular/core";

@Injectable({providedIn: 'root'})
export class Modal {
  aktivan: boolean;
  sadrzaj: string;
  potvdna: boolean | null;

  constructor() {
    this.potvdna = null;
    this.aktivan = false;
    this.sadrzaj = "";
  }

  napraviDijalog(sadrzaj: string) {
    this.potvdna = null;
    this.sadrzaj = sadrzaj;
    this.aktivan = true;
  }

  potvrdi() {
    this.potvdna = true;
    this.aktivan = false;
  }

  otkazi() {
    this.potvdna = false;
    this.aktivan = false;
  }
}
