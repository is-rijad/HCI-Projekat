import {Component, OnInit} from '@angular/core';
import {OtvoriDetaljeEndpoint} from "../../endpoints/otvori-detalje-endpoint/otvori-detalje-endpoint";
import {Config} from "../../config";
import {Router} from "@angular/router";
import {OtvoriDetaljeEndpointRes} from "../../endpoints/otvori-detalje-endpoint/otvori-detalje-endpoint-res";
import {HttpClientModule} from "@angular/common/http";
import {Navigator} from "../../navigator";
import {SobaModel} from "../../models/sobaModel";

@Component({
  selector: 'app-pregled-detalja',
  standalone: true,
  imports: [HttpClientModule],
  templateUrl: './pregled-detalja.component.html',
  styleUrl: './pregled-detalja.component.css',
  providers: [OtvoriDetaljeEndpoint]
})
export class PregledDetaljaComponent implements OnInit{
  soba : SobaModel | null = null;
  constructor(private router : Router,
              private otvoriDetaljeEndpoint:OtvoriDetaljeEndpoint) {
  }
  ngOnInit(): void {
    let routerUrl = this.router.routerState.snapshot.url;
    let url = Config.adresaServera + "Sobe/GetSobuId/?Id=" + routerUrl.charAt(routerUrl.length-1);
    this.otvoriDetaljeEndpoint.Akcija(url).subscribe({
      next: res => {
        this.soba = res.soba;
      }
    })
  }

}
