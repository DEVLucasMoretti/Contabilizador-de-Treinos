import { Component } from '@angular/core';

@Component({
  selector: 'app-menu',
  imports: [],
  templateUrl: './menu.html',
  styleUrl: './menu.css',
})
export class Menu {

  nomeUsuarioLogado : string = '';
logout(): void {
    // Lógica para fazer logout do usuário
    console.log('Usuário deslogado');
  }
}
