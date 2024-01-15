export class BaseResponse {
  status: number;
  poruka : string;

  constructor() {
    this.status = 200;
    this.poruka = "";
  }
}
