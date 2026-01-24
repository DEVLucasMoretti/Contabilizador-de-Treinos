import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-menu',
  imports: [RouterLink, RouterLinkActive ],
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
