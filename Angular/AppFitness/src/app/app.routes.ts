import { Routes } from '@angular/router';
import { IndexHome } from './pages/index-home/index-home';
import { IndexContabilizarTreino } from './pages/index-contabilizar-treino/index-contabilizar-treino';
import { IndexRelatorio } from './pages/index-relatorio/index-relatorio';

export const routes: Routes =
[

  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: IndexHome, title: 'Home'},
  {path: 'contabilizarTreino', component: IndexContabilizarTreino, title: 'contabilizar Treino'},
  {path: 'treinos', component: IndexContabilizarTreino, title: 'contabilizar Treino'},
  {path: 'relatorio', component: IndexRelatorio, title: 'contabilizar Treino'},
];


