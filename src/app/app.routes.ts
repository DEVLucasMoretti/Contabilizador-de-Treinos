import { Routes } from '@angular/router';
import { IndexHome } from './pages/index-home/index-home';

export const routes: Routes =
[

  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: IndexHome, title: 'Home'},
];


