import { Routes } from '@angular/router';
import { Connexion } from './component/connexion/connexion';
import { Articles } from './component/articles/articles';
import { Inscription } from './component/inscription/inscription';
import { ArticleDetail } from './component/article-detail/article-detail';
import { MesArticles } from './component/mes-articles/mes-articles';
import { NouvelArticle } from './component/nouvel-article/nouvel-article';

export const routes: Routes = [
  {
    path: 'connexion',
    component: Connexion,
  },
  {
    path: 'articles/nouveau',
    component: NouvelArticle,
  },
  {
    path: 'articles/:annonceId',
    component: ArticleDetail,
  },
  {
    path: 'articles',
    component: Articles,
  },
  {
    path: 'mes-articles',
    component: MesArticles,
  },
  {
    path:'inscription',
    component: Inscription,
  },
  {
    path: '',
    redirectTo: 'articles',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: 'articles',
  },
];
