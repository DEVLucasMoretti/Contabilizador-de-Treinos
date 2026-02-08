import { Routes } from '@angular/router';
import { IndexHome } from './pages/index-home/index-home';
import { IndexContabilizarTreino } from './pages/index-contabilizar-treino/index-contabilizar-treino';
import { IndexRelatorio } from './pages/index-relatorio/index-relatorio';
import { IndexLogin } from './pages/index-login/index-login';
import { AuthGuard } from './guards/auth-guard';

export const routes: Routes =
[

  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: IndexHome, canActivate: [AuthGuard], title: 'Home'},
  {path: 'contabilizarTreino', component: IndexContabilizarTreino, canActivate: [AuthGuard], title: 'contabilizar Treino'},
  {path: 'treinos', component: IndexContabilizarTreino, canActivate: [AuthGuard], title: 'contabilizar Treino'},
  {path: 'relatorio', component: IndexRelatorio, canActivate: [AuthGuard], title: 'Relatório'},
  {path: 'login', component: IndexLogin, title: 'Login'},
];


