
import 'moment/locale/pt-br';

import moment, { Moment } from 'moment';

import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE
} from '@angular/material/core';

import {
  MomentDateAdapter,
  MatMomentDateModule
} from '@angular/material-moment-adapter';
import { Menu } from "../../shared/menu/menu";
import { TreinoApi } from '../../services/treino-api';
import { Treino } from '../../Models/Treino';


export const MY_DATE_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel: 'DD/MM/YYYY',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-index-contabilizar-treino',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatMomentDateModule,
    Menu
  ],
  templateUrl: './index-contabilizar-treino.html',
  styleUrl: './index-contabilizar-treino.css',
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE],
    },
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS },
  ],
})
export class IndexContabilizarTreino {

  private readonly treinoApi = inject(TreinoApi);

  caloriasgastas: number = 0;
  treinoDoDia: string = '';
  dataSelecionada: Moment = moment();


  treino: Treino = {
    Id: 0,
    Data: this.dataSelecionada,
    DiaDaSemana: '',
    TreinoDoDia: this.treinoDoDia,
    QuantidadeCaloria: this.caloriasgastas
  };

  data: Moment = moment();

  constructor(private dateAdapter: DateAdapter<Moment>) {
    moment.locale('pt-br');
    this.dateAdapter.setLocale('pt-br');
  }



  btnContabilizarTreino() {
    const dataFormatada  = this.dataSelecionada.format('YYYY-MM-DD');


    this.treinoApi.getUpdateOuCreateTreino(dataFormatada).subscribe({
      next: (treinoRetornado) => {
        console.log('Treino contabilizado com sucesso:', treinoRetornado);
        console.log('CHAMAR UPDATE');
      },
      error: () => {
        console.error('Erro ao contabilizar o treino');
        console.log('CHAMAR POST');

      }
    });



  }


}
