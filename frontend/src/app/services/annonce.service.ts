import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { finalize } from 'rxjs';
import { getApiErrorMessage } from './api-error';
import { PagedResponse, emptyPagedResponse } from './paged-response';
import { UtilisateurService } from './utilisateur.service';

export interface Categorie {
  id: number;
  nom: string;
}

export interface Commune {
  id: number;
  nom: string;
  codePostal?: string | null;
}

export interface UtilisateurResume {
  id: number;
  nom: string;
  prenom: string;
  mail: string;
}

export interface Annonce {
  id: number;
  utilisateur?: UtilisateurResume | null;
  categorie?: Categorie | null;
  commune?: Commune | null;
  nom: string;
  description?: string | null;
  prix: number;
  dateAjout: string;
  derniereModification: string;
  photos: PhotoAnnonce[];
}

export interface PhotoAnnonce {
  id: number;
  titre?: string | null;
  lien: string;
}

export interface PhotoAnnonceRequest {
  titre?: string | null;
  lien: string;
}

export interface ImageUploadResponse {
  titre?: string | null;
  lien: string;
}

export interface CreerAnnonceRequest {
  categorieId: number;
  communeId?: number | null;
  nom: string;
  description?: string | null;
  prix: number;
  photos: PhotoAnnonceRequest[];
}

export interface ModifierAnnonceRequest {
  categorieId: number;
  communeId?: number | null;
  nom: string;
  description?: string | null;
  prix: number;
}

@Injectable({ providedIn: 'root' })
export class AnnonceService {
  private readonly http = inject(HttpClient);
  private readonly utilisateurService = inject(UtilisateurService);
  private readonly apiUrl = 'http://localhost:5124/annonces';

  readonly annonces = signal<Annonce[]>([]);
  readonly mesAnnonces = signal<Annonce[]>([]);
  readonly annoncesPagination = signal<PagedResponse<Annonce>>(emptyPagedResponse<Annonce>(6));
  readonly mesAnnoncesPagination = signal<PagedResponse<Annonce>>(emptyPagedResponse<Annonce>(6));
  readonly annonceSelectionnee = signal<Annonce | null>(null);
  readonly chargement = signal(false);
  readonly chargementMesAnnonces = signal(false);
  readonly erreur = signal<string | null>(null);

  private get authorizationHeaders(): { Authorization: string } | null {
    const token = this.utilisateurService.token;

    return token ? { Authorization: `Bearer ${token}` } : null;
  }

  rechercher(recherche?: string, categorieId?: number | null, page = 1, taillePage = 6): void {
    let params = new HttpParams();
    params = params.set('page', page).set('taillePage', taillePage);

    if (recherche?.trim()) {
      params = params.set('recherche', recherche.trim());
    }

    if (categorieId) {
      params = params.set('categorieId', categorieId);
    }

    this.chargement.set(true);
    this.erreur.set(null);

    this.http
      .get<PagedResponse<Annonce>>(this.apiUrl, { params })
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: (response) => {
          this.annonces.set(response.items);
          this.annoncesPagination.set(response);
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  consulter(annonceId: number): void {
    this.chargement.set(true);
    this.erreur.set(null);
    this.annonceSelectionnee.set(null);

    this.http
      .get<Annonce>(`${this.apiUrl}/${annonceId}`)
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: (annonce) => this.annonceSelectionnee.set(annonce),
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  chargerMesAnnonces(page = 1, taillePage = 6): void {
    const headers = this.authorizationHeaders;

    if (!headers) {
      this.mesAnnonces.set([]);
      this.mesAnnoncesPagination.set(emptyPagedResponse<Annonce>(taillePage));
      return;
    }

    const params = new HttpParams().set('page', page).set('taillePage', taillePage);

    this.chargementMesAnnonces.set(true);
    this.erreur.set(null);

    this.http
      .get<PagedResponse<Annonce>>(`${this.apiUrl}/mes-annonces`, {
        headers,
        params,
      })
      .pipe(finalize(() => this.chargementMesAnnonces.set(false)))
      .subscribe({
        next: (response) => {
          this.mesAnnonces.set(response.items);
          this.mesAnnoncesPagination.set(response);
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  modifier(annonceId: number, request: ModifierAnnonceRequest, apresModification: () => void): void {
    const headers = this.authorizationHeaders;

    if (!headers) {
      this.erreur.set('Vous devez etre connecte pour modifier une annonce.');
      return;
    }

    this.chargement.set(true);
    this.erreur.set(null);

    this.http
      .put<void>(`${this.apiUrl}/${annonceId}`, request, {
        headers,
      })
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: () => {
          apresModification();
          this.rechercher();
          this.chargerMesAnnonces();
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  televerserImage(file: File, titre: string, apresUpload: (image: ImageUploadResponse) => void): void {
    const headers = this.authorizationHeaders;

    if (!headers) {
      this.erreur.set('Vous devez etre connecte pour ajouter une image.');
      return;
    }

    const formData = new FormData();
    formData.append('image', file);
    formData.append('titre', titre);

    this.chargement.set(true);
    this.erreur.set(null);

    this.http
      .post<ImageUploadResponse>('http://localhost:5124/images/upload', formData, {
        headers,
      })
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: (image) => apresUpload(image),
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  creer(request: CreerAnnonceRequest, apresCreation: () => void): void {
    const headers = this.authorizationHeaders;

    if (!headers) {
      this.erreur.set('Vous devez etre connecte pour creer une annonce.');
      return;
    }

    this.chargement.set(true);
    this.erreur.set(null);

    this.http
      .post<Annonce>(this.apiUrl, request, {
        headers,
      })
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: () => {
          apresCreation();
          this.rechercher();
          this.chargerMesAnnonces();
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }

  supprimer(annonceId: number, apresSuppression?: () => void): void {
    const headers = this.authorizationHeaders;

    if (!headers) {
      this.erreur.set('Vous devez etre connecte pour supprimer une annonce.');
      return;
    }

    this.chargement.set(true);
    this.erreur.set(null);

    this.http
      .delete<void>(`${this.apiUrl}/${annonceId}`, {
        headers,
      })
      .pipe(finalize(() => this.chargement.set(false)))
      .subscribe({
        next: () => {
          this.annonces.update((annonces) => annonces.filter((annonce) => annonce.id !== annonceId));
          this.mesAnnonces.update((annonces) => annonces.filter((annonce) => annonce.id !== annonceId));
          apresSuppression?.();
        },
        error: (error: unknown) => this.erreur.set(getApiErrorMessage(error)),
      });
  }
}
