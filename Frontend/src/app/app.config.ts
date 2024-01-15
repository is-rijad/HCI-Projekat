import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {Navigator} from "./navigator";

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), Navigator]
};
