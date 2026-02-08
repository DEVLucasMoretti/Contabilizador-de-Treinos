import { CanActivate, CanActivateFn, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { inject, Injectable } from '@angular/core';
import { TreinoApi } from '../services/treino-api';
@Injectable({
  providedIn: 'root',
})

export class AuthGuard implements CanActivate {
    private readonly treinoApi = inject(TreinoApi);

  constructor(private router: Router) { }

  canActivate(): Observable<boolean> | Promise<boolean> | boolean {
    const token = localStorage.getItem('token');

    this.treinoApi.VerificaToken(token).subscribe({
      next: (isValid) => {
        if (!isValid) {
          this.router.navigate(['/login']);
        }
      },
      error: () => {
        this.router.navigate(['/login']);
      }
    });

    if (token) {
      // Se o token existir, permitimos o acesso
      return true;
    } else {
      // Se não houver token, redirecionamos para o login
      this.router.navigate(['/login']);
      return false;
    }
  }
}

