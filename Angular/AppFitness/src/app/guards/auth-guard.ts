import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { inject, Injectable } from '@angular/core';
import { TreinoApi } from '../services/treino-api';
import { map, catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {

  private readonly treinoApi = inject(TreinoApi);
  private snackBar: MatSnackBar = inject(MatSnackBar);


  constructor(private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {

    const token = localStorage.getItem('token');

    if (!token) {
      this.router.navigate(['/login']);
      return of(false);
    }

    return this.treinoApi.VerificaToken(token).pipe(

      map((isValid) => {

        if (!isValid) {
          this.router.navigate(['/login']);
          return false;
        }

        //PERMISSÃO
        const permissoes = JSON.parse(localStorage.getItem('permissoes') || '[]');

        const permissaoNecessaria = route.data['permissao'];

        if (permissaoNecessaria && !permissoes.includes(permissaoNecessaria)) {
          this.router.navigate(['/sem-permissao']);
          this.snackBarErro('Você não tem permissão para acessar esta página.');
          return false;
        }

        return true;
      }),

      catchError(() => {
        this.router.navigate(['/login']);
        return of(false);
      })
    );
  }


  snackBarErro(mensagem: string): void {

    this.snackBar.open(mensagem, 'OK', {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
      panelClass: ['snack-error']
    });
  }

}
