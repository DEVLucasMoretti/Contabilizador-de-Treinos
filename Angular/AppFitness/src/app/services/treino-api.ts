import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Treino } from '../Models/Treino';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Moment } from 'moment';

@Injectable({
  providedIn: 'root',
})
export class TreinoApi {

  private readonly httpClient = inject(HttpClient);

  private readonly apiUrlTreinos: string;

  constructor() {
    this.apiUrlTreinos = `${environment.apiUrl}/Treino`;
  }

  getProgressoDaSemana(): Observable<Treino[]> {
    return this.httpClient.get<Treino[]>(`${this.apiUrlTreinos}/ProgressoSemana`);
  }

  getQuantidadeDeTodosTreinos(): Observable<number> {
    return this.httpClient.get<number>(`${this.apiUrlTreinos}/TotalDeDiasTreinados`);
  }
  getUpdateOuCreateTreino(data: string): Observable<Treino> {
    return this.httpClient.get<Treino>(`${this.apiUrlTreinos}?data=${data}`);
  }
}
