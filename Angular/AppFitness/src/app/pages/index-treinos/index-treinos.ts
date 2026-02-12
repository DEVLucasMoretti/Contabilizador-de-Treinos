import { Component } from '@angular/core';
import { Header } from "../../shared/header/header";
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
@Component({
  selector: 'app-index-treinos',
  imports: [Header,MatCardModule,
  MatButtonModule],
  templateUrl: './index-treinos.html',
  styleUrl: './index-treinos.css',
})
export class IndexTreinos {

}
