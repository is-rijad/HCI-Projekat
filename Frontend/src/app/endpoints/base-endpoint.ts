import {Observable} from "rxjs";

export interface BaseEndpoint<Treq, Tres>{
  Akcija(url : string, req : Treq) : Observable<Tres> ;
}
