import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-menu',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './menu.html',
  styleUrl: './menu.css',
})
export class Menu {

  private router = inject(Router);
  nomeUsuarioLogado: string | null = '';
  constructor() {
    this.nomeUsuarioLogado = localStorage.getItem('nomeUsuario');
    this.nomeUsuarioLogado = this.nomeUsuarioLogado?.toUpperCase() || '';
  }

  logout(): void {
    localStorage.clear();
    this.nomeUsuarioLogado = '';
    this.router.navigate(['/login']);
  }
}
