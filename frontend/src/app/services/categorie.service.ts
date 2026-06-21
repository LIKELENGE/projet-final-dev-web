import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { finalize } from 'rxjs';
import { Categorie } from './annonce.service';
import { getApiErrorMessage } from './api-error';
import { PagedResponse, emptyPagedResponse } from './paged-response';

export interface CategorieRequest {
  nom: string;
}

@Injectable({ providedIn: 'root' })
export class CategorieService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5124/categories';

  readonly categories = signal<Categorie[]>([]);
  readonly categorieSelectionnee = signal<Categorie | null>(null);
  readonly pagination = signal<PagedResponse<Categorie>>(emptyPagedResponse<Categorie>(8));
  readonly chargement = signal(false);
  readonly erreur = signal<string | null>(null);
  readonly message = signal<string | null>(null);

  charger(page = 1, taillePage = 8): void {
    const params = new HttpParams().set('page', page).set('taillePage', taillePage);

    this.chargement.set(true);
    this.erreur.set(null);

    this.http
      .get<PagedResponse<Categorie>>(this.apiUrl, { params })
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: (response) => {
          this.categories.set(response.items);
          this.pagination.set(response);
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  chargerCategorie(categorieId: number): void {
    this.chargement.set(true);
    this.erreur.set(null);
    this.categorieSelectionnee.set(null);

    this.http
      .get<Categorie>(`${this.apiUrl}/${categorieId}`)
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: (categorie) => this.categorieSelectionnee.set(categorie),
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  creer(request: CategorieRequest, apresCreation: () => void): void {
    this.chargement.set(true);
    this.erreur.set(null);
    this.message.set(null);

    this.http
      .post<Categorie>(this.apiUrl, request)
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: () => {
          this.message.set('Categorie creee.');
          apresCreation();
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  modifier(categorieId: number, request: CategorieRequest, apresModification: () => void): void {
    this.chargement.set(true);
    this.erreur.set(null);
    this.message.set(null);

    this.http
      .put<void>(`${this.apiUrl}/${categorieId}`, request)
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: () => {
          this.message.set('Categorie modifiee.');
          apresModification();
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  supprimer(categorieId: number, apresSuppression: () => void): void {
    this.chargement.set(true);
    this.erreur.set(null);
    this.message.set(null);

    this.http
      .delete<void>(`${this.apiUrl}/${categorieId}`)
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: () => {
          this.message.set('Categorie supprimee.');
          apresSuppression();
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }
}
