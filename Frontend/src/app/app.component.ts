import {Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterOutlet} from '@angular/router';
import {PretragaComponent} from "./components/pretraga/pretraga.component";
import {Navigator} from "./navigator";
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
    await this.navigator.navigiraj("pocetna")
  }
}
