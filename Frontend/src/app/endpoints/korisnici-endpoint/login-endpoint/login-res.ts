import { BaseResponse } from "../../base-response"
import {Token} from "../../../models/token";

export interface LoginRes extends BaseResponse{
  token:Token
}
