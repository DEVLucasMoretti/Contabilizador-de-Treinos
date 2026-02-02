import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { Menu } from "../../shared/menu/menu";
import { MatCardModule } from '@angular/material/card';
import { TreinoApi } from '../../services/treino-api';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-index-home',
  imports: [Menu, CommonModule, FormsModule,
    MatCardModule],
  templateUrl: './index-home.html',
  styleUrl: './index-home.css',
})
export class IndexHome {

  private readonly treinoApi = inject(TreinoApi);
    private readonly cdr = inject(ChangeDetectorRef);



  diasTreinados : number = 0;
  caloriasGastasNaSemana : number = 0;
  diasTreinadosNaSemana : number = 0;


 constructor() {}

 ngOnInit(): void {
  this.construirProgressoSemanal();
  this.construirDiasTreinados();
 }


 construirProgressoSemanal() : void {
  this.treinoApi.getProgressoDaSemana().subscribe({
    next: (treinos) => {
      this.diasTreinadosNaSemana = treinos.length;
      this.caloriasGastasNaSemana = treinos.reduce((total, treino) => total + treino.QuantidadeCaloria, 0);
      console.log('Progresso da semana:', treinos);
      console.log('Calorias gastas na semana:', this.caloriasGastasNaSemana);
      console.log('Dias treinados na semana:', this.diasTreinadosNaSemana);
      this.cdr.detectChanges();
    },
    error: (err) => {
      console.error('Erro ao obter o progresso da semana:', err);
    }
  });
 }

 construirDiasTreinados() : void {
  this.treinoApi.getQuantidadeDeTodosTreinos().subscribe({
    next: (totalDias) => {
      this.diasTreinados = totalDias;
      console.log('Total de dias treinados:', this.diasTreinados);
      this.cdr.detectChanges();
    },
    error: (err) => {
      console.error('Erro ao obter o total de dias treinados:', err);
    }
  });


 }


}
