import {Injectable} from "@angular/core";

@Injectable()
export class Validator {
  public validirajEmail(email: string) {
    let regex: RegExp = RegExp('^[\\w-\.]+@([\\w-]+\.)+[\\w-]{2,4}$');
    return regex.test(email);
  }

  public validirajLozinku(lozinka: string) {
    let regex = RegExp('^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$')
    return regex.test(lozinka);
  }

  public validirajText(text: string) {
    let regex = RegExp('^[A-Za-z]+$')
    return regex.test(text);
  }
}
