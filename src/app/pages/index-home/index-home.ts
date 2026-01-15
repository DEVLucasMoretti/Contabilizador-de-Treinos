import { Component } from '@angular/core';
import { Menu } from "../../shared/menu/menu";
import { BrowserModule } from '@angular/platform-browser';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-index-home',
  imports: [Menu,
    MatCardModule],
  templateUrl: './index-home.html',
  styleUrl: './index-home.css',
})
export class IndexHome {

  diasTreinados : number = 0;
  caloriasGastasNaSemana : number = 0;
  diasTreinadosNaSemana : number = 0;
}
