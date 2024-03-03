import {Routes} from '@angular/router';
import {PretragaComponent} from "./components/pretraga/pretraga.component";
import {PocetnaComponent} from "./components/pocetna/pocetna.component";
import {PregledDetaljaComponent} from "./components/pregled-detalja/pregled-detalja.component";
import {ModifikacijaDetaljaComponent} from "./components/modifikacija-detalja/modifikacija-detalja.component";
import {PotvrdaRezervacijeComponent} from "./components/potvrda-rezervacije/potvrda-rezervacije.component";

export const routes: Routes = [
  {path: '', redirectTo: 'pocetna', pathMatch: 'full'},
  {path: 'pocetna', component: PocetnaComponent},
  {path: 'pretraga', component: PretragaComponent},
  {path: 'pregled/:id', component: PregledDetaljaComponent},
  {path: 'modifikacija/:id', component: ModifikacijaDetaljaComponent},
  {path: 'potvrda', component: PotvrdaRezervacijeComponent}
];
