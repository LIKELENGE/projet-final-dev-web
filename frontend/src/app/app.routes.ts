import { Routes } from '@angular/router';
import { Connexion } from './component/connexion/connexion';
import { Articles } from './component/articles/articles';
import { Inscription } from './component/inscription/inscription';
import { ArticleDetail } from './component/article-detail/article-detail';
import { MesArticles } from './component/mes-articles/mes-articles';
import { NouvelArticle } from './component/nouvel-article/nouvel-article';
import { ModifierArticle } from './component/modifier-article/modifier-article';
import { SupprimerArticle } from './component/supprimer-article/supprimer-article';
import { Categories } from './component/categories/categories';
import { CategorieCreate } from './component/categorie-create/categorie-create';
import { CategorieEdit } from './component/categorie-edit/categorie-edit';
import { CategorieDelete } from './component/categorie-delete/categorie-delete';

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
    path: 'mes-articles/:annonceId/modifier',
    component: ModifierArticle,
  },
  {
    path: 'mes-articles/:annonceId/supprimer',
    component: SupprimerArticle,
  },
  {
    path: 'categories',
    component: Categories,
  },
  {
    path: 'categories/nouveau',
    component: CategorieCreate,
  },
  {
    path: 'categories/:categorieId/modifier',
    component: CategorieEdit,
  },
  {
    path: 'categories/:categorieId/supprimer',
    component: CategorieDelete,
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
