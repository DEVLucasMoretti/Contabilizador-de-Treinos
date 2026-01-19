import { Component, inject } from '@angular/core';
import { Header } from "../../shared/header/header";
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMomentDateModule, MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { registerLocaleData, CommonModule } from '@angular/common';
import pt from '@angular/common/locales/pt';

// ✅ Moment.js e locale pt-BR
import _moment from 'moment';
// @ts-ignore
import 'moment/locale/pt-br';
import { FormsModule } from '@angular/forms';
import { timeInterval, timer } from 'rxjs';
const moment = _moment;
// registra o locale português no Angular
registerLocaleData(pt);

export const MY_DATE_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};


@Component({
  selector: 'app-index-relatorio',
  imports: [
    CommonModule,
    FormsModule,
    Header,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatButtonModule,],
  templateUrl: './index-relatorio.html',
  styleUrl: './index-relatorio.css',
})
export class IndexRelatorio {
  private readonly router = inject(Router);
  public dialog = inject(MatDialog);

  clicouEmBuscar: number = 0;
  periodo: { start: Date | null; end: Date | null } = { start: null, end: null };
  listaDeTreinos: any[] = [];

  constructor() {
    //força o calendário e nomes para português
    moment.locale('pt-br');
  }


   BuscarRelatorio() {
    this.clicouEmBuscar++;
    const dataInicio = moment(this.periodo.start).format('YYYY-MM-DD');
    const dataFim = moment(this.periodo.end).format('YYYY-MM-DD');
    console.log('Buscar relatório de', dataInicio, 'até', dataFim, 'para o tipo de serviço:');
   }
}
