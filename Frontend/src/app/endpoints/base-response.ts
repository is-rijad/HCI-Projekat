export class BaseResponse {
  status: number;
  message: string;

  constructor() {
    this.status = 200;
    this.message = "";
  }
}
