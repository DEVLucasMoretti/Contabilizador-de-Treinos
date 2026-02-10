import { Usuario } from './../../Models/Usuario';
import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { TreinoApi } from '../../services/treino-api';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar } from '@angular/material/snack-bar';


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
  private readonly cdr = inject(ChangeDetectorRef);
  private snackBar: MatSnackBar = inject(MatSnackBar);

  hide: boolean = true;
  senha: string = '';
  login: string = '';
  mensagemAuxiliar: string = '';
  btn: number | string = '';
  numeroAuxiliarAltararSenhaOuCastrarUsuario: number = 0;

  usuario: Usuario = {
    Id: 0,
    Nome: '',
    Senha: ''
  };

  // =============================
  // LOGIN
  // =============================
  btnlogar() {
    this.usuario.Nome = this.login;
    this.usuario.Senha = this.senha;
    this.treinoApi.login(this.usuario).subscribe({
      next: (token) => {
        localStorage.setItem('token', token);
        localStorage.setItem('nomeUsuario', this.usuario.Nome); // Salva o nome do usuário
        this.router.navigate(['/home']);
      },
      error: (err) => {
        this.mensagemAuxiliar = 'Usuário ou senha inválidos.';
        this.cdr.detectChanges();
      }
    });
  }

  btnValidarUsuarioMestre() {
    this.treinoApi.getByMasterUser(this.login, this.senha).subscribe({
      next: () => {
        if (this.numeroAuxiliarAltararSenhaOuCastrarUsuario === 1)
          this.zerarInputsEColocarMensagemAuxiliarEAtivaBtn("Cadastre o novo usuário", 3);

        if (this.numeroAuxiliarAltararSenhaOuCastrarUsuario === 2)
          this.zerarInputsEColocarMensagemAuxiliarEAtivaBtn("Altere a senha do usuário", 4);
        this.cdr.detectChanges();
      },
      error: () => {
        this.mensagemAuxiliar = "Usuário Mestre inválido";
        this.cdr.detectChanges();
      }
    });
  }

  cadastrarNovoUsuario() {
    this.zerarInputsEColocarMensagemAuxiliarEAtivaBtn("Informe o usuário Mestre", 2);
    this.numeroAuxiliarAltararSenhaOuCastrarUsuario = 1;
  }

  btncadastrarNovoUsuario() {
    this.usuario.Nome = this.login;
    this.usuario.Senha = this.senha;

    this.treinoApi.addUsuario(this.usuario).subscribe({
      next: () => {
        this.snackBarSucesso('Novo usuário cadastrado com sucesso!');
        setTimeout(() => {
          window.location.reload();
        }, 1500);
      },
      error: () => {
        this.mensagemAuxiliar = "Erro ao cadastrar novo usuário, nome já existe.";
        this.cdr.detectChanges();
      }
    });
  }

  esqueciMinhaSenha() {
    this.zerarInputsEColocarMensagemAuxiliarEAtivaBtn("Informe o usuário Mestre", 2);
    this.numeroAuxiliarAltararSenhaOuCastrarUsuario = 2;
  }

  btnEsqueciMinhaSenha() {
    this.treinoApi.UpdatePasswordFromUser(this.login, this.senha).subscribe({
      next: () => {
        this.snackBarSucesso("Senha alterada com sucesso.");
        setTimeout(() => {
          window.location.reload();
        }, 1500);
      },
      error: () => {
        this.mensagemAuxiliar = "Erro ao alterar a senha, verifique se o usuário existe.";
        this.cdr.detectChanges();
      }
    });
  }

  // =============================
  // ÚTIL
  // =============================
  zerarInputsEColocarMensagemAuxiliarEAtivaBtn(mensagemAuxiliar: string, btn: number) {
    this.senha = "";
    this.login = "";
    this.mensagemAuxiliar = mensagemAuxiliar;
    this.btn = btn;
  }

  snackBarSucesso(mensagem: string): void {
    this.snackBar.open('Gravado com sucesso!', 'OK', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: ['snack-success']
    });
  }

}
