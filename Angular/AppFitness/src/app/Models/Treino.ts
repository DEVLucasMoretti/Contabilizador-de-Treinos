import { Moment } from "moment";

export interface Treino {
  Id: number;
  Data: Date | null | Moment;
  DiaDaSemana: string;
  TreinoDoDia: string;
  QuantidadeCaloria : number;
}

