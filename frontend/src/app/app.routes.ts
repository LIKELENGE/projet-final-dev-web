import { Routes } from '@angular/router';
import { Connexion } from './component/connexion/connexion';
import { Articles } from './component/articles/articles';
import { Inscription } from './component/inscription/inscription';

export const routes: Routes = [
  {
    path: 'connexion',
    component: Connexion,
  },
  {
    path: 'articles',
    component: Articles,
  },
  {
    path:'inscription',
    component: Inscription,
  },
  {
    path: '',
    redirectTo: 'connexion',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: 'connexion',
  },
];
