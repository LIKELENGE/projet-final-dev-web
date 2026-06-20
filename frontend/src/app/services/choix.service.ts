import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { finalize } from 'rxjs';
import { Categorie, Commune } from './annonce.service';
import { getApiErrorMessage } from './api-error';

export interface Sexe {
  code: number;
  nom: string;
}

@Injectable({ providedIn: 'root' })
export class ChoixService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5124/choix';

  readonly categories = signal<Categorie[]>([]);
  readonly communes = signal<Commune[]>([]);
  readonly sexes = signal<Sexe[]>([]);
  readonly chargement = signal(false);
  readonly erreur = signal<string | null>(null);

  charger(): void {
    this.chargement.set(true);
    this.erreur.set(null);

    this.http
      .get<Categorie[]>(`${this.apiUrl}/categories`)
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: (categories) => this.categories.set(categories),
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });

    this.http.get<Commune[]>(`${this.apiUrl}/communes`).subscribe({
      next: (communes) => this.communes.set(communes),
      error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
    });

    this.http.get<Sexe[]>(`${this.apiUrl}/sexes`).subscribe({
      next: (sexes) => this.sexes.set(sexes),
      error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
    });
  }
}
