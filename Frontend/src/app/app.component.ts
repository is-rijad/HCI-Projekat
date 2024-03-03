import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterOutlet} from '@angular/router';
import {PretragaComponent} from "./components/pretraga/pretraga.component";
import {Navigator} from "./navigator";
import {Alert} from "./alert";
import {Modal} from "./modal";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, PretragaComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: []
})
export class AppComponent implements OnInit {
  title = 'Hotel Leo';


  constructor(public navigator: Navigator,
              protected modal: Modal) {
  }

  async ngOnInit() {
    await this.navigator.navigiraj("pocetna")
  }

  protected readonly Navigator = Navigator;
  protected readonly Alert = Alert;
}
