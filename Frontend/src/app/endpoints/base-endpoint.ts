import {Observable} from "rxjs";

export interface BaseEndpoint<Treq, Tres> {
  Akcija(req: Treq): Observable<Tres>;
}
