import {BasePostEndpoint} from "../base-endpoint";
import {LoginReq} from "./login-req";
import {BaseResponse} from "../base-response";

export class LoginEndpoint {
  constructor(private post : BasePostEndpoint<LoginReq, BaseResponse>) {
  }

}
