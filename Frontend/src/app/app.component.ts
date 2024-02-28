import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {NavigationEnd, Router, RouterOutlet} from '@angular/router';
import {PretragaComponent} from "./components/pretraga/pretraga.component";
import {Navigator} from "./navigator";
import {Alert} from "./alert";
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, PretragaComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: [Navigator]
})
export class AppComponent implements OnInit{
  title = 'Hotel Leo';


  constructor(public navigator : Navigator) {}

  async ngOnInit(){
    // await this.navigator.navigiraj("pocetna")
    Navigator.trenutniIdSobe = 6;
    await this.navigator.navigiraj("modifikacija", [6])
  }

  protected readonly Navigator = Navigator;
  protected readonly Alert = Alert;
}
