import {KorisnickiNalog} from "./korisnicki-nalog";

export interface Token {
  id : number,
token:string,
korisnickiNalogId:number,
korisnickiNalog:KorisnickiNalog,
vrijeme : Date,
isLogiran:boolean
}
