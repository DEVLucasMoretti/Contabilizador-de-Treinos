
import 'moment/locale/pt-br';

import moment, { Moment } from 'moment';

import { Component } from '@angular/core';
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

  caloriasgastas: number | string = '';
  treinoDoDia: string = '';
dataSelecionada: Moment | null = null;

  data: Moment = moment();

  constructor(private dateAdapter: DateAdapter<Moment>) {
    moment.locale('pt-br');
    this.dateAdapter.setLocale('pt-br');
  }



btnContabilizarTreino() {
  if (this.dataSelecionada) {
    const dataFormatada = this.dataSelecionada.format('YYYY-MM-DD');
    console.log(dataFormatada);
  }
}


}
