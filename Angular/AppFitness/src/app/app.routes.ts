import { Routes } from '@angular/router';
import { IndexHome } from './pages/index-home/index-home';
import { IndexContabilizarTreino } from './pages/index-contabilizar-treino/index-contabilizar-treino';
import { IndexRelatorio } from './pages/index-relatorio/index-relatorio';
import { IndexLogin } from './pages/index-login/index-login';
import { AuthGuard } from './guards/auth-guard';

export const routes: Routes =
[

  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: IndexHome, canActivate: [AuthGuard], data: { permissao: 'Home' }, title: 'Home'},
  {path: 'contabilizarTreino', component: IndexContabilizarTreino, canActivate: [AuthGuard], data: { permissao: 'ContabilizarTreino' }, title: 'contabilizar Treino'},
  {path: 'treinos', component: IndexContabilizarTreino, canActivate: [AuthGuard], data: { permissao: 'Treino' }, title: 'contabilizar Treino'},
  {path: 'relatorio', component: IndexRelatorio, canActivate: [AuthGuard], data: { permissao: 'Relatorio' }, title: 'Relatório'},
  {path: 'login', component: IndexLogin, title: 'Login'},
];


