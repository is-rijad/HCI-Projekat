import {ApplicationConfig} from '@angular/core';
import {provideRouter} from '@angular/router';

import {routes} from './app.routes';
import {Navigator} from "./navigator";
import {Modal} from "./modal";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {AuthServis} from "./auth-servis";
import {MenadzerGuard} from "./menadzer-guard";
import {AuthGuard} from "./auth-guard";

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), Navigator, Modal, AuthServis, MenadzerGuard, AuthGuard]
};
