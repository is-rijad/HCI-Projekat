import {Component, OnInit} from '@angular/core';
import {Navigator} from "../../navigator";

@Component({
  selector: 'app-pocetna',
  standalone: true,
  imports: [],
  templateUrl: './pocetna.component.html',
  styleUrl: './pocetna.component.css'
})
export class PocetnaComponent implements OnInit{
  constructor(private navigator:Navigator) {
  }
  async ngOnInit(): Promise<void> {
    await this.navigator.navigiraj('pocetna');
  }
}
