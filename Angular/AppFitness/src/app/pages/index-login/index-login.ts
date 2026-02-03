import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { TreinoApi } from '../../services/treino-api';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-index-login',
  imports: [FormsModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    RouterModule],
  templateUrl: './index-login.html',
  styleUrl: './index-login.css',
})
export class IndexLogin {
  private readonly router = inject(Router);
  private readonly treinoApi = inject(TreinoApi);

  hide: boolean = true;
  senha: string = '';
  login: string = '';
  mensagemAuxiliar: string = '';
  btn: number | string = '';
  numeroAuxiliarAltararSenhaOuCastrarUsuario: number = 0;



  // =============================
  // LOGIN
  // =============================
  btnlogar() {

  }

cadastrarNovoUsuario(){

}

  btncadastrarNovoUsuario() {

  }

  esqueciMinhaSenha() {

  }

  btnEsqueciMinhaSenha() {

  }

}
