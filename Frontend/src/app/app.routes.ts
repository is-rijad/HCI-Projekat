import {Routes} from '@angular/router';
import {PretragaComponent} from "./components/pretraga/pretraga.component";
import {PocetnaComponent} from "./components/pocetna/pocetna.component";
import {PregledDetaljaComponent} from "./components/pregled-detalja/pregled-detalja.component";
import {ModifikacijaDetaljaComponent} from "./components/modifikacija-detalja/modifikacija-detalja.component";
import {PotvrdaRezervacijeComponent} from "./components/potvrda-rezervacije/potvrda-rezervacije.component";
import {MojeRezervacijeComponent} from "./components/moje-rezervacije/moje-rezervacije.component";
import {MenadzerGuard} from "./menadzer-guard";
import {AuthGuard} from "./auth-guard";
import {LoginComponent} from "./components/login/login.component";
import {RegistracijaComponent} from "./components/registracija/registracija.component";

export const routes: Routes = [
  {path: '', redirectTo: 'pocetna', pathMatch: 'full'},
  {path: 'pocetna', component: PocetnaComponent},
  {path: 'pretraga', component: PretragaComponent},
  {path: 'pregled/:id', component: PregledDetaljaComponent},
  {path: 'modifikacija/:id', component: ModifikacijaDetaljaComponent, canActivate: [AuthGuard, MenadzerGuard]},
  {path: 'potvrda', component: PotvrdaRezervacijeComponent, canActivate: [AuthGuard]},
  {path: 'moje-rezervacije', component: MojeRezervacijeComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginComponent},
  {path: 'registracija', component: RegistracijaComponent}
];
