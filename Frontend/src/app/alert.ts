export class Alert {
  static alert: Alert = {aktivan: false, sadrzaj: "", tip: ""};
  aktivan: boolean;
  tip: string;
  sadrzaj: string;

  constructor(tip: string, sadrzaj: string, trajanje: number = 2000) {
    this.tip = tip;
    this.sadrzaj = sadrzaj;
    this.aktivan = true;
    setTimeout(() => {
      this.aktivan = false
    }, trajanje);
  }
}

export class TipAlerta {
  static readonly success = "primary";
  static readonly error = "danger";
}
