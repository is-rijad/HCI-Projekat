import {ApplicationConfig} from '@angular/core';
import {provideRouter} from '@angular/router';

import {routes} from './app.routes';
import {Navigator} from "./navigator";
import {Modal} from "./modal";

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), Navigator, Modal]
};
