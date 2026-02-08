import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Treino } from '../Models/Treino';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Moment } from 'moment';
import { Usuario } from '../Models/Usuario';

@Injectable({
  providedIn: 'root',
})
export class TreinoApi {

  private readonly httpClient = inject(HttpClient);

  private readonly apiUrlTreinos: string;
  private readonly apiUrlAuth: string;

  constructor() {
    this.apiUrlTreinos = `${environment.apiUrl}/Treino`;
    this.apiUrlAuth= `${environment.apiUrl}/Auth`;

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
  getRelatorioDeTreinos(dataInicio: string, dataFim: string): Observable<Treino[]> {

    return this.httpClient.get<Treino[]>(`${this.apiUrlTreinos}?dataInicio=${dataInicio}&dataFim=${dataFim}`);
  }
  updateTreino(treino: Treino): Observable<Treino> {
    return this.httpClient.put<Treino>(this.apiUrlTreinos, treino);
  }
  addTreino(treino: Treino): Observable<Treino> {
    return this.httpClient.post<Treino>(this.apiUrlTreinos, treino);
  }

  login(usuario: Usuario): Observable<string> {
    return this.httpClient.post<string>(`${this.apiUrlAuth}`, usuario);
  }
  VerificaToken(token : string | null): Observable<boolean> {
    return this.httpClient.get<boolean>(`${this.apiUrlAuth}/VerificarToken?token=${token}`);
  }
}
